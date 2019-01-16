using System;
using App.Data.CSU;
using EnhancedUI.EnhancedScroller;
using UnityEngine;
using UnityEngine.UI;

public class DayScrollItemView : EnhancedScrollerCellView
{
    [SerializeField] protected Text _text;
    [SerializeField] protected Color _focusedColor;
    [SerializeField] protected Color _normalColor;
    [SerializeField] protected RectTransform _point;
    [SerializeField] protected RectTransform _pointContainer;

    protected string _initialTextFormat;
    public PatientJournalScreen.GraphData graphData;

    public delegate void OnCellSelected(DayScrollItemView item);

    public OnCellSelected onCellSelected;

    private void Awake()
    {
        _initialTextFormat = _text.text;
    }

    /// <summary>
    /// Set the date to show.
    /// </summary>
    public void SetData(PatientJournalScreen.GraphData graphData, bool showDay, int numOfWeek = -1,
        string weekDateRange = null)
    {
        this.graphData = graphData;

        SetFocus(false);
        _text.enabled = true;
        DateTime dt = this.graphData.date;

        // Debug.Log(
        // $"newData: {newData.date.ToShortDateString()}, showDay: {showDay}, numOfWeek: {numOfWeek}, weekDateRange: {weekDateRange}");

        if (numOfWeek > -1)
        {
            _text.text = string.Format(_initialTextFormat, "WEEK" + numOfWeek, weekDateRange);
        }
        else if (showDay)
        {
            string date = dt.Date.Day.ToString();
            string day = dt.ToString("ddd").ToUpper();
            _text.text = string.Format(_initialTextFormat, date, day);

            // set focus only for Today
            SetFocus(dt.Date == DateTime.Today.Date);
        }
        else
        {
            _text.enabled = false;
        }

        Vector2 anchor = new Vector2(0.5f, this.graphData.interpolatedScore / this.graphData.maxScore);
        _point.anchorMin = _point.anchorMax = anchor;
        _point.anchoredPosition = Vector2.zero;

        // we don't show the points for CSU tracker cells
        if (graphData.data == null || graphData.data is CSUData)
        {
            _point.gameObject.SetActive(false);
        }
        else
        {
            _point.gameObject.SetActive(true);
        }
    }

    public void SetFocus(bool focus)
    {
        if (focus)
        {
            _text.color = _focusedColor;
            _text.fontStyle = FontStyle.Bold;
        }
        else
        {
            _text.color = _normalColor;
            _text.fontStyle = FontStyle.Normal;
        }
    }

    public void OnSelected()
    {
        onCellSelected?.Invoke(this);
    }
}