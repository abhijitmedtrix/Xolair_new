using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using System.Globalization;

public class MonthPickerLayout : DatePickerLayout
{
    public override void SetupCells(DateTime now)
    {
        // calculate the starting year which is dividable by 16
        CultureInfo culture = CultureInfo.CurrentCulture;
        int i = 0;
        foreach (PickerCell cell in cells)
        {
            cell.text.text =
                culture.DateTimeFormat.AbbreviatedMonthNames[i].ToString();
            cell.text.color = fontActiveColor;
            if (i + 1 == now.Month)
                cell.image.sprite = currentEntryImg;
            else
                cell.image.sprite = actualEntryImg;

            i++;
        }
    }

    public override void CellClicked(PickerCell cell)
    {
        // set the new date
        calendar.focusedDate = new DateTime(
            calendar.focusedDate.Year,
            cell.transform.GetSiblingIndex() + 1,
            Mathf.Clamp(calendar.focusedDate.Day, 1,
                DateTime.DaysInMonth(calendar.focusedDate.Year, cell.transform.GetSiblingIndex() + 1)));
        // zoom out
        calendar.ZoomOut();
    }

    public override string GetInfo(DateTime selected)
    {
        return selected.Year.ToString();
    }
}