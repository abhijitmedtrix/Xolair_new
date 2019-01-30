﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using App.Data;
using App.Data.CSU;
using EnhancedUI.EnhancedScroller;
using MaterialUI;
using QuickEngine.Extensions;
using UnityEngine;
using UnityEngine.UI;

public class PatientJournalScreen : MonoBehaviour, IEnhancedScrollerDelegate
{
    public enum GraphView
    {
        Weekly,
        Monthly
    }

    public class GraphData
    {
        public GraphData(QuestionBasedTrackerData data, DateTime date, float interpolatedScore, float maxScore)
        {
            this.data = data;
            this.date = date;
            this.interpolatedScore = interpolatedScore;
            this.maxScore = maxScore;
        }

        public QuestionBasedTrackerData data;
        public DateTime date;
        public float interpolatedScore;
        public float maxScore;
        public bool dontUseInGraph;

        public void UpdateScore(float score)
        {
            this.interpolatedScore = score;
        }
    }

    [SerializeField] protected AppMode appMode;
    [SerializeField] protected GraphController _graphController;
    [SerializeField] protected CSUViewController _CSUViewController;
    [SerializeField] protected GraphRenderer _graphRenderer;
    [SerializeField] protected ScreenConfig _screenConfig;
    [SerializeField] protected RectTransform _graphContainer;
    [SerializeField] protected RectTransform _scrollerRectTransform;
    [SerializeField] protected Toggle _typeToggle;
    [SerializeField] protected NotesScreen _notesScreen;

    [Header("Hints")] [SerializeField] protected Hint _scoreHint;
    [SerializeField] protected Hint _notesHint;
    [SerializeField] protected Hint _photoHint;
    [SerializeField] private PhotosScreen photosScreen;

    protected Dictionary<TrackerManager.TrackerType, float> _scrollLastPositions =
        new Dictionary<TrackerManager.TrackerType, float>();

    protected string[] _graphViewPickOptions;
    protected Rect _nativePopupRect;
    protected GraphView _graphView;
    protected TrackerManager.TrackerType _trackerType;
    private DayScrollItemView _lastActiveCellView;
    protected int _lastActiveCellIndex = -1;
    protected int _daysToShow;
    protected bool _initialized;
    protected List<GraphData> _graphDatas = new List<GraphData>();

    /// <summary>
    /// In this example we are going to use a standard generic List. We could have used
    /// a SmallList for efficiency, but this is just a demonstration that other list
    /// types can be used.
    /// </summary>
    private List<GraphData> _data;

    /// <summary>
    /// Reference to the scrollers
    /// </summary>
    [SerializeField] public EnhancedScroller scroller;

    /// <summary>
    /// Reference to the cell prefab
    /// </summary>
    public EnhancedScrollerCellView cellViewPrefab;

    private void Awake()
    {
        _typeToggle.onValueChanged.AddListener(OnTrackerTypeToggleValueChanged);
        _screenConfig.OnShowStarted += OnShowStarted;
    }

    private void Start()
    {
        _nativePopupRect = new Rect(new Vector2(Screen.width / 2f, Screen.height / 2f),
            new Vector2(Screen.width * 0.25f, Screen.height * 0.3f));

        // set native picker dialog options
        _graphViewPickOptions = Enum.GetNames(typeof(GraphView));
        Array.Resize(ref _graphViewPickOptions, _graphViewPickOptions.Length + 1);
        _graphViewPickOptions[_graphViewPickOptions.Length - 1] = "Cancel";

        RectOffset offset = scroller.padding;
        offset.left = offset.right = Mathf.RoundToInt(_scrollerRectTransform.rect.width / 2);
        scroller.padding = offset;
    }

    private void OnShowStarted()
    {
        _trackerType = DefineTrackerType(_typeToggle.isOn);
        Initialize();
    }

    private void Initialize()
    {
        // setup scroller parameters
        if (appMode == AppMode.SAA)
        {
            if (_graphView == GraphView.Weekly)
            {
                // show 2 weeks + 1 day = 15
                _daysToShow = 15;
            }
            else if (_graphView == GraphView.Monthly)
            {
                // 2 months to show (max is Dec 31 + Jan 31 + 3 days)
                _daysToShow = 65;
            }
        }
        else if (appMode == AppMode.CSU)
        {
            // show 5 days according to UI reference
            _daysToShow = 5;
        }

        // clear old data
        Dispose();

        Debug.Log("Tracker type: " + _trackerType);

        _graphContainer.gameObject.SetActive(false);
        _CSUViewController.gameObject.SetActive(false);

        // hide both hints
        _scoreHint.UpdateValue(string.Empty);
        _photoHint.UpdateValue(string.Empty);
        _notesHint.UpdateValue(string.Empty);

        List<DateTime> fullDateRange = TrackerManager.GetDataDateRange(_trackerType);

        // can ba a case when there is no date at all
        if (fullDateRange == null)
        {
            return;
        }
        else
        {
            Debug.Log("Max date range count: " + fullDateRange.Count);
        }

        // for SAA we should add last some Data to set start with Sunday for weeks display        
        if (appMode == AppMode.SAA)
        {
            DateTime firstEntryData = fullDateRange[0];

            int daysToAdd = 0;

            if (_graphView == GraphView.Weekly)
            {
                // originally DayOfWeek enum starts with Sunday, so if it's not Sunday, insert some days in a list 
                daysToAdd = (int) firstEntryData.DayOfWeek;
            }
            else if (_graphView == GraphView.Monthly)
            {
                // check how many days starting fro the 1st date of the month
                daysToAdd = firstEntryData.Day - 1;
            }

            for (int i = 0; i < daysToAdd; i++)
            {
                fullDateRange.Insert(0, firstEntryData.AddDays(-(i + 1)));
            }

            Debug.Log($"Added {daysToAdd} days");
        }

        int maxScore = 0;
        int numOfLabels = 1;

        switch (_trackerType)
        {
            // define what to show
            case TrackerManager.TrackerType.Symptom:
                _graphContainer.gameObject.SetActive(true);
                maxScore = TrackerManager.GetMaxScore(TrackerManager.TrackerType.Symptom);
                numOfLabels = 8;
                break;
            case TrackerManager.TrackerType.Asthma:
                _graphContainer.gameObject.SetActive(true);
                maxScore = TrackerManager.GetMaxScore(TrackerManager.TrackerType.Asthma);
                numOfLabels = 13;
                break;
            case TrackerManager.TrackerType.CSU:
                _CSUViewController.gameObject.SetActive(true);
                maxScore = TrackerManager.GetMaxScore(TrackerManager.TrackerType.CSU);
                break;
            case TrackerManager.TrackerType.UAS:
                _graphContainer.gameObject.SetActive(true);
                maxScore = TrackerManager.GetMaxScore(TrackerManager.TrackerType.UAS);
                numOfLabels = 8;
                break;
        }

        Debug.Log("Max score: " + maxScore);

        // fill up all data with struct
        List<GraphData> listWithData = new List<GraphData>();
        List<GraphData> interpolationList = new List<GraphData>();
        GraphData graphData;
        
        for (int i = 0; i < fullDateRange.Count; i++)
        {
            QuestionBasedTrackerData data = TrackerManager.GetData(fullDateRange[i], _trackerType, false);

            // create default struct
            graphData = new GraphData(data, fullDateRange[i], 0, maxScore);

            // if some entry exist at this date that GraphStruct will be 1 of 2 interpolation side values
            if (data != null)
            {
                graphData.UpdateScore(data.GetScore());
            }

            graphData.dontUseInGraph = data == null;
            _graphDatas.Add(graphData);
        }
        
        // that is a complex solution in case when needed interpolation between 2 actual data entries, to shop <middle> value. It doesn't work correctly so should be fixes later on
        #region Complex solution
        /*
        int firstDataEntryIndex = -1;
        int lastDataEntryIndex = -1;
        
        for (int i = 0; i < fullDateRange.Count; i++)
        {
            QuestionBasedTrackerData data = TrackerManager.GetData(fullDateRange[i], _trackerType, false);

            // create default struct
            graphData = new GraphData(data, fullDateRange[i], 0, maxScore);

            // if some entry exist at this date that GraphStruct will be 1 of 2 interpolation side values
            if (data != null)
            {
                if (firstDataEntryIndex < 0)
                {
                    firstDataEntryIndex = i;
                }

                listWithData.Add(graphData);
                graphData.UpdateScore(data.GetScore());

                // each time new entry with data is added need to interpolate all values between 2 entries
                if (listWithData.Count > 1)
                {
                    InterpolateData(listWithData[0].interpolatedScore,
                        listWithData[listWithData.Count - 1].interpolatedScore, interpolationList);

                    // remove 1st base object used in last interpolation
                    listWithData.RemoveAt(0);

                    interpolationList.Clear();
                }

                lastDataEntryIndex = i;
            }
            else if (firstDataEntryIndex > -1)
            {
                // add to interpolation list which will be used later to fill up middle values for dates, without data
                interpolationList.Add(graphData);
            }
            else
            {
                Debug.Log($"Graph data dated: {graphData.date} should be skipped in graph");
                graphData.dontUseInGraph = true;
            }

            _graphDatas.Add(graphData);
        }

        // if last real data index doesn't equals to total count, that means some last entries doesn't have a real data filled by user. Flag it to skip in graph renderer
        if (lastDataEntryIndex < _graphDatas.Count - 1)
        {
            int startIndex = lastDataEntryIndex > -1 ? lastDataEntryIndex + 1 : 0;

            for (int i = lastDataEntryIndex + 1; i < _graphDatas.Count; i++)
            {
                Debug.Log($"Graph data dated: {_graphDatas[i].date} should be skipped in graph");
                _graphDatas[i].dontUseInGraph = true;
            }
        }
        */
        #endregion

        // set up the scroller delegates
        scroller.Delegate = this;
        scroller.scrollerScrolled = ScrollerScrolled;
        scroller.scrollerSnapped = ScrollerSnapped;

        // set scroller data
        SetData(_graphDatas);

        // Debug.Log(
        // $"First date: {fullDateRange[0]}, max date range to show: {fullDateRange.Count}, Last date: {fullDateRange[fullDateRange.Count - 1]}");

        // if it's a mode with active graph controller
        if (_trackerType != TrackerManager.TrackerType.CSU)
        {
            // update graph mesh, labels and other data
            // _graphController.Initialize(_graphDatas.Where(x => !x.dontUseInGraph).ToArray(), _mode != AppManager.Mode.SAA, false, maxScore,
            _graphController.Initialize(_graphDatas.ToArray(), appMode != AppMode.SAA, false, maxScore, _daysToShow,
                numOfLabels);

            _graphController.UpdateCameraView(scroller.NormalizedScrollPosition);
        }

        Debug.Log("Data count: " + _data.Count);
        _initialized = true;
    }

    private void InterpolateData(float startValue, float endValue, List<GraphData> list)
    {
        float interval = 1f / (list.Count + 1);

        for (int i = 0; i < list.Count; i++)
        {
            GraphData obj = list[i];
            obj.interpolatedScore = Mathf.Lerp(startValue, endValue, interval * i);
        }
    }

    private TrackerManager.TrackerType DefineTrackerType(bool toggleIsOn)
    {
        // Debug.Log("toggleIsOn: " + toggleIsOn);

        // define what to show
        if (appMode == AppMode.SAA)
        {
            if (!toggleIsOn)
            {
                return TrackerManager.TrackerType.Symptom;
            }
            else
            {
                return TrackerManager.TrackerType.Asthma;
            }
        }
        else
        {
            if (!toggleIsOn)
            {
                return TrackerManager.TrackerType.CSU;
            }
            else
            {
                return TrackerManager.TrackerType.UAS;
            }
        }
    }

    private void OnTrackerTypeToggleValueChanged(bool toggleIsOn)
    {
        _trackerType = DefineTrackerType(toggleIsOn);
        Initialize();
    }

    private void ScrollerSnapped(EnhancedScroller enhancedScroller, int cellindex, int dataindex,
        EnhancedScrollerCellView cellview)
    {
        // Debug.Log($"ScrollerSnapped. DataIndex: {dataindex}, cellIndex: {cellview}");
    }

    private void ScrollerScrolled(EnhancedScroller enhancedScroller, Vector2 val, float scrollposition)
    {
        int currentActiveCellIndex = scroller.GetClosestCellIndex();
        // Debug.Log($"ScrollerScrolled. _lastActiveCellIndex: {_lastActiveCellIndex}, currentActiveCellIndex: {currentActiveCellIndex}, total data count: {_data.Count}");

        // check is start and end cell items indexes are in a range of 7 days and value not equals to previously cached value
        if (_lastActiveCellIndex != currentActiveCellIndex && currentActiveCellIndex > -1 &&
            currentActiveCellIndex < _data.Count)
        {
            _lastActiveCellIndex = currentActiveCellIndex;

            EnhancedScrollerCellView activeCell = scroller.GetClosestCellView();

            if (activeCell != null)
            {
                // set focus of cells only for daily view in CSU mode
                if (appMode == AppMode.CSU)
                {
                    if (activeCell != null)
                    {
                        _lastActiveCellView?.SetFocus(false);

                        _lastActiveCellView = (activeCell as DayScrollItemView);
                        // Debug.Log(
                        // $"active cell data: {_lastActiveCellView.graphData.data}, date: {_lastActiveCellView.graphData.date}, value: {_lastActiveCellView.graphData.interpolatedScore}");
                        _lastActiveCellView.SetFocus(true);
                    }
                }
            }
        }

        QuestionBasedTrackerData data = _data[_lastActiveCellIndex].data;

        // for CSU
        if (_trackerType == TrackerManager.TrackerType.CSU)
        {
            // Debug.Log("data: " + data);

            // hide by default
            _photoHint.UpdateValue(null);

            if (data != null)
            {
                // if there are some photos
                int numOfPhotos = (data as CSUData).GetPhotosCount();
                // Debug.Log("Photos count: " + numOfPhotos);
                if (numOfPhotos > 0)
                {
                    _photoHint.UpdateValue(numOfPhotos.ToString());
                }
            }

            _CSUViewController.UpdateData(data as CSUData);
        }
        // if using the graph (3 of 4 trackers)
        else
        {
            // move graph camera together with a slider
            _graphController.UpdateCameraView(val.x);

            // set score
            _scoreHint.UpdateValue(data == null ? string.Empty : data.GetScore().ToString());
        }

        // cache last scroll position to restore last position on screen Hide >> Show or tracker type change
        if (_scrollLastPositions.ContainsKey(_trackerType))
        {
            _scrollLastPositions[_trackerType] = val.x;
        }
        else
        {
            _scrollLastPositions.Add(_trackerType, val.x);
        }

        // check notes
        if (_lastActiveCellIndex > -1)
        {
            List<NoteData> notes = NotesManager.GetNoteData(_data[_lastActiveCellIndex].date);
            if (notes.Count > 0)
            {
                _notesHint.UpdateValue(notes.Count.ToString());
            }
            else
            {
                _notesHint.UpdateValue(null);
            }
        }
        else
        {
            _notesHint.UpdateValue(null);
        }
    }

    public void SetType(TrackerManager.TrackerType type)
    {
        _trackerType = type;
    }

    public void SetData(List<GraphData> graphDatas)
    {
        _data = graphDatas;

        float scrollPosition;
        // if some value been cached before
        if (!_scrollLastPositions.TryGetValue(_trackerType, out scrollPosition))
        {
            scrollPosition = 1;
        }

        // tell the scroller to reload now that we have the data
        scroller.ReloadData(scrollPosition);

        // we need to snap immediately but scroller asset got some limitation here, that's why need to use this workaround
        EnhancedScroller.TweenType tween = scroller.snapTweenType;
        scroller.snapTweenType = EnhancedScroller.TweenType.immediate;
        scroller.Snap();
        scroller.snapTweenType = tween;

        // simulate scrolling callback after forced position setup to set correct focus and update ui. ScrollPosition parameter can be any value, it doesn't used anyway
        ScrollerScrolled(scroller, new Vector2(scrollPosition, 0), 1);
    }

    private IEnumerator WaitForFrameRoutine()
    {
        yield return null;
    }

    public void ShowPhotosPopup()
    {
        // update photo hint if needed
        if (_trackerType == TrackerManager.TrackerType.CSU)
        {
            CSUData csuData =
                TrackerManager.GetData(_data[_lastActiveCellIndex].date, TrackerManager.TrackerType.CSU) as CSUData;

            // if data exists
            if (csuData != null)
            {
                string[] texturesPaths = csuData.GetAllPhotosPaths();
                photosScreen.Show(texturesPaths, csuData.GetDate());
            }
        }
    }

    public void AddNote()
    {
        if (_lastActiveCellIndex > -1)
        {
            _notesScreen.LoadData(_data[_lastActiveCellIndex].date);
            ScreenManager.Instance.Set(11);
        }
    }

    public void Dispose()
    {
        _graphDatas.Clear();
        _data = null;

        // set up the scroller delegates
        scroller.Delegate = null;
        scroller.scrollerScrolled = null;
    }

    public void ChangeGraphView()
    {
        if (Application.isEditor)
        {
            GraphViewPicked(_graphView == GraphView.Weekly ? (long) GraphView.Monthly : (long) GraphView.Weekly);
        }
        else
        {
            NativePicker.Instance.ShowCustomPicker(_nativePopupRect, _graphViewPickOptions, (int) _graphView,
                GraphViewPicked, null);
        }
    }

    private void GraphViewPicked(long option)
    {
        GraphView newView = (GraphView) (int) option;

        if (_graphView != newView)
        {
            _graphView = newView;

            Initialize();

            scroller.RefreshActiveCellViews();
        }
    }

    #region EnhancedScroller Handlers

    /// <summary>
    /// This tells the scroller the number of cells that should have room allocated. This should be the length of your data array.
    /// </summary>
    /// <param name="scroller">The scroller that is requesting the data size</param>
    /// <returns>The number of cells</returns>
    public int GetNumberOfCells(EnhancedScroller scroller)
    {
        // in this example, we just pass the number of our data elements
        return _data.Count;
    }

    /// <summary>
    /// This tells the scroller what the size of a given cell will be. Cells can be any size and do not have
    /// to be uniform. For vertical scrollers the cell size will be the height. For horizontal scrollers the
    /// cell size will be the width.
    /// </summary>
    /// <param name="scroller">The scroller requesting the cell size</param>
    /// <param name="dataIndex">The index of the data that the scroller is requesting</param>
    /// <returns>The size of the cell</returns>
    public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
    {
        // Debug.Log($"Screen width: {Screen.width}, _rectTransform size delta: {_scrollerRectTransform.sizeDelta} and rect size: {_scrollerRectTransform.rect.size}");
        return _scrollerRectTransform.rect.width / _daysToShow;
    }

    /// <summary>
    /// Gets the cell to be displayed. You can have numerous cell types, allowing variety in your list.
    /// Some examples of this would be headers, footers, and other grouping cells.
    /// </summary>
    /// <param name="scroller">The scroller requesting the cell</param>
    /// <param name="dataIndex">The index of the data that the scroller is requesting</param>
    /// <param name="cellIndex">The index of the list. This will likely be different from the dataIndex if the scroller is looping</param>
    /// <returns>The cell for the scroller to use</returns>
    public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
    {
        // first, we get a cell from the scroller by passing a prefab.
        // if the scroller finds one it can recycle it will do so, otherwise
        // it will create a new cell.
        DayScrollItemView cellView = scroller.GetCellView(cellViewPrefab) as DayScrollItemView;

        // set the name of the game object to the cell's data index.
        // this is optional, but it helps up debug the objects in 
        // the scene hierarchy.
        cellView.name = "Cell " + dataIndex.ToString();

        // in this example, we just pass the data to our cell's view which will update its UI
        DateTime firstDate = _data[0].date;
        GraphData graphData = _data[dataIndex];

        // define what data to provide
        if (appMode == AppMode.SAA)
        {
            if (_graphView == GraphView.Weekly)
            {
                // Debug.Log($"data date: {data.date}, firstDate: {firstDate}");
                // show week text only for the 1st day of the week (Sunday)
                if (graphData.date.DayOfWeek == DayOfWeek.Sunday)
                {
                    // check num of week
                    int numOfWeek = Mathf.FloorToInt(graphData.date.Subtract(firstDate).Days / 7f) + 1;

                    DateTime periodEndDate = graphData.date.AddDays(7);

                    cellView.SetData(graphData, false, null, numOfWeek,
                        new StringBuilder(graphData.date.FormatToDateMonth()).Append("-")
                            .Append(periodEndDate.FormatToDateMonth())
                            .ToString());
                }
                else
                {
                    cellView.SetData(graphData, false);
                }
            }
            else if (_graphView == GraphView.Monthly)
            {
                // show month only for the 1st day of the month
                if (graphData.date.Day == 1)
                {
                    // check what week range does this month it include
                    int daysToMonthStart = graphData.date.Subtract(firstDate).Days;
                    int daysToMonthEnd = graphData.date.EndOfMonth().Subtract(firstDate).Days;
                    
                    int startNumOfWeek = Mathf.FloorToInt(daysToMonthStart / 7f) + 1;
                    int endNumOfWeek = Mathf.FloorToInt(daysToMonthEnd / 7f) + 1;
                    StringBuilder sb = new StringBuilder("WEEK ").Append(startNumOfWeek).Append(" - WEEK ").Append(endNumOfWeek);
                    
                    cellView.SetData(graphData, false, new []{graphData.date.ToString("MMM").ToUpper(), sb.ToString()});
                }
                else
                {
                    cellView.SetData(graphData, false);
                }
            }
        }
        else
        {
            cellView.SetData(graphData, true);
        }

        // return the cell to the scroller
        return cellView;
    }

    #endregion

    #region Tests

    public void NextDay()
    {
        if (_lastActiveCellIndex + 4 < _data.Count)
        {
            _lastActiveCellIndex++;
        }
    }

    public void PrevDay()
    {
        if (_lastActiveCellIndex - 4 >= 0)
        {
            _lastActiveCellIndex--;
        }
    }

    #endregion
}