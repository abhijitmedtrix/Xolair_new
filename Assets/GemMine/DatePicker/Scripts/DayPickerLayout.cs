using UnityEngine;
using System;
using System.Collections.Generic;
using System.Globalization;
using App.Data.Reminders;
using QuickEngine.Extensions;

public class DayPickerLayout : DatePickerLayout
{
    public override void SetupCells(DateTime now)
    {
        // Debug.Log("Now: " + now);
        
        DateTime firstOfMonth = new DateTime(now.Year, now.Month, 1);
        DateTime firstEntry = firstOfMonth.AddDays(-(int) firstOfMonth.DayOfWeek);

        int i = 0;
        foreach (PickerCell cell in cells)
        {
            cell.text.text = firstEntry.Day.ToString();
            if (firstEntry == now)
            {
                cell.text.color = fontActiveColor;
                cell.image.sprite = currentEntryImg;
            }
            else if (firstEntry.Month != now.Month)
            {
                cell.text.color = fontInactiveColor;
                cell.image.sprite = otherEntryImg;
            }
            else
            {
                cell.text.color = fontActiveColor;
                cell.image.sprite = actualEntryImg;
            }

            // make a selection of active selected day
            PickerCell.CellState state;
            // Debug.Log($"calendar.SelectedDate: {calendar.focusedDate}, selected date: {calendar.selectedDate}, firstEntry.Date: {firstEntry.Date}, IsSameDay: {calendar.focusedDate.IsSameDay(firstEntry.Date)}");
            
            if (calendar.selectedDate.IsSameDay(firstEntry.Date))
            {
                state = PickerCell.CellState.Selected;
                cell.text.color = fontSelectedColor;
            }
            else if (firstEntry.Date.IsOlderDate(DateTime.Now.Date))
            {
                state = PickerCell.CellState.Inactive;
            }
            else
            {
                state = PickerCell.CellState.Active;
            }

            List<ReminderData> reminders = ReminderManager.Instance.GetRemindersByDate(firstEntry, true);
            cell.SetDate(firstEntry, state, reminders);

            firstEntry = firstEntry.AddDays(1);
            i++;
        }
    }

    public override void CellClicked(PickerCell cell)
    {
        DateTime temp;
        
        int clickedIndex = cell.transform.GetSiblingIndex();
        int firstDay = (int) new DateTime(calendar.focusedDate.Year, calendar.focusedDate.Month, 1).DayOfWeek;
        int numberOfDays = DateTime.DaysInMonth(calendar.focusedDate.Year, calendar.focusedDate.Month);
        int numberOfDaysLastMonth = 0;

        if (calendar.focusedDate.Month == 1)
            numberOfDaysLastMonth = DateTime.DaysInMonth(calendar.focusedDate.Year - 1, 12);
        else
            numberOfDaysLastMonth = DateTime.DaysInMonth(calendar.focusedDate.Year, calendar.focusedDate.Month - 1);

        // clicked in the last month
        if (clickedIndex < firstDay)
        {
             temp = new DateTime(
                calendar.focusedDate.AddMonths(-1).Year,
                calendar.focusedDate.AddMonths(-1).Month,
                clickedIndex + numberOfDaysLastMonth - firstDay + 1);
        }
        // clicked in the next month
        else if (clickedIndex > firstDay + numberOfDays - 1)
        {
            temp = new DateTime(
                calendar.focusedDate.AddMonths(1).Year,
                calendar.focusedDate.AddMonths(1).Month,
                clickedIndex - numberOfDays - firstDay + 1);
        }
        // clicked in the current month
        else
        {
            temp = new DateTime(
                calendar.focusedDate.Year,
                calendar.focusedDate.Month,
                clickedIndex - firstDay + 1);
        }

        // don't allow to proceed if past date selected and it's not allowed
        if (temp.Date.IsOlderDate(DateTime.Today) && !calendar.allowPastSelection) return;

        calendar.focusedDate = temp;
        calendar.ZoomOut();
        cellClickedDelegate?.Invoke(cell);
    }

    public override string GetInfo(DateTime selected)
    {
        return selected.ToString("MMMM", CultureInfo.CurrentCulture).ToUpper() + " " + selected.Year;
    }
}