using System.Collections.Generic;
using UnityEngine;
using System;
using App.Data.Reminders;
using EnhancedUI.EnhancedScroller;
using MaterialUI;
using QuickEngine.Extensions;
using UnityEngine.UI;

public class CalendarScreen : MonoBehaviour, IEnhancedScrollerDelegate
{
    [SerializeField] private ScreenConfig _screenConfig;
    [SerializeField] private Calendar _calendar;
    [SerializeField] private ReminderScreen _reminderScreen;
    [SerializeField] private Button _addReminderButton;
    private List<ReminderData> _data;

    public EnhancedScroller scroller;
    public EnhancedScrollerCellView cellViewPrefab;

    private void Awake()
    {
        _screenConfig.OnShowStarted += ShowStarted;
    }

    private void Start()
    {
        _calendar.cellClickedDelegate = CellClickedDelegate;
    }

    private void ShowStarted()
    {
        SetCalendar(DateTime.Now.Date);
        
        // set up the scroller delegates
        scroller.Delegate = this;
        scroller.scrollerScrolled = ScrollerScrolled;
        scroller.cellViewInstantiated = CellViewInstantiated;

        SetScrollData(ReminderManager.Instance.GetRemindersByDate(_calendar.focusedDate.Date));
    }

    private void SetCalendar(DateTime date)
    {
        // update selected date
        _calendar.focusedDate = _calendar.selectedDate = date;
    }

    private void CellClickedDelegate(PickerCell cell)
    {
        // don't allow to add new reminder to past dates
        if (cell.dateTime.Date.IsOlderDate(DateTime.Now.Date))
        {
            _addReminderButton.gameObject.SetActive(false);
        }
        else
        {
            _addReminderButton.gameObject.SetActive(true);
        }
        
        SetScrollData(cell.reminders);
    }

    public void SetScrollData(List<ReminderData> data)
    {
        Debug.Log("Found data count: "+data.Count);
        _data = data;
        scroller.ReloadData();
    }

    public void AddRemainder()
    {
        ScreenManager.Instance.Set(10);
        
        // show reminder screen with predefined (selected date). IMPORTANT to call it after screen set to let OnShowStart event reset default date value
        _reminderScreen.SetDefaultDate(_calendar.focusedDate.Date);
    }
    
    private void OnReminderItemClicked(DetailedReminderUiItem item)
    {
        // open Reminder screen
        ScreenManager.Instance.Set(10);
    }

    #region EnhancedScroller Handlers

    private void CellViewInstantiated(EnhancedScroller enhancedScroller, EnhancedScrollerCellView cellview)
    {
        DetailedReminderUiItem item = cellview as DetailedReminderUiItem;
        item.onReminderItemClicked = OnReminderItemClicked;
    }

    private void ScrollerScrolled(EnhancedScroller enhancedScroller, Vector2 val, float scrollposition)
    {
    }

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
        return 160;
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
        DetailedReminderUiItem cellView = scroller.GetCellView(cellViewPrefab) as DetailedReminderUiItem;

        // set the name of the game object to the cell's data index.
        // this is optional, but it helps up debug the objects in 
        // the scene hierarchy.
        cellView.name = "Cell " + dataIndex.ToString();

        // update data and view
        cellView.SetData(_data[dataIndex], _calendar.focusedDate.Date);

        // return the cell to the scroller
        return cellView;
    }

    #endregion
}