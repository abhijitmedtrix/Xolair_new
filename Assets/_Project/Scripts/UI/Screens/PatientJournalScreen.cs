﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using App.Data;
using App.Data.CSU;
using EnhancedUI;
using EnhancedUI.EnhancedScroller;
using MaterialUI;
using UnityEngine;
using UnityEngine.UI;

public class PatientJournalScreen : MonoBehaviour, IEnhancedScrollerDelegate
{
    public struct GraphScruct
    {
        public GraphScruct(QuestionBasedTrackerData data, DateTime date, float interpolatedScore, float maxScore)
        {
            this.data = data;
            this.data = data;
            this.interpolatedScore = interpolatedScore;
            this.maxScore = maxScore;
        }

        public QuestionBasedTrackerData data;
        public DateTime date;
        public float interpolatedScore;
        public float maxScore;
    }

    [SerializeField] protected AppManager.Mode _mode;
    [SerializeField] protected GraphController _graphController;
    [SerializeField] protected CSUViewController _CSUViewController;
    [SerializeField] protected GraphRenderer _graphRenderer;
    [SerializeField] protected ScreenConfig _screenConfig;
    [SerializeField] protected RawImage _graphImage;
    [SerializeField] protected RectTransform _graphContainer;
    [SerializeField] protected RectTransform _scrollerRectTransform;
    [SerializeField] protected Toggle _typeToggle;

    [Header("Hints")] [SerializeField] protected Hint _scoreHint;
    [SerializeField] protected Hint _photoHint;
    [SerializeField] private Show_Images show_Images;

    protected TrackerManager.TrackerType _trackerType;
    protected int _lastMiddleCellIndex;
    protected int _daysToShow;

    protected List<GraphScruct> _graphDatas = new List<GraphScruct>();

    public TrackerManager.TrackerType trackerType
    {
        get { return _trackerType; }
        set { _trackerType = value; }
    }

    /// <summary>
    /// In this example we are going to use a standard generic List. We could have used
    /// a SmallList for efficiency, but this is just a demonstration that other list
    /// types can be used.
    /// </summary>
    private List<GraphScruct> _data;

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
        _typeToggle.onValueChanged.AddListener(value => SwitchTrackerType());
        _screenConfig.OnShowStarted += OnShowStarted;

        // setup scroller parameters
        if (_mode == AppManager.Mode.SAA)
        {
            // show 2 weeks + 1 day = 15
            _daysToShow = 15;
        }
        else if (_mode == AppManager.Mode.CSU)
        {
            // show 5 days according to UI reference
            _daysToShow = 5;
        }
    }

    private void Start()
    {
        RectOffset offset = scroller.padding;
        offset.left = offset.right = Mathf.RoundToInt(_scrollerRectTransform.rect.width / 2);
        scroller.padding = offset;
    }

    private void OnShowStarted()
    {
        DefineTrackerType();
        Initialize();
    }

    private async void Initialize()
    {
        _graphController.gameObject.SetActive(false);
        _CSUViewController.gameObject.SetActive(false);

        List<DateTime> maxDateRange = TrackerManager.GetDataDateRange(_trackerType);

        // can ba a case when there is no date at all
        if (maxDateRange == null)
        {
            return;
        }

        // for SAA we should add last some Data to set start with Sunday for weeks display        
        if (_mode == AppManager.Mode.SAA)
        {
            DateTime firstEntryData = maxDateRange[0];

            // originally DayOfWeek enum starts with Sunday, so if it's not Sunday, insert some days in a list 
            int dayInt = (int) firstEntryData.DayOfWeek;

            for (int i = 0; i < dayInt; i++)
            {
                maxDateRange.Insert(0, firstEntryData.AddDays(-(i + 1)));
            }

            Debug.Log($"Added {dayInt} days");
        }

        int maxScore = 0;

        switch (_trackerType)
        {
            // define what to show
            case TrackerManager.TrackerType.Symptom:
                _graphController.gameObject.SetActive(true);
                maxScore = TrackerManager.GetMaxScore(TrackerManager.TrackerType.Symptom);
                break;
            case TrackerManager.TrackerType.Asthma:
                _graphController.gameObject.SetActive(true);
                maxScore = TrackerManager.GetMaxScore(TrackerManager.TrackerType.Asthma);
                break;
            case TrackerManager.TrackerType.CSU:
                _CSUViewController.gameObject.SetActive(true);
                maxScore = TrackerManager.GetMaxScore(TrackerManager.TrackerType.CSU);
                break;
            case TrackerManager.TrackerType.UAS:
                _graphController.gameObject.SetActive(true);
                maxScore = TrackerManager.GetMaxScore(TrackerManager.TrackerType.UAS);
                break;
        }

        Debug.Log("Max score: " + maxScore);

        // fill up all data with struct
        List<GraphScruct> listWithData = new List<GraphScruct>();
        List<GraphScruct> interpolationList = new List<GraphScruct>();

        for (int i = 0; i < maxDateRange.Count; i++)
        {
            QuestionBasedTrackerData data = TrackerManager.GetData(maxDateRange[i], _trackerType);

            // create default struct
            GraphScruct graphData = new GraphScruct(data, maxDateRange[i], -1, maxScore);
            
            // add to interpolation list which will be used later to fill up middle values for dates, without data
            interpolationList.Add(graphData);
            
            if (data != null)
            {
                listWithData.Add(graphData);
                graphData.interpolatedScore = data.GetScore();
                
                // each time new entry with data is added need to interpolate all values between 2 entries
                if (listWithData.Count > 1)
                {
                    InterpolateData(interpolationList);
                }
                
                interpolationList.Clear();
            }

            _graphDatas.Add(graphData);
        }


        // set scroller data
        SetData(_graphDatas);

        // set up the scroller delegates
        scroller.Delegate = this;
        scroller.scrollerScrolled = ScrollerScrolled;

        // Debug.Log(
        // $"First date: {maxDateRange[0]}, max date range to show: {maxDateRange.Count}, Last date: {maxDateRange[maxDateRange.Count - 1]}");

        // if it's a mode with active graph controller
        if (_graphContainer.gameObject.activeSelf)
        {
            // update graph labels
            _graphController.UpdateLabels(maxScore);

            _graphRenderer.Init(maxScore, _daysToShow);
            _graphImage.texture = _graphRenderer.GetRenderTexture();
        }

        Debug.Log("Data count: " + _data.Count);
        Debug.Log("scroller: " + scroller);

        // wait to allow scroller update
        await WaitForFrameRoutine();
        
        // scroll to the end
        scroller.JumpToDataIndex(_data.Count - 1);

        // update graph
        SetDateRange(_data.Count - 1);
        // SetDateRange(0);
    }

    private void InterpolateData(List<GraphScruct> list)
    {
        float start = list[0].interpolatedScore;
        float end = list[list.Count - 1].interpolatedScore;
        float interval = 1 / list.Count - 1;

        for (int i = 1; i < list.Count-1; i++)
        {
            GraphScruct obj = list[i];
            obj.interpolatedScore = Mathf.Lerp(start, end, interval * i);
        }
    }

    private void DefineTrackerType()
    {
        // define what to show
        if (_mode == AppManager.Mode.SAA)
        {
            if (!_typeToggle.isOn)
            {
                trackerType = TrackerManager.TrackerType.Symptom;
            }
            else
            {
                trackerType = TrackerManager.TrackerType.Asthma;
            }
        }
        else if (_mode == AppManager.Mode.CSU)
        {
            if (!_typeToggle.isOn)
            {
                trackerType = TrackerManager.TrackerType.CSU;
            }
            else
            {
                trackerType = TrackerManager.TrackerType.UAS;
            }
        }
    }

    public void SwitchTrackerType()
    {
        DefineTrackerType();
        Initialize();
    }

    public void OnDragStart()
    {
        scroller.snapping = false;
    }

    public void OnDragStop()
    {
        scroller.snapping = true;
    }

    private void ScrollerScrolled(EnhancedScroller enhancedScroller, Vector2 val, float scrollposition)
    {
        // Debug.Log($"_lastMiddleCellIndex: {_lastMiddleCellIndex}, closest cell: {scroller.GetClosestCell()}");
        int focusedCellIndex = scroller.GetClosestCell();

        // check is start and end cell items indexes are in a range of 7 days and value not equals to previously cached value
        if (_lastMiddleCellIndex != focusedCellIndex && focusedCellIndex - 3 >= 0 && focusedCellIndex + 3 < _data.Count)
        {
            _lastMiddleCellIndex = scroller.GetClosestCell();

            // update date range
            SetDateRange(_lastMiddleCellIndex);
        }

        // for debug only
        /*
        if (scroller.GetCellViewAtDataIndex(_lastMiddleCellIndex) != null)
        {
            CalendarScrollItemView itemView =
                scroller.GetCellViewAtDataIndex(_lastMiddleCellIndex) as CalendarScrollItemView;
            Debug.Log("Get closest point: " + _lastMiddleCellIndex
                                            + " with data " + itemView.data.ToShortDateString());
        }
        else
        {
            Debug.Log("Get closest point: " + _lastMiddleCellIndex);
        }
        */
    }

    public void SetType(TrackerManager.TrackerType type)
    {
        _trackerType = type;
    }

    public void SetData(List<GraphScruct> graphDatas)
    {
        _data = graphDatas;

        // tell the scroller to reload now that we have the data
        scroller.ReloadData();

        // update graph paddings
        Vector2 min = _graphContainer.offsetMin;
        Vector2 max = _graphContainer.offsetMax;
        min.x = _scrollerRectTransform.rect.width / 7 / 2f;
        max.x = -_scrollerRectTransform.rect.width / 7 / 2f;
        _graphContainer.offsetMin = min;
        _graphContainer.offsetMax = max;
    }

    private void SetDateRange(int activeIndex)
    {
        // check all data within displayed range
        CalculateDataByDateRange();

        // update graph mesh
        // _graphRenderer.UpdateGraph(_scoreByDaysRange);

        // transform render texture (graph camera) point position to UI graph
        Vector3 viewportPos = _graphRenderer.GetLastDayPointViewportPosition();
        Vector3 localPos = new Vector3(_graphContainer.rect.width * _graphContainer.pivot.x,
            _graphContainer.rect.height * viewportPos.y, 0);

        // set point
        // _graphPoint.localPosition = localPos;
    }

    private async void CalculateDataByDateRange()
    {
        await WaitForFrameRoutine();
        SmallList<EnhancedScrollerCellView> list = scroller.GetActiveCellViews();
        Debug.Log("Visible items count: " + list.Count);

        // find 1st data before 1st element in a range
        int prefixIndex = -1;
        if (list[0].dataIndex > 0)
        {
            // try find any
            for (int i = list[0].dataIndex - 1; i > -1; i--)
            {
                if (TrackerManager.GetData(_data[i].date, _trackerType) != null)
                {
                    prefixIndex = i;
                    break;
                }
            }
        }

        // find 1st data after last element in a range
        int postfixIndex = -1;
        if (list[list.Count - 1].dataIndex < _data.Count - 1)
        {
            // try find any
            for (int i = list[0].dataIndex - 1; i > -1; i--)
            {
                if (TrackerManager.GetData(_data[i].date, _trackerType) != null)
                {
                    postfixIndex = i;
                    break;
                }
            }
        }


        for (int i = 0; i < list.Count; i++)
        {
            Debug.Log($"Item indexes: {i} and data: {list[i]}");
        }
    }

    private IEnumerator WaitForFrameRoutine()
    {
        yield return null;
    }

    public void ShowPhotosPopup()
    {
        // update photo hint if needed
        if (_trackerType == TrackerManager.TrackerType.CSU || _trackerType == TrackerManager.TrackerType.UAS)
        {
            CSUData csuData =
                TrackerManager.GetData(_data[_lastMiddleCellIndex + 3].date, TrackerManager.TrackerType.CSU) as CSUData;
            // if data exists
            if (csuData != null)
            {
                Texture2D[] textures = csuData.GetAllPhotos();
                if (textures.Length != 0)
                {
                    AppManager.Images.Clear();

                    foreach (Texture2D tx in textures)
                    {
                        AppManager.Images.Add(tx);
                    }

                    show_Images.set();
                }

                // TODO - implement popup update here
                Debug.Log("Textures count to open: " + textures.Length);
            }
        }
    }

    public void Dispose()
    {
        _data = null;

        // set up the scroller delegates
        scroller.Delegate = null;
        scroller.scrollerScrolled = null;
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
        GraphScruct data = _data[dataIndex];

        // define what data to provide
        if (_mode == AppManager.Mode.SAA)
        {
            // show week text only for the 1st day of the week (Sunday)
            if (data.date.DayOfWeek == DayOfWeek.Sunday)
            {
                // check num of week
                int numOfWeek = Mathf.FloorToInt(data.date.Subtract(firstDate).Days / 7) + 1;

                DateTime periodEndDate = data.date.AddDays(7);

                cellView.SetData(data, false, numOfWeek,
                    new StringBuilder(data.date.FormatToDateMonth()).Append("-").Append(periodEndDate.FormatToDateMonth())
                        .ToString());
            }
            else
            {
                cellView.SetData(data, false);
            }
        }
        else
        {
            cellView.SetData(data, true);
        }


        // return the cell to the scroller
        return cellView;
    }

    #endregion

    #region Tests

    public void NextDay()
    {
        if (_lastMiddleCellIndex + 4 < _data.Count)
        {
            _lastMiddleCellIndex++;
            SetDateRange(_lastMiddleCellIndex);
        }
    }

    public void PrevDay()
    {
        if (_lastMiddleCellIndex - 4 >= 0)
        {
            _lastMiddleCellIndex--;
            SetDateRange(_lastMiddleCellIndex);
        }
    }

    #endregion
}