using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class YearPickerLayout : DatePickerLayout
{
    int currentYear;
    int startYear;

    public int getStartYear()
    {
        return startYear;
    }

    public override void SetupCells(DateTime now)
    {
        // calculate the starting year which is dividable by 16
        startYear = ((int) (now.Year / GetNumberOfCells())) * GetNumberOfCells();
        currentYear = startYear;
        foreach (PickerCell cell in cells)
        {
            cell.text.text = currentYear.ToString();
            cell.text.color = fontActiveColor;

            if (currentYear == now.Year)
                cell.image.sprite = currentEntryImg;
            else
                cell.image.sprite = actualEntryImg;
            currentYear++;
        }
    }

    public override void CellClicked(PickerCell cell)
    {
        // set the new date
        calendar.focusedDate = new DateTime(
            cell.transform.GetSiblingIndex() + startYear,
            calendar.focusedDate.Month,
            calendar.focusedDate.Day);
        // zoom out
        calendar.ZoomOut();
    }

    public override string GetInfo(DateTime selected)
    {
        return startYear + " - " + (currentYear - 1);
    }
}