using System;
using EnhancedUI.EnhancedScroller;
using UnityEngine;
using UnityEngine.UI;

public class CalendarScrollItemView : EnhancedScrollerCellView
{
    [SerializeField] protected Text _text;
    [SerializeField] protected Color _focusedColor;
    [SerializeField] protected Color _normalColor;
    [SerializeField] protected float _focusedSpacing;
    [SerializeField] protected float _normalSpacing;
    [SerializeField] protected int _focusedFontSize;
    [SerializeField] protected int _normalFontSize;
    [SerializeField] protected Font _focusedFont;
    [SerializeField] protected Font _normalFont;

    public DateTime data;
    
    public delegate void OnCellSelected(CalendarScrollItemView item);

    public OnCellSelected onCellSelected;
    
    public void SetData(DateTime newData)
    {
        this.data = newData;
        _text.text = $"{data.ToString("ddd")}\n{data.Date.Day}";
        
        // set focus only for Today
        SetFocus(newData.Date == DateTime.Today.Date);
    }

    public void SetFocus(bool focus)
    {
        if (focus)
        {
            _text.color = _focusedColor;
            _text.lineSpacing = _focusedSpacing;
            _text.fontSize = _focusedFontSize;
            _text.font = _focusedFont;
        }
        else
        {
            _text.color = _normalColor;
            _text.lineSpacing = _normalSpacing;
            _text.fontSize = _normalFontSize;
            _text.font = _normalFont;
        }
    }

    public void OnSelected()
    {
        onCellSelected?.Invoke(this);
    }
}