using System;
using EnhancedUI.EnhancedScroller;
using UnityEngine;
using UnityEngine.UI;

public class DayScrollItemView : EnhancedScrollerCellView
{
    [SerializeField] protected Text _text;
    [SerializeField] protected Color _focusedColor;
    [SerializeField] protected Color _normalColor;

    protected string _initialTextFormat;
    public DateTime data;

    public delegate void OnCellSelected(DayScrollItemView item);

    public OnCellSelected onCellSelected;

    private void Awake()
    {
        _initialTextFormat = _text.text;
    }

    /// <summary>
    /// Set the date to show.
    /// </summary>
    public void SetData(DateTime newData, bool showDay, int numOfWeek = -1, string weekDateRange = null)
    {
        this.data = newData;
        _text.enabled = true;
        SetFocus(false);

        // Debug.Log(
        // $"newData: {newData.ToShortDateString()}, showDay: {showDay}, numOfWeek: {numOfWeek}, weekDateRange: {weekDateRange}");
        if (numOfWeek > -1)
        {
            _text.text = string.Format(_initialTextFormat, "WEEK" + numOfWeek, weekDateRange);
            
            // if that is a last week (current)
            if (DateTime.Today.Subtract(this.data).Days < 8)
            {
                SetFocus(true);
            }
        }
        else if (showDay)
        {
            string date = data.ToString("ddd");
            string day = data.Date.Day.ToString();
            _text.text = string.Format(_initialTextFormat, date, day);
            
            // set focus only for Today
            SetFocus(newData.Date == DateTime.Today.Date);
        }
        else
        {
            _text.enabled = false;
        }
    }

    public void SetFocus(bool focus)
    {
        if (focus)
        {
            _text.color = _focusedColor;
        }
        else
        {
            _text.color = _normalColor;
        }
    }

    public void OnSelected()
    {
        onCellSelected?.Invoke(this);
    }
}