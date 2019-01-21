using System;
using App.Data.Reminders;
using EnhancedUI.EnhancedScroller;
using UnityEngine;
using UnityEngine.UI;

public class ReminderUiItem : EnhancedScrollerCellView, IDisposable
{
    [SerializeField] protected Text _titleText;
    [SerializeField] protected Image _bgImage;
    private bool _wasSelected;

    public Toggle toggle;
    public ReminderData data;
    public event Action<ReminderUiItem, bool> OnToggled;

    protected void Awake()
    {
        toggle.onValueChanged.AddListener(OnToggleValueChanged);
        _wasSelected = toggle.isOn;
    }

    public void SetData(ReminderData data)
    {
        Dispose();

        this.data = data;

        UpdateView(this.data);
        data.OnDataUpdate += UpdateView;
    }

    public void Dispose()
    {
        if (data != null)
        {
            data.OnDataUpdate -= UpdateView;
            data = null;
        }
    }

    protected virtual void UpdateView(ReminderData reminderData)
    {
        _titleText.text = data.title;
        
        // tint gray a bit to show that reminder is not active
        _bgImage.color = data.isActive ? Color.white : Color.gray;
    }

    protected void OnToggleValueChanged(bool isOn)
    {
        // Debug.Log("OnToggleValueChanged: "+isOn);
        
        // if was toggle off by multiple click
        if (_wasSelected && !isOn || isOn)
        {
            OnToggled?.Invoke(this, isOn);
        }
        _wasSelected = isOn;
    }
}