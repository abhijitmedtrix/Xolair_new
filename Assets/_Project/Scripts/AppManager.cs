using System;

using RogoDigital.Lipsync;
using System.Collections.Generic;

using UnityEngine.UI;
using System.Collections;
using UnityEngine;
public class AppManager : MonoBehaviour
{
    public static bool FirstTest, SecondTest;

    private DateTime _currentDate;
    public static string Current_mode;
    public static string User_name;
    public static List<Texture2D> Images;
    public static string UserName, Gender, Age;
    public static string Password = "xolair";
    public static int AnswerIndx;
    [SerializeField] private GameObject CSU_not;
    [SerializeField] private GameObject SAA_not;
    [SerializeField] private GameObject Notification, Notification_CSU;
    [SerializeField] private GameObject SAA_text, CSU_text;

    [SerializeField] private GameObject avatar;
    [SerializeField] private Vector3 avatrpos;
    [SerializeField] private Button CSU_butn;
    [SerializeField] private Button SAA_butn;
    //public static bool Avatrshow = true;
    [SerializeField] private MaterialUI.ScreenManager screenManager;
    //public bool startonce;
    //[SerializeField] private List<GameObject> SAA_obj, CSU_obj;
   // [SerializeField] private LipSyncData[] lipsync;
    [SerializeField] private Sprite[] spriteswap;
    [SerializeField] private ExampleConversation exampleConversation;

    //[SerializeField] private GameObject[] saa;
    //[SerializeField] private GameObject[] csu;
    public enum Mode
    {
        SSA,
        CSU
    }
    [SerializeField]
    private Text[] currentinfo;
    public Mode CurrentMode;
    [SerializeField]
    private Scrollbar saa_Scroll, csu_Scroll;
    [SerializeField]
    private Text[] saa_Txt, csu_Txt;
    public Action<Mode> OnModeChange;
    private void Start()
    {
        Debug.Log(saa_Txt[0].fontStyle);
       // Debug.Log(UnityEngine.iOS.CalendarUnit.Year);
        FirstTest = false;
        SecondTest = false;
        Images = new List<Texture2D>();
        //  Debug.Log(DateTime.Today.DayOfYear);
        //PlayerPrefs.DeleteAll();
        currentinfo[2].text=currentinfo[0].text = DateTime.Today.ToString("MMM")+" "+DateTime.Today.Day.ToString();
        currentinfo[3].text=currentinfo[1].text = DateTime.Today.ToString("ddd");
        var asthma = TrackerManager.GetData(DateTime.Today, TrackerManager.TrackerType.Asthma);
        Debug.Log("asthma" + asthma.GetScore());

    }
    public void SetMode(Mode mode)
    {
        CurrentMode = mode;
        OnModeChange?.Invoke(CurrentMode);
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
            Notification.SetActive(false);
            SAA_not.SetActive(false);
        }
        else
        {
            SAA_not.SetActive(true);
        }
        var asthma = TrackerManager.GetData(DateTime.Today, TrackerManager.TrackerType.Asthma);
        var symptom = TrackerManager.GetData(DateTime.Today, TrackerManager.TrackerType.Symptom);
    
 
        if (asthma.GetScore() > 19 && symptom.GetScore() >= 1)
        {
            UnityEngine.iOS.LocalNotification notif = new UnityEngine.iOS.LocalNotification();
            notif.fireDate = DateTime.Now.AddDays(1).AddSeconds(-1);
            notif.alertBody = "PLEASE TAKE THE CSU TEST";
            // Notification.SetActive(true);
            SAA_not.transform.GetChild(0).GetComponent<Text>().text = "No notification";
        }
        else if (asthma.GetScore() >= 19)
        {

            SAA_not.transform.GetChild(0).GetComponent<Text>().text = "PLEASE TAKE SYMPTOM TRACKER TEST";
        }
        else if (symptom.GetScore() >= 1)
        {

            SAA_not.transform.GetChild(0).GetComponent<Text>().text = "PLEASE VISIT A PHYSICIAN";
        }
        else
        {
            Notification.gameObject.SetActive(false);
            SAA_not.SetActive(false);
        }
        //SAA_not.transform.GetChild(0).GetComponent<Text>()=
    }
    public void Onclick_csu()
    {

        if (CSU_not.active)
        {
            Notification_CSU.SetActive(false);
            CSU_not.SetActive(false);
        }
        else
        {
            Notification_CSU.SetActive(false);
            CSU_not.SetActive(true);
        }
        var CSU = TrackerManager.GetData(DateTime.Today, TrackerManager.TrackerType.CSU);
        var UAS = TrackerManager.GetData(DateTime.Today, TrackerManager.TrackerType.UAS);

        if (CSU.GetScore() > 1 && UAS.GetScore() >= 2)
        {
            UnityEngine.iOS.LocalNotification notif = new UnityEngine.iOS.LocalNotification();
            notif.fireDate = DateTime.Now.AddDays(1).AddSeconds(-1);
            notif.alertBody = "PLEASE TAKE THE CSU TEST";
            // Notification.SetActive(true);
            CSU_not.transform.GetChild(0).GetComponent<Text>().text = "No notification";
        }
        else if (CSU.GetScore() >= 1)
        {

            CSU_not.transform.GetChild(0).GetComponent<Text>().text = "PLEASE TAKE UAS TEST";
        }
        else if (UAS.GetScore() >= 2)
        {
            CSU_not.transform.GetChild(0).GetComponent<Text>().text = "PLEASE TAKE CSU TEST";
        }
        else
        {
            Notification_CSU.SetActive(false);
            CSU_not.SetActive(false);
        }
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
    //public void setavatr()
    //{
    //    AppManager.Avatrshow = true;
    //    avatar.transform.position = avatrpos;
    //    CSU_text.SetActive(true);
    //    SAA_text.SetActive(true);
    //}
    //public void hideavatr()
    //{
    //    AppManager.Avatrshow = false;
    //    avatar.transform.position = Vector3.one * 1000;
    //    CSU_text.SetActive(false);
    //    SAA_text.SetActive(false);
    //}
    public void Onclick()
    {
        //if(AppManager.Current_mode=="CSU")
        //{
        //    Debug.Log("destroycsu");
        //    for (int i =0; i < saa.Length;i++)
        //    {
        //        Destroy(saa[i]);
        //    }

        //}
        //else if(AppManager.Current_mode=="SAA")
        //{
        //    Debug.Log("destroysaa");
        //    for (int i = 0; i < csu.Length; i++)
        //    {
        //        Destroy(csu[i]);
        //    }
        //}
        StartCoroutine(generalmsg());
    }
    IEnumerator generalmsg()
    {
        if (AppManager.Current_mode == "CSU")
        {
            CSU_butn.image.sprite = spriteswap[0];

        }
        else if (AppManager.Current_mode == "SAA")
        {
            SAA_butn.image.sprite = spriteswap[1];
        }
        yield return new WaitForSeconds(1);
        if(AppManager.Current_mode=="CSU")
        {
            screenManager.Set(3);
        }
        else if(AppManager.Current_mode=="SAA")
        {
            screenManager.Set(2);
        }
        yield return new WaitForSeconds(1);
        if(PlayerPrefs.HasKey("first"))
        {

            //avatar.GetComponent<LipSync>().Play(lipsync[UnityEngine.Random.Range(0, lipsync.Length - 1)]);
            LipSyncData currentdat = Resources.Load<LipSyncData>("General_datafiles/"+UnityEngine.Random.Range(1,28).ToString());
            avatar.GetComponent<LipSync>().Play(currentdat);
        }
        else
        {
            PlayerPrefs.SetString("first", "first");
            LipSyncData currentdat = Resources.Load<LipSyncData>("General_datafiles/" +"xen");
            avatar.GetComponent<LipSync>().Play(currentdat);
        }
   
    }
    public void Fliptracker()
    {
        if(Current_mode=="SAA")
        {
            if(saa_Scroll.value>0.5)
            {
                saa_Txt[0].fontStyle = FontStyle.Normal;
                csu_Txt[0].fontStyle = FontStyle.Bold;
                saa_Scroll.value = 1;
            }
            else 
            {
                saa_Txt[0].fontStyle = FontStyle.Bold;
                csu_Txt[0].fontStyle = FontStyle.Normal;
                saa_Scroll.value = 0;
            }
        }
        else if(Current_mode=="CSU")
        {

        }
    }
}