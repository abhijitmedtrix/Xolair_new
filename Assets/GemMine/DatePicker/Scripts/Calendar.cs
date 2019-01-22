using UnityEngine;
using UnityEngine.UI;
using System;

public class Calendar : MonoBehaviour
{
    private DatePickerLayout[] _panels;

    public DatePickerLayout dayPanel;
    public DatePickerLayout monthPanel;
    public DatePickerLayout yearPanel;
    public DatePickerLayout weekdayPanel;

    public PickerTweens year;
    public PickerTweens month;
    public PickerTweens day;
    public PickerTweens completePanel;

    public GameObject NavPanel;
    public GameObject TopPanel;

    public Text NavPanelInfoText;
    public Text SelectedDateText;

    public bool startCalOpen;

    public Sprite actualMonthImg;
    public Sprite otherMonthImg;
    public Sprite currentMonthImg;
    public Sprite navBarImg;
    public Color fontActiveColor;
    public Color fontInactiveColor;
    public Color fontSelectedColor;
    public Color spacingColor;

    public int horizSpacing, vertSpacing;

    public bool zoomAnimation;
    public bool hideAnimation;
    public bool allowPastSelection;

    private DateTime _focusedDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

    public enum ActivePanel
    {
        Year,
        Month,
        Day
    }

    public ActivePanel activePanel;

    public DateTime selectedDate;
    public DateTime focusedDate
    {
        get { return _focusedDate; }
        set
        {
            _focusedDate = value;
            SetupCells();
        }
    }

    public PickerCell.CellClicked cellClickedDelegate;

    private void Awake()
    {
        _panels = new[] {yearPanel, monthPanel, weekdayPanel, dayPanel};
    }

    // Use this for initialization
    private void Start()
    {
        if (!startCalOpen)
            completePanel.startShrunk();

        // set default view
        if (activePanel == ActivePanel.Day)
        {
            yearPanel.gameObject.SetActive(false);
            monthPanel.gameObject.SetActive(false);
        }
        else if (activePanel == ActivePanel.Month)
        {
            yearPanel.gameObject.SetActive(false);
        }

        SetTheme();

        // yearPanel.createCells("Year");
        // monthPanel.createCells("Month");
        dayPanel.CreateCells("Day");
        weekdayPanel.CreateCells("WeekDay");

        SetupCells();
        SetNavPanel();
        SetSelectedDateText();

        dayPanel.cellClickedDelegate = OnDaySelected;
    }

    private void OnDaySelected(PickerCell cell)
    {
        // NOTE - important to keep this order
        selectedDate = new DateTime(focusedDate.Year, focusedDate.Month, cell.dateTime.Day);
        focusedDate = selectedDate;
        
        // Debug.Log($"selectedDate: {selectedDate}");
        cellClickedDelegate?.Invoke(cell);
    }
    
    public void SetTheme()
    {
        for (int i = 0; i < _panels.Length; i++)
        {
            _panels[i].SetSprites(currentMonthImg, actualMonthImg, otherMonthImg);
            _panels[i].SetFontColors(fontActiveColor, fontInactiveColor, fontSelectedColor);
            _panels[i].SetSpacing(horizSpacing, vertSpacing, spacingColor);
        }
    }

    public void Shrink()
    {
        if (completePanel.canShrink())
            completePanel.ShrinkIn(hideAnimation);
        else
            completePanel.ShrinkOut(hideAnimation);
    }

    public void ZoomOut()
    {
        if (!completePanel.canShrink())
            return;

        switch (activePanel)
        {
            case ActivePanel.Year:
                year.ZoomOut(zoomAnimation);
                activePanel++;
                break;
            case ActivePanel.Month:
                month.ZoomOut(zoomAnimation);
                activePanel++;
                break;
            case ActivePanel.Day:
                break;
        }

        SetupCells();
        SetSelectedDateText();
        SetNavPanel();
    }

    public void ZoomIn()
    {
        if (!completePanel.canShrink())
            return;

        switch (activePanel)
        {
            case ActivePanel.Year:
                break;
            case ActivePanel.Month:
                year.ZoomIn(zoomAnimation);
                activePanel--;
                break;
            case ActivePanel.Day:
                month.ZoomIn(zoomAnimation);
                activePanel--;
                break;
        }

        SetupCells();
        SetNavPanel();
        SetSelectedDateText();
    }

    private void SetupCells()
    {
        for (int i = 0; i < _panels.Length; i++)
        {
            _panels[i].SetupCells(_focusedDate);
        }
    }

    private void SetNavPanel()
    {
        switch (activePanel)
        {
            case ActivePanel.Year:
                NavPanelInfoText.text = yearPanel.GetInfo(_focusedDate);
                break;
            case ActivePanel.Month:
                NavPanelInfoText.text = monthPanel.GetInfo(_focusedDate);
                break;
            case ActivePanel.Day:
                NavPanelInfoText.text = dayPanel.GetInfo(_focusedDate);
                break;
        }

        /*
        NavPanelInfoText.color = fontActiveColor;
        NavPanel.GetComponent<Image>().sprite = navBarImg;
        NavPanel.GetComponent<Image>().color = Color.white;
        NavPanel.transform.Find("ButtonBack").Find("Image").GetComponent<Image>().color = fontActiveColor;
        NavPanel.transform.Find("ButtonForward").Find("Image").GetComponent<Image>().color = fontActiveColor;
        */
    }

    private void SetSelectedDateText()
    {
        SelectedDateText.text = _focusedDate.ToString("D");
        SelectedDateText.color = fontActiveColor;
    }

    public void previousButtonClicked()
    {
        changeDate(-1);
    }

    public void nextButtonClicked()
    {
        changeDate(1);
    }

    public void changeDate(int direction)
    {
        switch (activePanel)
        {
            case ActivePanel.Day:
                _focusedDate = new DateTime(
                    _focusedDate.AddMonths(direction).Year,
                    _focusedDate.AddMonths(direction).Month,
                    _focusedDate.AddMonths(direction).Day);
                break;
            case ActivePanel.Month:
                _focusedDate = new DateTime(
                    _focusedDate.AddYears(direction).Year,
                    _focusedDate.AddYears(direction).Month,
                    _focusedDate.AddYears(direction).Day);
                break;
            case ActivePanel.Year:
                int numberOfCells = yearPanel.GetNumberOfCells();
                _focusedDate = new DateTime(
                    _focusedDate.AddYears(direction * numberOfCells).Year,
                    _focusedDate.AddYears(direction * numberOfCells).Month,
                    _focusedDate.AddYears(direction * numberOfCells).Day);
                break;
        }

        SetupCells();
        SetNavPanel();
        SetSelectedDateText();
    }
}