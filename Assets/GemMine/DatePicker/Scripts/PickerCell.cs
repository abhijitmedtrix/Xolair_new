using System;
using System.Collections.Generic;
using App.Data.Reminders;
using QuickEngine.Extensions;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class PickerCell : MonoBehaviour
{
    public enum CellState
    {
        Inactive,
        Active,
        Selected
    }

    [SerializeField] private GameObject[] _remindersIcons;
    [SerializeField] private Image _selectedBgImage;
    [SerializeField] private GameObject _remindersIconsContainer;

    public DateTime dateTime = DateTime.MinValue;
    public Image image;
    public Text text;
    public Button button;
    public CellState state;
    public CellClicked cellClickedDelegate;

    [HideInInspector] public List<ReminderData> reminders;

    public delegate void CellClicked(PickerCell cell);

    public void ClickHandler()
    {
        cellClickedDelegate?.Invoke(this);
    }

    public void SetDate(DateTime dateTime, CellState state, List<ReminderData> reminders)
    {
        this.reminders = reminders;
        this.state = state;
        this.dateTime = dateTime;
        
        UpdateView();
    }

    private void UpdateView()
    {
        _selectedBgImage.enabled = false;
        _remindersIconsContainer.SetActive(false);

        if (state == CellState.Inactive)
        {
            text.fontStyle = FontStyle.Normal;
            _remindersIconsContainer.SetActive(true);
        }
        else if (state == CellState.Active)
        {
            text.fontStyle = FontStyle.Bold;
            _remindersIconsContainer.SetActive(true);
        }
        else if (state == CellState.Selected)
        {
            text.fontStyle = FontStyle.Bold;
            _selectedBgImage.enabled = true;
        }
        
        for (int i = 0; i < _remindersIcons.Length; i++)
        {
            _remindersIcons[i].SetActive(i < this.reminders.Count);
        }
    }
}