using System;
using System.Collections.Generic;
using App.Data.Reminders;
using QuickEngine.Extensions;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class CalendarUiItem : MonoBehaviour
{
    [SerializeField] protected Text _text;
    [SerializeField] protected Color _normalTextColor, _selectedTextColor;
    [SerializeField] protected GameObject _todayViewObject;
    [SerializeField] protected GameObject _selectedViewObject;

    protected bool IsSelected => toggle.isOn;
    
    public CalendarData data;
    public Toggle toggle;
    public List<NotificationData> notifications = new List<NotificationData>();

    private void Awake()
    {
        if (toggle == null)
        {
            toggle = GetComponent<Toggle>();
        }

        toggle.onValueChanged.AddListener(OnToggled);
    }

    public void SetData(CalendarData data)
    {
        this.data = data;
        
        _todayViewObject.SetActive(data.date.IsToday());
    }

    public void OnToggled(bool isOn)
    {
        
    }

    public void UpdateView(bool selected)
    {
        if (selected)
        {
            _selectedViewObject.SetActive(true);
            _text.color = _selectedTextColor;
        }
        else
        {
            _selectedViewObject.SetActive(false);
            _text.color = _normalTextColor;
        }
    }
}