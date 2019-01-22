using System;
using System.Collections.Generic;
using System.Linq;
using App.Data.Reminders;
using MaterialUI;
using QuickEngine.Extensions;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CustomReminderScreen : MonoBehaviour
{
    private enum DatePickState
    {
        None,
        FireDate,
        EndDate
    }

    private string[] repeatOptionsNames =
    {
        "Every day",
        "Every week",
        "Every fortnight",
        "Every month",
        "Every year"
    };

    [SerializeField] private ScreenConfig _screenConfig;
    [SerializeField] protected StateChangeToggle[] _daysOfWeekToggles;
    [SerializeField] protected Toggle[] _endsOnToggles;
    [SerializeField] protected ToggleGroup _endsOnToggleGroup;
    [SerializeField] protected GameObject _daysOfWeekContainer;
    [SerializeField] protected GameObject _calendarDateContainer;
    [SerializeField] protected Calendar _calendar;
    [SerializeField] protected GameObject _calendarPopup;
    [SerializeField] protected InputField _numOfRepeatsInputField;
    [SerializeField] protected Text _endsOnText, _repeatIntervalText, _fireDateText;

    private DatePickState _datePickState;
    private ReminderData _originalReminderData;
    private ReminderData _tempReminderData;
    private Rect _nativePopupRect;
    private List<DayOfWeek> _selectedDays = new List<DayOfWeek>();

    private void Awake()
    {
        _calendar.cellClickedDelegate = CellClickedDelegate;
        _screenConfig.OnShowStarted += ShowStarted;
        _numOfRepeatsInputField.onEndEdit.AddListener(OnNumOfRepaetsEdited);

        for (int i = 0; i < _daysOfWeekToggles.Length; i++)
        {
            _daysOfWeekToggles[i].OnToggleChange += OnDayOfWeekToggled;
        }

        // set picker size for iPad (possible only there)
        _nativePopupRect = new Rect(new Vector2(Screen.width / 2f, Screen.height / 2f),
            new Vector2(Screen.width * 0.25f, Screen.height * 0.3f));
    }

    public void SetReminderData(ReminderData data)
    {
        _originalReminderData = data;
        _tempReminderData = new ReminderData(_originalReminderData);

        // if some days already been selected
        if (_tempReminderData.daysOfWeek != null)
        {
            _selectedDays = _tempReminderData.daysOfWeek.ToList();
        }
        else
        {
            _selectedDays.Clear();
        }
    }

    private void ShowStarted()
    {
        // reset all previous ui settings


        _datePickState = DatePickState.None;

        // deselect days
        foreach (StateChangeToggle stateChangeToggle in _daysOfWeekToggles)
        {
            stateChangeToggle.SetValue(false);
        }

        _selectedDays.Clear();

        // set 1st end on toggle option ON by default
        _endsOnToggles[0].SetValue(true);

        UpdateView();
    }

    private void UpdateView()
    {
        // disable by default
        _daysOfWeekContainer.SetActive(false);
        _calendarDateContainer.SetActive(false);

        if (_tempReminderData.repeatInterval == RepeatInterval.DAY)
        {
        }
        else if (_tempReminderData.repeatInterval == RepeatInterval.WEEK)
        {
            _daysOfWeekContainer.SetActive(true);

            // if some days already been selected before
            if (_selectedDays.Count > 0)
            {
                for (int i = 0; i < _selectedDays.Count; i++)
                {
                    int index = (int) _selectedDays[i];
                    _daysOfWeekToggles[index].SetValue(true);
                }
            }
        }
        else if (_tempReminderData.repeatInterval == RepeatInterval.FORTNIGHT)
        {
            _daysOfWeekContainer.SetActive(true);
        }
        else if (_tempReminderData.repeatInterval == RepeatInterval.MONTH)
        {
            _calendarDateContainer.SetActive(true);
            _fireDateText.text = _tempReminderData.fireDate.ToString("ddd, MMM d, yyyy");
        }
        else if (_tempReminderData.repeatInterval == RepeatInterval.YEAR)
        {
            _calendarDateContainer.SetActive(true);
            _fireDateText.text = _tempReminderData.fireDate.ToString("ddd, MMM d, yyyy");
        }

        _repeatIntervalText.text = _tempReminderData.repeatInterval.ToString().ToTitleCase();


        if (_tempReminderData.endDate == DateTime.MaxValue)
        {
            _endsOnToggles[0].SetValue(true);
        }
        else
        {
            _endsOnToggles[1].SetValue(true);
            _endsOnText.text = _tempReminderData.endDate.ToString("ddd, MMM d, yyyy");
        }
    }

    public void PickFireDate()
    {
        _datePickState = DatePickState.FireDate;
        ControlCalendarPopup(true);
    }

    public void PickEndDate()
    {
        _datePickState = DatePickState.EndDate;
        ControlCalendarPopup(true);
    }

    public void PickRepeatInterval()
    {
        Debug.Log("Pick native picker for repeat interval");
        if (Application.isEditor)
        {
            // set some random interval
            RepeatIntervalPicked((long) Random.Range(0, repeatOptionsNames.Length - 1));
        }
        else
        {
            NativePicker.Instance.ShowCustomPicker(_nativePopupRect, repeatOptionsNames,
                (int) _originalReminderData.repeatInterval,
                RepeatIntervalPicked, null);
        }
    }

    private void RepeatIntervalPicked(long value)
    {
        // add +1 to skip ONCE option
        int index = (int) value + 1;
        Debug.Log($"Option picked: {value}, it's: {repeatOptionsNames[index]}");

        _tempReminderData.repeatInterval = (RepeatInterval) index;
        ;

        UpdateView();
    }

    private void OnDayOfWeekToggled(StateChangeToggle toggle, bool isOn)
    {
        int index = Array.IndexOf(_daysOfWeekToggles, toggle);
        Debug.Log($"Day by index {index} toggled to: {isOn}");

        DayOfWeek dayOfWeek = (DayOfWeek) index;

        if (isOn)
        {
            if (!_selectedDays.Contains(dayOfWeek))
            {
                _selectedDays.Add(dayOfWeek);
            }
        }
        else
        {
            if (_selectedDays.Contains(dayOfWeek))
            {
                _selectedDays.Remove(dayOfWeek);
            }
        }
    }

    private void OnNumOfRepaetsEdited(string val)
    {
        int repetitions;
        if (int.TryParse(val, out repetitions))
        {
            if (repetitions == 0)
            {
                repetitions = 1;
            }
        }
        else
        {
            repetitions = 1;
        }

        // update field view
        _numOfRepeatsInputField.SetValue(repetitions.ToString());
    }

    private void CellClickedDelegate(PickerCell cell)
    {
        ControlCalendarPopup(false);
    }

    public void ControlCalendarPopup(bool enable)
    {
        _calendarPopup.SetActive(enable);

        // we need to set both focus and selected because if Calendar asset limitation - it uses focusedDate everywhere, and it changes on month change (next/prev) without exact date selection
        if (enable)
        {
            if (_datePickState == DatePickState.FireDate)
            {
                _calendar.focusedDate = _calendar.selectedDate = _originalReminderData.fireDate.Date;
            }
            else if (_datePickState == DatePickState.EndDate)
            {
                if (_originalReminderData.endDate != DateTime.MaxValue)
                {
                    _calendar.focusedDate = _calendar.selectedDate = _originalReminderData.endDate.Date;
                }
                else
                {
                    // set a next day by default
                    _calendar.focusedDate = _calendar.selectedDate = _originalReminderData.fireDate.AddDays(1);
                }
            }
        }
        else
        {
            if (_datePickState == DatePickState.FireDate)
            {
                // keep original time
                _tempReminderData.fireDate = _calendar.selectedDate.Date + _tempReminderData.fireDate.TimeOfDay;

                // deselect all days
                for (int i = 0; i < _daysOfWeekToggles.Length; i++)
                {
                    _daysOfWeekToggles[i].SetValue(false);
                }

                // clear list to make correct days selection if user switch to Week or Fortnight 
                _selectedDays.Clear();
            }
            else if (_datePickState == DatePickState.EndDate)
            {
                _tempReminderData.endDate = _calendar.selectedDate.Date +
                                            _tempReminderData.fireDate.AddMinutes(1).TimeOfDay;
            }

            UpdateView();
        }

        // reset state
        _datePickState = DatePickState.None;
    }

    public void Save()
    {
        // define, should we save days of week or not
        if (_tempReminderData.repeatInterval == RepeatInterval.WEEK ||
            _tempReminderData.repeatInterval == RepeatInterval.FORTNIGHT)
        {
            _tempReminderData.daysOfWeek = _selectedDays.ToArray();
        }

        // check, what end date option been selected
        // if last option selected (repeat num of times)
        if (_endsOnToggles[0].isOn)
        {
            _tempReminderData.endDate = DateTime.MaxValue;
        }
        else if (_endsOnToggles[1].isOn)
        {
            // this value already been recorded after calendar picked
        }
        else if (_endsOnToggles[2].isOn)
        {
            // calculate end time for this case
            if (_tempReminderData.repeatInterval == RepeatInterval.DAY)
            {
                _tempReminderData.fireDate.AddDays(int.Parse(_numOfRepeatsInputField.text));
            }
            else if (_tempReminderData.repeatInterval == RepeatInterval.WEEK)
            {
                _tempReminderData.fireDate.AddDays(int.Parse(_numOfRepeatsInputField.text) * 7);
            }
            else if (_tempReminderData.repeatInterval == RepeatInterval.FORTNIGHT)
            {
                _tempReminderData.fireDate.AddDays(int.Parse(_numOfRepeatsInputField.text) * 14);
            }
            else if (_tempReminderData.repeatInterval == RepeatInterval.MONTH)
            {
                _tempReminderData.fireDate.AddMonths(int.Parse(_numOfRepeatsInputField.text));
            }
            else if (_tempReminderData.repeatInterval == RepeatInterval.YEAR)
            {
                _tempReminderData.fireDate.AddYears(int.Parse(_numOfRepeatsInputField.text));
            }
        }

        // write changes to original data and show ReminderScreen. To save reminder and set notifications it should be saved in ReminderScreen 
        _originalReminderData.CopyData(_tempReminderData);

        Back();
    }

    public void Back()
    {
        // currently back is open Reminder screen again
        ScreenManager.Instance.Set(10);
    }
}