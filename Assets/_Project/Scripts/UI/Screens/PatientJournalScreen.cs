using System;
using System.Collections.Generic;
using App.Data.CSU;
using EnhancedUI.EnhancedScroller;
using MaterialUI;
using UnityEngine;
using UnityEngine.UI;

public class PatientJournalScreen : MonoBehaviour, IEnhancedScrollerDelegate
{
    [SerializeField] protected TrackerManager.TrackerType _trackerType;
    [SerializeField] protected GraphRenderer _graphRenderer;
    [SerializeField] protected ScreenConfig _screenConfig;
    [SerializeField] protected RawImage _graphImage;
    [SerializeField] protected RectTransform _graphContainer;
    [SerializeField] protected RectTransform _scrollerRectTransform;
    [SerializeField] protected RectTransform _graphPoint;

    [Header("Hints")] [SerializeField] protected Hint _scoreHint;
    [SerializeField] protected Hint _photoHint;

    protected int _lastMiddleCellIndex;
    protected int _daysToShow = 7;
    protected int[] _scoreByDaysRange = new int[7];

    /// <summary>
    /// In this example we are going to use a standard generic List. We could have used
    /// a SmallList for efficiency, but this is just a demonstration that other list
    /// types can be used.
    /// </summary>
    private List<DateTime> _data;

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
        _screenConfig.OnShowStarted += OnShowStarted;
    }

    private void Start()
    {
        // set up the scroller delegates
        scroller.Delegate = this;
        OnShowStarted();
        scroller.scrollerScrolled = ScrollerScrolled;
    }

    private void OnShowStarted()
    {
        List<DateTime> maxDateRange = TrackerManager.GetMaxDateRange();
        SetData(maxDateRange);

        Debug.Log("Max date range to show: " + maxDateRange.Count);
        Debug.Log("Last date: " + maxDateRange[maxDateRange.Count - 1]);

        int maxScore;
        if (_trackerType == TrackerManager.TrackerType.CSU || _trackerType == TrackerManager.TrackerType.UAS)
        {
            maxScore = TrackerManager.GetMaxScore(TrackerManager.TrackerType.CSU) +
                       TrackerManager.GetMaxScore(TrackerManager.TrackerType.UAS);

            _photoHint.gameObject.SetActive(true);
        }
        else
        {
            maxScore = TrackerManager.GetMaxScore(TrackerManager.TrackerType.Asthma) +
                       TrackerManager.GetMaxScore(TrackerManager.TrackerType.Symptom);

            _photoHint.gameObject.SetActive(false);
        }

        _graphRenderer.Init(maxScore, _daysToShow);
        _graphImage.texture = _graphRenderer.GetRenderTexture();

        // update graph by middle index
        SetDateRange(maxDateRange.Count - 4);

        // scroll to the end
        scroller.JumpToDataIndex(maxDateRange.Count - 1);
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

    public void SetData(List<DateTime> datas)
    {
        _data = datas;

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

    private void SetDateRange(int middleIndex)
    {
        // get score points for current period
        int counter = 0;
        for (int i = middleIndex - 3; i < middleIndex + 4; i++)
        {
            if (_trackerType == TrackerManager.TrackerType.CSU || _trackerType == TrackerManager.TrackerType.UAS)
            {
                _scoreByDaysRange[counter] = TrackerManager.GetScore(_data[i], TrackerManager.TrackerType.CSU)
                                             +
                                             TrackerManager.GetScore(_data[i], TrackerManager.TrackerType.UAS);
            }
            else
            {
                _scoreByDaysRange[counter] = TrackerManager.GetScore(_data[i], TrackerManager.TrackerType.Asthma)
                                             +
                                             TrackerManager.GetScore(_data[i], TrackerManager.TrackerType.Symptom);
            }

            counter++;
        }
        
        Debug.Log($"Date range defined. Min: {_data[middleIndex - 3].ToShortDateString()} and Max: {_data[middleIndex + 3].ToShortDateString()}");

        // update graph mesh
        _graphRenderer.UpdateGraph(_scoreByDaysRange);

        // transform render texture (graph camera) point position to UI graph
        Vector3 viewportPos = _graphRenderer.GetLastDayPointViewportPosition();
        Vector3 localPos = new Vector3(_graphContainer.rect.width * _graphContainer.pivot.x,
            _graphContainer.rect.height * viewportPos.y, 0);

        // set point
        _graphPoint.localPosition = localPos;

        // update score hint
        _scoreHint.UpdateValue(_scoreByDaysRange[_scoreByDaysRange.Length - 1].ToString());

        // update photo hint if needed
        if (_trackerType == TrackerManager.TrackerType.CSU || _trackerType == TrackerManager.TrackerType.UAS)
        {
            CSUData csuData =
                TrackerManager.GetData(_data[_lastMiddleCellIndex + 3], TrackerManager.TrackerType.CSU) as CSUData;
            // if data exists
            if (csuData != null)
            {
                int photosCount = csuData.GetPhotosCount();

                // don't show it if it's 0, there will be a UI positioning issue + it's not needed at all 
                _photoHint.gameObject.SetActive(photosCount != 0);

                // update photos count text
                _photoHint.UpdateValue(photosCount.ToString());

                // set it to the left of point 
                _photoHint.rectTransform.localPosition = _graphPoint.localPosition - Vector3.right * 100;
            }
            else
            {
                _photoHint.gameObject.SetActive(false);
            }
        }
    }

    public void ShowPhotosPopup()
    {
        // update photo hint if needed
        if (_trackerType == TrackerManager.TrackerType.CSU || _trackerType == TrackerManager.TrackerType.UAS)
        {
            CSUData csuData =
                TrackerManager.GetData(_data[_lastMiddleCellIndex + 3], TrackerManager.TrackerType.CSU) as CSUData;
            // if data exists
            if (csuData != null)
            {
                Texture2D[] textures = csuData.GetAllPhotos();

                // TODO - implement popup update here
                Debug.Log("Textures count to open: " + textures.Length);
            }
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
        CalendarScrollItemView cellView = scroller.GetCellView(cellViewPrefab) as CalendarScrollItemView;

        // set the name of the game object to the cell's data index.
        // this is optional, but it helps up debug the objects in 
        // the scene hierarchy.
        cellView.name = "Cell " + dataIndex.ToString();

        // in this example, we just pass the data to our cell's view which will update its UI
        cellView.SetData(_data[dataIndex]);

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