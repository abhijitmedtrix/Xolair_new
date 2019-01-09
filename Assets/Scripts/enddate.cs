using System;
using UnityEngine;
using UnityEngine.UI;
public class enddate : MonoBehaviour {
    [SerializeField] private Text date_txt;
    [SerializeField] private Text month_txt;
    [SerializeField] private Text year_txt;
    [SerializeField] private int maxday,maxmonth=12;
    [SerializeField] private int temp_date,temp_month;

    [SerializeField] private DateTime Enddate,dateref;
    public DateTime EndDate
    {
        get
        {
            return Enddate;
        }
    }
    private void Start()
    {
        Enddate =  DateTime.Today.AddDays(1);
        date_txt.text = Enddate.Day.ToString();
        month_txt.text = Enddate.ToString("MMM");
        year_txt.text = Enddate.ToString("yyyy");

        
    }
    public void onclickdate(string tag)
    {
        switch(tag)
        {
            case "up":
                {
                    dateref = Enddate;
                    dateref = dateref.AddDays(1);
                    break;
                }
            case "down":
                {
                    dateref = Enddate;
                    dateref=  dateref.AddDays(-1);
                    break;
                }
        }
        Debug.Log(DateTime.Compare(dateref,DateTime.Today));
        if(DateTime.Compare(dateref, DateTime.Today) ==1)
        {
            Enddate = dateref;
            date_txt.text =Enddate.Day.ToString();
            month_txt.text = Enddate.ToString("MMM");
            year_txt.text = Enddate.ToString("yyyy");
        }
        date_txt.text = Enddate.Day.ToString();
        month_txt.text = Enddate.ToString("MMM");
        year_txt.text = Enddate.ToString("yyyy");
    }
    public void onclickmonth(string tag)
    {
        switch (tag)
        {
            case "up":
                {
                    dateref = Enddate;
                    dateref = dateref.AddMonths(1);
                    break;
                }
            case "down":
                {
                    dateref = Enddate;
                    dateref = dateref.AddMonths(-1);
                    break;
                }
        }
       // Debug.Log(DateTime.Compare(dateref, DateTime.Today));
        if (DateTime.Compare(dateref, DateTime.Today) == 1)
        {
            Enddate = dateref;
            date_txt.text = Enddate.Day.ToString();
            month_txt.text = Enddate.ToString("MMM");
            year_txt.text = Enddate.ToString("yyyy");
        }
        date_txt.text = Enddate.Day.ToString();
        month_txt.text = Enddate.ToString("MMM");
        year_txt.text = Enddate.ToString("yyyy");
    }

    public void Onclickyear(string tag)
    {
        switch (tag)
        {
            case "up":
                {
                    dateref = Enddate;
                    dateref = dateref.AddYears(1);
                    break;
                }
            case "down":
                {
                    dateref = Enddate;
                    dateref = dateref.AddYears(-1);
                    break;
                }
        }
        // Debug.Log(DateTime.Compare(dateref, DateTime.Today));
        if (DateTime.Compare(dateref, DateTime.Today) == 1)
        {
            Enddate = dateref;
            date_txt.text = Enddate.Day.ToString();
            month_txt.text = Enddate.ToString("MMM");
            year_txt.text = Enddate.ToString("yyyy");
        }
        date_txt.text = Enddate.Day.ToString();
        month_txt.text = Enddate.ToString("MMM");
        year_txt.text = Enddate.ToString("yyyy");
    }
    }

