using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public class Calander_Manager : MonoBehaviour {

    // Use this for initialization
    [SerializeField]
    private List<GameObject> Day_space;
    private int Max_days, current_day, start_indx,  Next_month;
    [SerializeField]
    private Text Date_Year;
    DateTime temp_date;
    public static int Reminder_Month, Reminder_Day, Reminder_Year,Current_Month,Current_Year;
    [SerializeField] private GameObject Enddate_panel;
    //public List<GameObject> Reminder_list;
    void Start()
    {


        Debug.Log("divide"+32f/7f+"remainider"+32%7);
        Next_month =DateTime.Now.Month;
        //Debug.Log(DateTime.DaysInMonth(2018, 11));
        Max_days = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
        Reset();
      //  Debug.Log(DateTime.Now.AddDays(-DateTime.Now.Day + 1).DayOfWeek);
        calender_set(Max_days, DateTime.Now.AddDays(-DateTime.Now.Day + 1).DayOfWeek.ToString());
        Date_Year.text =DateTime.Today.ToString("MMMM")+" "+DateTime.Today.Year;
        temp_date = DateTime.Today;
       // Debug.Log ( temp_date.AddDays(-temp_date.Day + 1).DayOfWeek.ToString());
        Reminder_Day = DateTime.Now.Day;
        Current_Month=Reminder_Month = DateTime.Now.Month;
        Current_Year=Reminder_Year = DateTime.Now.Year;
        

    }
	
	// Update is called once per frame
	void Update () {

		
	}
    void calender_set(int max_days,string day)
    {
        switch(day)
        {
            case "Sunday":
                {
                    start_indx = 0;
                    break;
                }
            case "Monday":
                {
                    start_indx = 1;
                    break;
                }
            case "Tuesday":
                {
                    start_indx = 2;
                    break;
                }
            case "Wednesday":
                {
                    start_indx = 3;
                    break;
                }
            case "Thursday":
                {
                    start_indx = 4;
                    break;
                }
            case "Friday":
                {
                    start_indx = 5;
                    break;
                }
            case "Saturday":
                {
                    start_indx = 6;
                    break;
                }
               
        }
        for (int i=1; i <= max_days; start_indx++,i++)
        {
            Day_space[start_indx].SetActive(true);
            Day_space[start_indx].tag = i.ToString();
            Day_space[start_indx].GetComponent<Text>().text =i.ToString();



        }

    }
    public void Next ()
    {
        Next_month++;
        if(Next_month>12)
        {
            Next_month = 12;
            Current_Month=Reminder_Month = 12;
        }
        else
        {
            Reset();
            temp_date = temp_date.AddMonths(1);
            Max_days = DateTime.DaysInMonth(DateTime.Now.Year, Next_month);
            Current_Month=Reminder_Month =Next_month;
            Current_Year = DateTime.Today.Year;
            Date_Year.text = temp_date.ToString("MMMM") + " " + DateTime.Today.Year.ToString();
            calender_set(Max_days, temp_date.AddDays(-temp_date.Day + 1).DayOfWeek.ToString());
        }

    }
    public void back()
    {
        Next_month--;
        if (Next_month < DateTime.Today.Month)
        {
            Next_month = DateTime.Today.Month;
            Current_Month=Reminder_Month = DateTime.Today.Month;
        }
        else
        {
            Reset();
            temp_date = temp_date.AddMonths(-1);
            Max_days = DateTime.DaysInMonth(DateTime.Now.Year, Next_month);
            Current_Month=Reminder_Month = Next_month;
            Current_Year = DateTime.Today.Year;
            Date_Year.text = temp_date.ToString("MMMM") + " " + DateTime.Today.Year.ToString();
            calender_set(Max_days, temp_date.AddDays(-temp_date.Day + 1).DayOfWeek.ToString());
        }
    }
    private void Reset()
    {
        for (int i = 0; i < Day_space.Count;i++)
        {
            Day_space[i].SetActive(false);
        }
    }
   
    public void AddRemainder()
    {
        if(Enddate_panel.active)
        {
            Enddate_panel.SetActive(false);
        }
        else
        {
            Enddate_panel.SetActive(true);
        }
       
    }

}
