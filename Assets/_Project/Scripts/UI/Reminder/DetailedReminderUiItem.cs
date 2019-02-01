using System;
using App.Data.Reminders;
using EnhancedUI.EnhancedScroller;
using UnityEngine;
using UnityEngine.UI;

public class DetailedReminderUiItem : EnhancedScrollerCellView, IDisposable
{
    [SerializeField] protected Text _titleText;
    [SerializeField] protected Text _dateText;
    [SerializeField] protected Text _timeText;
    [SerializeField] protected Image _bgImage;
    [SerializeField] protected Button _button;
    [SerializeField] protected ToggleSlider _toggleSlider;
    [SerializeField] private Color _inactiveColor;
    [SerializeField] private Color _deletedColor;

    private string _initialDateTextFormat;

    public OnReminderItemClicked onReminderItemClicked;

    public delegate void OnReminderItemClicked(DetailedReminderUiItem item);

    public ReminderData data;

    protected void Awake()
    {
        _initialDateTextFormat = _dateText.text;
        _toggleSlider.OnToggleChange += ToggleSliderOnToggleChanged;
    }

    public void SetData(ReminderData data, DateTime currentData)
    {
        Debug.Log($"Set data for detailed reminder. Title: {data.title}, Data: {currentData}");
        
        Dispose();

        this.data = data;

        UpdateView(this.data);
        data.OnDataUpdate += UpdateView;

        // show as <Wed 30>
        _dateText.text = string.Format(_initialDateTextFormat, currentData.ToString("ddd"), currentData.Day.ToString());
    }

    public void Dispose()
    {
        if (data != null)
        {
            data.OnDataUpdate -= UpdateView;
            data = null;
        }
    }

    protected void UpdateView(ReminderData reminderData)
    {
        _titleText.text = data.title;
        _timeText.text = data.fireDate.ToString("h:mm tt");
        
        // tint gray a bit to show that reminder is not active
        if (data.isActive)
        {
            _bgImage.color = Color.white;
        }
        else
        {
            if (data.isDeleted)
            {
                _bgImage.color = _deletedColor;
            }
            else
            {
                _bgImage.color = _inactiveColor;
            }
        }

        // don't allow to select deleted reminder
        _button.interactable = !data.isDeleted;
        _toggleSlider.SetActive(data.isDone);
        _toggleSlider.SetInteractive(!data.isDeleted);
    }

    private void ToggleSliderOnToggleChanged(bool isOn)
    {
        data.SetDone(isOn);
    }

    public void OnSelected()
    {
        onReminderItemClicked?.Invoke(this);
    }
}