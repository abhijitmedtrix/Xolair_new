using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class CalendarScreen : MonoBehaviour
{
    [SerializeField] private List<GameObject> Day_space;
    [SerializeField] private Text Date_Year;

    private int Max_days, current_day, startIndex, Next_month;
    private DateTime temp_date;

    public static int Reminder_Month, Reminder_Day, Reminder_Year, Current_Month, Current_Year;

    [SerializeField] private GameObject Enddate_panel;

    private void Start()
    {
        Debug.Log("divide" + 32f / 7f + "remainider" + 32 % 7);
        Next_month = DateTime.Now.Month;

        Debug.Log($"Days in month {DateTime.Now.Month}: {DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month)}");

        Max_days = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);

        Reset();

        //  Debug.Log(DateTime.Now.AddDays(-DateTime.Now.Day + 1).DayOfWeek);
        SetCalendar(Max_days, DateTime.Now.AddDays(-DateTime.Now.Day + 1).DayOfWeek);
        Date_Year.text = DateTime.Today.ToString("MMMM") + " " + DateTime.Today.Year;
        temp_date = DateTime.Today;

        // Debug.Log ( temp_date.AddDays(-temp_date.Day + 1).DayOfWeek.ToString());
        Reminder_Day = DateTime.Now.Day;
        Current_Month = Reminder_Month = DateTime.Now.Month;
        Current_Year = Reminder_Year = DateTime.Now.Year;
    }

    private void SetCalendar(int maxDays, DayOfWeek day)
    {
        startIndex = (int) day;
        for (int i = 1;
            i <= maxDays;
            startIndex++, i++)
        {
            Day_space[startIndex].SetActive(true);
            Day_space[startIndex].tag = i.ToString();
            Day_space[startIndex].GetComponent<Text>().text = i.ToString();
        }
    }

    public void Next()
    {
        Next_month++;
        if (Next_month > 12)
        {
            Next_month = 12;
            Current_Month = Reminder_Month = 12;
        }

        else
        {
            Reset();
            temp_date = temp_date.AddMonths(1);
            Max_days = DateTime.DaysInMonth(DateTime.Now.Year, Next_month);
            Current_Month = Reminder_Month = Next_month;
            Current_Year = DateTime.Today.Year;
            Date_Year.text = temp_date.ToString("MMMM") + " " + DateTime.Today.Year.ToString();
            SetCalendar(Max_days, temp_date.AddDays(-temp_date.Day + 1).DayOfWeek);
        }
    }

    public void Prev()
    {
        Next_month--;
        if (Next_month < DateTime.Today.Month)
        {
            Next_month = DateTime.Today.Month;
            Current_Month = Reminder_Month = DateTime.Today.Month;
        }

        else
        {
            Reset();
            temp_date = temp_date.AddMonths(-1);
            Max_days = DateTime.DaysInMonth(DateTime.Now.Year, Next_month);
            Current_Month = Reminder_Month = Next_month;
            Current_Year = DateTime.Today.Year;
            Date_Year.text = temp_date.ToString("MMMM") + " " + DateTime.Today.Year.ToString();
            SetCalendar(Max_days, temp_date.AddDays(-temp_date.Day + 1).DayOfWeek);
        }
    }

    private void Reset()
    {
        for (int i = 0; i < Day_space.Count; i++)
        {
            Day_space[i].SetActive(false);
        }
    }

    public void AddRemainder()
    {
        if (Enddate_panel.active)
        {
            Enddate_panel.SetActive(false);
        }
        else
        {
            Enddate_panel.SetActive(true);
        }
    }
}