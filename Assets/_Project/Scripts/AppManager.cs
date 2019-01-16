using System;
using RogoDigital.Lipsync;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AppManager : MonoSingleton<AppManager>
{
    private DateTime _currentDate;

    [SerializeField] private GameObject CSU_not;
    [SerializeField] private GameObject SAA_not;
    [SerializeField] private GameObject Notification, Notification_CSU;
    [SerializeField] private GameObject avatar;
    [SerializeField] private AudioSource audioSource;

    [SerializeField] public Text[] Saanotfn_txt, Csunotfn_text;
    [SerializeField] private Text[] currentinfo;

    public enum Mode
    {
        SAA,
        CSU
    }

    public Mode CurrentMode;

    public static Action<Mode> OnModeChange;

    public static bool FirstTest, SecondTest;
    public static string User_name;
    public static string UserName, Gender, Age;
    public static string Password = "xolair";
    public static int AnswerIndx;

    public static List<string> Saanotfn = new List<string>();
    public static List<string> cusnotfn = new List<string>();

    private void Start()
    {
        // Debug.Log(UnityEngine.iOS.CalendarUnit.Year);
        FirstTest = false;
        SecondTest = false;
        //  Debug.Log(DateTime.Today.DayOfYear);
        //PlayerPrefs.DeleteAll();
        currentinfo[2].text =
            currentinfo[0].text = DateTime.Today.ToString("MMM") + " " + DateTime.Today.Day.ToString();
        currentinfo[3].text = currentinfo[1].text = DateTime.Today.ToString("ddd");
        var asthma = TrackerManager.GetData(DateTime.Today, TrackerManager.TrackerType.Asthma);
        Debug.Log("asthma" + asthma.GetScore());
    }

    private void OnDisable()
    {
        if (FirstTest && SecondTest)
        {
            UnityEngine.iOS.LocalNotification notif = new UnityEngine.iOS.LocalNotification();
            notif.fireDate = DateTime.Now.AddDays(1).AddSeconds(-1);
            notif.alertBody = "PLEASE TAKE THE CSU TEST";
        }
    }

    public void SetMode(Mode mode)
    {
        CurrentMode = mode;
        OnModeChange?.Invoke(CurrentMode);

        if (PlayerPrefs.HasKey("first"))
        {
            //avatar.GetComponent<LipSync>().Play(lipsync[UnityEngine.Random.Range(0, lipsync.Length - 1)]);
            LipSyncData currentdat =
                Resources.Load<LipSyncData>("General_datafiles/" + UnityEngine.Random.Range(1, 28).ToString());
            avatar.GetComponent<LipSync>().Play(currentdat);
        }
        else
        {
            PlayerPrefs.SetString("first", "first");
            LipSyncData currentdat = Resources.Load<LipSyncData>("General_datafiles/" + "xen");
            avatar.GetComponent<LipSync>().Play(currentdat);
        }
    }

    #region Test

    // test method to add day to current date (avoiding using System DateTime.now)
    public void AddDay()
    {
        _currentDate = _currentDate.AddDays(1);
    }

    public DateTime GetNextDay()
    {
        return _currentDate.AddDays(1);
    }

    // test method to remove day to current date
    public void RemoveDay()
    {
        _currentDate = _currentDate.AddDays(-1);
    }

    public DateTime GetPreviousDay()
    {
        return _currentDate.AddDays(-1);
    }

    #endregion

    public void OnClick_saa()
    {
        if (SAA_not.active)
        {
            SAA_not.SetActive(false);
        }
        else
        {
            SAA_not.SetActive(true);
        }

        if (Saanotfn.Count == 0)
        {
            foreach (Text tx in Saanotfn_txt)
            {
                tx.text = null;
            }

            Saanotfn_txt[0].text = "NO NEW NOTIFICATION";
        }
        else if (Saanotfn.Count == 1)
        {
            Saanotfn_txt[0].text = Saanotfn[0];
            Saanotfn_txt[1].text = null;
            //Notification.SetActive(true);
        }
        else if (Saanotfn.Count == 2)
        {
            Saanotfn_txt[0].text = Saanotfn[0];
            Saanotfn_txt[1].text = Saanotfn[1];
            // Notification.SetActive(true);
        }

        Notification.SetActive(false);
        Saanotfn.Clear();
        //if (SAA_not.active)
        //{
        //    Notification.SetActive(false);
        //    SAA_not.SetActive(false);
        //}
        //else
        //{
        //    Notification.SetActive(false);
        //    SAA_not.SetActive(true);
        //}
        //var asthma = TrackerManager.GetData(DateTime.Today, TrackerManager.TrackerType.Asthma);
        //var symptom = TrackerManager.GetData(DateTime.Today, TrackerManager.TrackerType.Symptom);


        //if (asthma.GetScore() > 19 && symptom.GetScore() >= 1)
        //{
        //    //UnityEngine.iOS.LocalNotification notif = new UnityEngine.iOS.LocalNotification();
        //    //notif.fireDate = DateTime.Now.AddDays(1).AddSeconds(-1);
        //    //notif.alertBody = "PLEASE TAKE THE CSU TEST";
        //   // Notification.SetActive(true);
        //    SAA_not.transform.GetChild(0).GetComponent<Text>().text = "PLEASE CONSULT A PHYSICIAN SOON";
        //}
        //else if (asthma.GetScore() >= 19)
        //{

        //    SAA_not.transform.GetChild(0).GetComponent<Text>().text = "PLEASE TAKE SYMPTOM TRACKER TEST";
        //}
        //else if (symptom.GetScore() >= 1)
        //{

        //    SAA_not.transform.GetChild(0).GetComponent<Text>().text = "PLEASE VISIT A PHYSICIAN";
        //}
        //else
        //{
        //    Notification.gameObject.SetActive(false);
        //    //SAA_not.SetActive(true);
        //    SAA_not.transform.GetChild(0).GetComponent<Text>().text = "NO NEW NOTIFICATIONS";
        //}
        //SAA_not.transform.GetChild(0).GetComponent<Text>()=
    }

    public void Onclick_csu()
    {
        if (CSU_not.active)
        {
            CSU_not.SetActive(false);
        }
        else
        {
            CSU_not.SetActive(true);
        }

        if (cusnotfn.Count == 0)
        {
            CSU_not.transform.GetChild(0).GetComponent<Text>().text = "NO NEW NOTIFICATION";
        }
        else
        {
            CSU_not.transform.GetChild(0).GetComponent<Text>().text = cusnotfn[0];
        }

        Notification_CSU.SetActive(false);
        cusnotfn.Clear();
        //if(csuno)
        //var CSU = TrackerManager.GetData(DateTime.Today, TrackerManager.TrackerType.CSU);
        //var UAS = TrackerManager.GetData(DateTime.Today, TrackerManager.TrackerType.UAS);

        //if (CSU.GetScore() > 1 && UAS.GetScore() >= 2)
        //{
        //    UnityEngine.iOS.LocalNotification notif = new UnityEngine.iOS.LocalNotification();
        //    notif.fireDate = DateTime.Now.AddDays(1).AddSeconds(-1);
        //    notif.alertBody = "PLEASE TAKE THE CSU TEST";
        //    // Notification.SetActive(true);
        //    CSU_not.transform.GetChild(0).GetComponent<Text>().text = "PLEASE VISIT A PHYSICIAN";
        //}
        //else if (CSU.GetScore() >= 1)
        //{

        //    CSU_not.transform.GetChild(0).GetComponent<Text>().text = "PLEASE TAKE UAS TEST";
        //}
        //else if (UAS.GetScore() >= 2)
        //{
        //    CSU_not.transform.GetChild(0).GetComponent<Text>().text = "PLEASE TAKE CSU TEST";
        //}
        //else
        //{
        //    Notification_CSU.SetActive(false);
        //    CSU_not.SetActive(true);

        //    CSU_not.transform.GetChild(0).GetComponent<Text>().text = "NO NEW NOTIFICATIONS";
        //}
    }

    public void Stop_avtranim()
    {
        audioSource.Stop();
    }
}