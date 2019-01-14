using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Reminder : MonoBehaviour 
{
    [SerializeField]
    private Text Hour;
    [SerializeField]
    private Text Minute;
    [SerializeField]
    private Text Day;
    //[SerializeField]
    //private InputField Details_txt;
    [SerializeField]
    private int hour_display;
    [SerializeField]
    private int minute_display;
    [SerializeField]
    private string key,dat;
    [SerializeField]
    private enddate enddate;
    DateTime Enddate,notifdate;
    [SerializeField] private string[] notifmessage;
    private void Start()
    {
        //PlayerPrefs.DeleteAll();
        UnityEngine.iOS.NotificationServices.RegisterForNotifications(
       UnityEngine.iOS.NotificationType.Alert |
        UnityEngine.iOS.NotificationType.Badge |
      UnityEngine.iOS.NotificationType.Sound);
       
        
       
    }

    public void OnClick()
    {
      //  Debug.Log(AppManager.Instance.CurrentMode);
        hour_display = int.Parse(Hour.text);
        minute_display = int.Parse(Minute.text);
        if (Day.text=="AM")
        {
            if(hour_display==12)
            {
                hour_display = 0;
            }
           // Debug.Log("hour" + hour_display + "min" + minute_display);
            Debug.Log(enddate.EndDate.Year + "-" + enddate.EndDate.Month + "-" + enddate.EndDate.Day);
            TimeSpan span = enddate.EndDate - DateTime.Today;
            int x =(int)span.TotalDays;
            Debug.Log(x);
            notifdate = new DateTime(notifdate.Year, notifdate.Month, notifdate.Day, hour_display, minute_display, 0);
            for (int i = 1; i <= x;i++)
            {
                notifdate = DateTime.Today.AddDays(i);
                for (int y = 0; y < 3;y++)

                {
                   
                    key = AppManager.Instance.CurrentMode + "," + notifdate.Day.ToString()+"/"+notifdate.Month.ToString()+"/"+notifdate.Year.ToString();
                    Debug.Log(key);
                
                    dat = key + "," + Hour.text + "/" + Minute.text+","+Day.text;
                    PlayerPrefs.SetString(key, dat);
                    UnityEngine.iOS.LocalNotification notif = new UnityEngine.iOS.LocalNotification();
                    notif.fireDate = notifdate;
                    notif.alertBody = notifmessage[y];
                    UnityEngine.iOS.NotificationServices.ScheduleLocalNotification(notif);
                }
                Debug.Log("notification number" + UnityEngine.iOS.NotificationServices.scheduledLocalNotifications.Length);
             }
           // DateTime time = new DateTime(Calander_Manager.Reminder_Year, Calander_Manager.Reminder_Month, Calander_Manager.Reminder_Day, hour_display, minute_display, 0);
            

           // dat = AppManager.Instance.CurrentMode + "," + Calander_Manager.Reminder_Day.ToString() + "/" + Calander_Manager.Reminder_Month.ToString() + "/" + Calander_Manager.Reminder_Year.ToString() + "," + Hour.text + "/" + Minute.text + count.ToString() ;
          //  key = AppManager.Instance.CurrentMode + "," + Calander_Manager.Reminder_Day.ToString() + "/" + Calander_Manager.Reminder_Month.ToString() + "/" + Calander_Manager.Reminder_Year.ToString() + ","+count.ToString();
            // Debug.Log(key);
            //while (PlayerPrefs.HasKey(key))
            //{
            //    count++;
            //    key = AppManager.Instance.CurrentMode + "," + Calander_Manager.Reminder_Day.ToString() + "/" + Calander_Manager.Reminder_Month.ToString() + "/" + Calander_Manager.Reminder_Year.ToString() + "," + count.ToString();

            //}
           //Debug.Log(key);
            //PlayerPrefs.SetString(key, dat);
            //Debug.Log(PlayerPrefs.GetString(key));
           // UnityEngine.iOS.LocalNotification notif = new UnityEngine.iOS.LocalNotification();
           // notif.fireDate = time;
            //notif.alertBody = Details_txt.text;
            //UnityEngine.iOS.NotificationServices.ScheduleLocalNotification(notif);
        }
        else if(Day.text=="PM")
        {
            
            if(hour_display==12)
            {
                hour_display = 12;
            }
            else
            {
                hour_display += 12;
            }
            TimeSpan span = enddate.EndDate - DateTime.Today;
            int x = (int)span.TotalDays;
            for (int i = 1; i <= x; i++)
            {
                notifdate = DateTime.Today.AddDays(i);
                notifdate = new DateTime(notifdate.Year, notifdate.Month, notifdate.Day, hour_display, minute_display, 0);
                for (int y = 0; y < 3; y++)

                {
                    key = AppManager.Instance.CurrentMode + "," + notifdate.Day.ToString() + "/" + notifdate.Month.ToString() + "/" + notifdate.Year.ToString();
                    dat = key + "," + Hour.text + "/" + Minute.text+"," + Day.text;
                   
                    PlayerPrefs.SetString(key, dat);
                    UnityEngine.iOS.LocalNotification notif = new UnityEngine.iOS.LocalNotification();
                    notif.fireDate = notifdate;
                    notif.alertBody = notifmessage[y];
                    UnityEngine.iOS.NotificationServices.ScheduleLocalNotification(notif);
                }

            }
            // //Debug.Log("hour" + hour_display + "min" + minute_display);
            // //Debug.Log(Calander_Manager.Reminder_Year + "-" + Calander_Manager.Reminder_Month + "" + Calander_Manager.Reminder_Day);
            // DateTime time = new DateTime(Calander_Manager.Reminder_Year, Calander_Manager.Reminder_Month, Calander_Manager.Reminder_Day, hour_display, minute_display, 0);
            // count = 1;

            // dat = AppManager.Instance.CurrentMode + "," + Calander_Manager.Reminder_Day.ToString() + "/" + Calander_Manager.Reminder_Month.ToString() + "/" + Calander_Manager.Reminder_Year.ToString() + "," + Hour.text + "/" + Minute.text + "," + Details_txt.text;
            // key = AppManager.Instance.CurrentMode + "," + Calander_Manager.Reminder_Day.ToString() + "/" + Calander_Manager.Reminder_Month.ToString() + "/" + Calander_Manager.Reminder_Year.ToString() + "," + count.ToString();
            // // Debug.Log(key);
            // while (PlayerPrefs.HasKey(key))
            // {
            //     count++;
            //     key = AppManager.Instance.CurrentMode + "," + Calander_Manager.Reminder_Day.ToString() + "/" + Calander_Manager.Reminder_Month.ToString() + "/" + Calander_Manager.Reminder_Year.ToString() + "," + count.ToString();

            // }
            // //Debug.Log(key);
            // PlayerPrefs.SetString(key, dat);
            // //Debug.Log(PlayerPrefs.GetString(key));

            // UnityEngine.iOS.LocalNotification notif = new UnityEngine.iOS.LocalNotification();
            // notif.fireDate = time;
            //// notif.alertBody = Details_txt.text;
            //UnityEngine.iOS.NotificationServices.ScheduleLocalNotification(notif);

        }
    }


}
