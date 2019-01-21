using UnityEngine;
using UnityEngine.UI;
using System;
using System.Globalization;

public class WeekDayLayout : DatePickerLayout
{
    public override void SetupCells(DateTime now)
    {
        CultureInfo culture = CultureInfo.CurrentCulture;
        int i = 0;
        foreach (PickerCell cell in cells)
        {
            cell.text.text = culture.DateTimeFormat.AbbreviatedDayNames[i].ToUpper();
            cell.text.color = fontActiveColor;
            cell.image.sprite = actualEntryImg;
            i++;
        }
    }
}