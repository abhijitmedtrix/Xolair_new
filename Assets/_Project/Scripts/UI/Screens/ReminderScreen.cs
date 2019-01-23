using System;
using System.Collections.Generic;
using System.Globalization;
using App.Data.Reminders;
using DoozyUI;
using EnhancedUI.EnhancedScroller;
using MaterialUI;
using QuickEngine.Extensions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ReminderScreen : MonoBehaviour, IEnhancedScrollerDelegate
{
    private string[] repeatOptionsNames =
    {
        "Does not repeat",
        "Every day",
        "Every week",
        "Every fortnight",
        "Every month",
        "Every year",
        "Custom...",
        "Cancel"
    };

    [SerializeField] private ToggleSlider _toggleSlider;
    [SerializeField] private ScreenConfig _screenConfig;
    [SerializeField] private ToggleGroup _toggleGroup;
    [SerializeField] private Button _saveButton, _editButton, _deleteButton;
    [SerializeField] private InputField _titleInputField;
    [SerializeField] private Text _repeatText, _dateText, _timeText;
    [SerializeField] private Calendar _calendar;
    [SerializeField] private GameObject _calendarPopup;
    [SerializeField] private CustomReminderScreen _customReminderScreen;

    private Rect _nativePopupRect;
    private ReminderData _tempNewReminderData;
    private ReminderData _tempReminderData;
    private List<ReminderData> _data;
    private ReminderUiItem _selectedItem;
    private ReminderData _defaultSelection;

    public EnhancedScroller scroller;
    public EnhancedScrollerCellView cellViewPrefab;

    private enum State
    {
        New,
        Edit
    }

    private State state;

    private void Awake()
    {
        _screenConfig.OnShowStarted += ShowStarted;
        _toggleSlider.OnToggleChange += OnReminderToggleSliderValueChanged;
        _titleInputField.onEndEdit.AddListener(OnTitleChanged);

        _tempNewReminderData = ReminderManager.Instance.CreateSimpleTemplateReminder();

        // set picker size for iPad (possible only there)
        _nativePopupRect = new Rect(new Vector2(Screen.width / 2f, Screen.height / 2f),
            new Vector2(Screen.width * 0.25f, Screen.height * 0.3f));
    }

    private void Start()
    {
        // listen reminders update event to update scroller

        // TODO - it's very bad practice to keep listener active all the time. Ideally it shoud be added OnShowStarted, and removed OnHideStarted, but current UI Screen manager doesn't trigger screen hide, it just overlay with a new scren...
        ReminderManager.OnRemindersUpdate += SetScroller;

        _calendar.cellClickedDelegate = CellClickedDelegate;
    }

    private void ShowStarted()
    {
        _customReminderScreen.gameObject.SetActive(false);
        _tempNewReminderData.fireDate = DateTime.Today.Date + _tempNewReminderData.fireDate.TimeOfDay;

        // initialize all reminders
        SetScroller(ReminderManager.Instance.GetAllReminders());

        // of there is such reminder
        int index = _data.IndexOf(_defaultSelection);
        if (index > -1)
        {
            scroller.JumpToDataIndex(index);
            _selectedItem = scroller.GetCellViewAtDataIndex(index) as ReminderUiItem;
            SetState(State.Edit);
        }
        else
        {
            // set New state by default
            SetState(State.New);
        }
    }

    private void SetState(State state)
    {
        this.state = state;

        _editButton.gameObject.SetActive(false);
        _deleteButton.gameObject.SetActive(false);

        if (this.state == State.New)
        {
            _tempReminderData = _tempNewReminderData;
            _toggleSlider.gameObject.SetActive(false);
        }
        else if (this.state == State.Edit)
        {
            _tempReminderData = new ReminderData(_selectedItem.data);
            _toggleSlider.gameObject.SetActive(true);
            _toggleSlider.SetActive(_tempReminderData.isActive);

            // we can remove only custom reminders
            _deleteButton.gameObject.SetActive(!_tempReminderData.isDefault);
        }

        _titleInputField.interactable = !_tempReminderData.isDefault;
        UpdateView();
    }

    private void UpdateView()
    {
        _dateText.text = _tempReminderData.fireDate.ToString("ddd, MMM d, yyyy");
        _timeText.text = _tempReminderData.fireDate.ToString("hh:mm tt", CultureInfo.InvariantCulture);
        _titleInputField.text = _tempReminderData.title;
        _repeatText.text = repeatOptionsNames[(int) _tempReminderData.repeatInterval];
    }

    /// <summary>
    /// Method to make a default selection 
    /// </summary>
    /// <param name="reminderData"></param>
    public void SetSelection(ReminderData reminderData)
    {
        _defaultSelection = reminderData;
    }

    public void SetScroller(List<ReminderData> data)
    {
        // set up the scroller delegates
        scroller.Delegate = this;
        scroller.scrollerScrolled = ScrollerScrolled;
        scroller.cellViewInstantiated = CellViewInstantiated;

        // set scroller data
        SetData(data);
    }

    public void SetDefaultDate(DateTime date)
    {
        _tempReminderData.fireDate = date.Date + _tempReminderData.fireDate.TimeOfDay;
        UpdateView();
    }

    public void SetData(List<ReminderData> datas)
    {
        _data = datas;
        Debug.Log("Data count: " + _data.Count);
        scroller.ReloadData();
    }

    public void PickTime()
    {
        Debug.Log("Pick native picker for repeat interval");

        if (Application.isEditor)
        {
            DateTime dateTime = _tempReminderData.fireDate.AddMinutes(10);
            _tempReminderData.fireDate = _tempReminderData.fireDate.Date + dateTime.TimeOfDay;
            UpdateView();
        }
        else
        {
            NativePicker.Instance.ShowTimePicker(_nativePopupRect, DateTime.Now,
                val =>
                {
                    DateTime dateTime = NativePicker.ConvertToDateTime(val);
                    _tempReminderData.fireDate = _tempReminderData.fireDate.Date + dateTime.TimeOfDay;
                    UpdateView();
                },
                () =>
                {
                    // do nothing
                }
            );
        }
    }

    public void PickDate()
    {
        Debug.Log("Pick native picker for repeat interval");

        ControlCalendarPopup(true);
    }

    public void PickRepeatInterval()
    {
        if (_tempReminderData.isDefault) return;

        Debug.Log("Pick native picker for repeat interval");
        if (Application.isEditor)
        {
            // set some random interval
            // RepeatIntervalPicked((long) Random.Range(0, repeatOptionsNames.Length - 1));
            // RepeatIntervalPicked((long) 0);
            RepeatIntervalPicked((long) 6);
        }
        else
        {
            NativePicker.Instance.ShowCustomPicker(_nativePopupRect, repeatOptionsNames,
                (int) _tempReminderData.repeatInterval,
                RepeatIntervalPicked, null);
        }
    }

    public void SaveReminder()
    {
        // check some critical values set by user before saving
        if (string.IsNullOrEmpty(_tempReminderData.title)) return;


        // we can't set new notification without repetition in a past
        if (_tempReminderData.repeatInterval == RepeatInterval.ONCE &&
            _tempReminderData.fireDate.IsOlderDate(DateTime.Now))
        {
            // show error
            UIManager.NotificationManager.ShowNotification(
                "OneOptionTitleUINotification",
                -1,
                false,
                "Sorry!",
                "Reminders can only be set for a future date and time.",
                null,
                new string[] {"Ok"},
                new string[] {"Thanks!"}
            );
            return;
        }


        if (this.state == State.New)
        {
            _tempReminderData.SetupReminder();
            ReminderManager.Instance.AddReminder(_tempReminderData);

            // update variables and create new reminder
            _tempReminderData = _tempNewReminderData = ReminderManager.Instance.CreateSimpleTemplateReminder();
        }
        else if (this.state == State.Edit)
        {
            // check, did user change something at all or not
            if (!_selectedItem.data.Equals(_tempReminderData))
            {
                UIManager.NotificationManager.ShowNotification(
                    "TwoOptionsTitleUINotification",
                    -1,
                    false,
                    "Save changes?",
                    "Are you sure you wish to save changes?",
                    null,
                    new string[] {"No", "Yes"},
                    new string[] {"No", "Yes"},
                    new UnityAction[]
                    {
                        null,
                        () =>
                        {
                            _selectedItem.data.CopyData(_tempReminderData);
                            _selectedItem.data.SetupReminder();

                            _tempReminderData = null;
                            // deselect all toggles
                            _toggleGroup.SetAllTogglesOff();

                            // switch to new reminder
                            SetState(State.New);
                        }
                    }
                );
            }
            else
            {
                _tempReminderData = null;

                // deselect all toggles
                _toggleGroup.SetAllTogglesOff();

                // switch to new reminder
                SetState(State.New);
            }
        }
    }

    public void EditReminder()
    {
    }

    public void DeleteReminder()
    {
        UIManager.NotificationManager.ShowNotification(
            "TwoOptionsTitleUINotification",
            -1,
            false,
            "Delete?",
            "You can't restore reminder after it's deleted. Are you sure you wish to proceed?",
            null,
            new string[] {"No", "Yes"},
            new string[] {"No", "Yes"},
            new UnityAction[]
            {
                null,
                () =>
                {
                    ReminderManager.Instance.DeleteReminder(_tempReminderData.id);
                    _tempReminderData = null;

                    // deselect all toggles
                    _toggleGroup.SetAllTogglesOff();

                    // switch to new reminder
                    SetState(State.New);
                }
            }
        );
    }

    private void OnReminderToggleSliderValueChanged(bool isOn)
    {
        // _tempReminderData.isActive = isOn;
        _tempReminderData.SetActive(isOn);
    }

    private void OnTitleChanged(string str)
    {
        _tempReminderData.title = str;
        UpdateView();
    }

    private void RepeatIntervalPicked(long value)
    {
        int index = (int) value;

        Debug.Log($"Option picked: {value}, it's: {repeatOptionsNames[index]}");

        // Custom... option
        if (index == repeatOptionsNames.Length - 2)
        {
            // load current reminder data to setup
            _customReminderScreen.SetReminderData(_tempReminderData);

            // open reminder customize screen
            _customReminderScreen.gameObject.SetActive(true);
            // ScreenManager.Instance.Set(14);
        }
        // Cancel option
        else if (index == repeatOptionsNames.Length - 1)
        {
            // do nothing for now
        }
        else
        {
            _tempReminderData.repeatInterval = (RepeatInterval) index;
            UpdateView();
        }
    }

    private void ItemOnToggled(ReminderUiItem item, bool isOn)
    {
        if (isOn)
        {
            _selectedItem = item;
            SetState(State.Edit);
        }
        else
        {
            _selectedItem = null;
            SetState(State.New);
        }
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
            _calendar.focusedDate = _calendar.selectedDate = _tempReminderData.fireDate.Date;
        }
        else
        {
            // if some date been picked and it's differ to previous out
            if (!_tempReminderData.fireDate.IsSameDay(_calendar.selectedDate))
            {
                _tempReminderData.fireDate = _calendar.selectedDate.Date + _tempReminderData.fireDate.TimeOfDay;
                UpdateView();
            }
        }
    }

    public void Back()
    {
        _defaultSelection = null;
        _selectedItem = null;
        _tempReminderData = null;
        ScreenManager.Instance.Back();
    }

    public void HideCustomReminderScreen()
    {
        _customReminderScreen.gameObject.SetActive(false);
        UpdateView();
    }

    #region EnhancedScroller Handlers

    private void CellViewInstantiated(EnhancedScroller enhancedScroller, EnhancedScrollerCellView cellview)
    {
        ReminderUiItem item = cellview as ReminderUiItem;
        item.OnToggled += ItemOnToggled;
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
        return 200;
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
        ReminderUiItem cellView = scroller.GetCellView(cellViewPrefab) as ReminderUiItem;
        cellView.toggle.group = _toggleGroup;

        // set the name of the game object to the cell's data index.
        // this is optional, but it helps up debug the objects in 
        // the scene hierarchy.
        cellView.name = "Cell " + dataIndex.ToString();

        // update data and view
        cellView.SetData(_data[dataIndex]);

        // return the cell to the scroller
        return cellView;
    }

    #endregion
}