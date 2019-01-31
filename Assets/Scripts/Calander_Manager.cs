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
    [SerializeField]
    private Sprite[] moodImg;
    string path;
    void Start()
    {


      
      
     
        

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
            //Day_space[start_indx].tag = i.ToString();
            Day_space[start_indx].GetComponent<Text>().text =i.ToString();
            Debug.Log(i.ToString() + temp_date.Month.ToString() + temp_date.Year.ToString());
            path = i.ToString() + temp_date.Month.ToString() + temp_date.Year.ToString();
            if (PlayerPrefs.HasKey(path))
            {
                Day_space[start_indx].transform.GetChild(0).GetComponent<Image>().sprite = moodImg[PlayerPrefs.GetInt(path)];
            }
            else
            {
                Day_space[start_indx].transform.GetChild(0).GetComponent<Image>().sprite = null;
            }

        }

    }
    public void Next ()
    {
        Reset();
        temp_date = temp_date.AddMonths(1);
        Max_days = DateTime.DaysInMonth(temp_date.Year, temp_date.Month);
        Date_Year.text = temp_date.ToString("MMMM") + " " + temp_date.Year.ToString();
        calender_set(Max_days, temp_date.AddDays(-temp_date.Day + 1).DayOfWeek.ToString());

      

    }
    public void back()
    {
      
       
            Reset();
            temp_date = temp_date.AddMonths(-1);
            Max_days = DateTime.DaysInMonth(temp_date.Year, temp_date.Month);
            Date_Year.text = temp_date.ToString("MMMM") + " " + temp_date.Year.ToString();
            calender_set(Max_days, temp_date.AddDays(-temp_date.Day + 1).DayOfWeek.ToString());

    }
    private void Reset()
    {
        for (int i = 0; i < Day_space.Count;i++)
        {
            Day_space[i].SetActive(false);
        }
    }
   
    public void StartCalendar()
    {
        Next_month = DateTime.Now.Month;
       
        Max_days = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
        Reset();
        temp_date = DateTime.Today;
        calender_set(Max_days, DateTime.Now.AddDays(-DateTime.Now.Day + 1).DayOfWeek.ToString());
        Date_Year.text = DateTime.Today.ToString("MMMM") + " " + DateTime.Today.Year;
       
    }

}
