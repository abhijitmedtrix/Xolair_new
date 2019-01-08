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
    [SerializeField] private AudioSource audioSource;
    //[SerializeField] private GameObject[] saa;
    //[SerializeField] private GameObject[] csu;
    public static List<string> Saanotfn=new List<string>();
    public static List<string> cusnotfn = new List<string>();
    [SerializeField] public Text[] Saanotfn_txt,Csunotfn_text;
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
        //Debug.Log(saa_Txt[0].fontStyle);
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
        if(SAA_not.active)
        {
            SAA_not.SetActive(false);
        }
        else
        {
            SAA_not.SetActive(true);
        }
        if(Saanotfn.Count==0)
        {
            foreach(Text tx in Saanotfn_txt)
            {
                tx.text = null;
            }
            Saanotfn_txt[0].text = "NO NEW NOTIFICATION";
        }
        else if(Saanotfn.Count==1)
        {
           
            Saanotfn_txt[0].text = Saanotfn[0];
            Saanotfn_txt[1].text = null;
            //Notification.SetActive(true);
        }
        else if(Saanotfn.Count==2)
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
        if(cusnotfn.Count==0)
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
            yield return new WaitForSeconds(1);
            screenManager.Set(3);

        }
        else if (AppManager.Current_mode == "SAA")
        {
            SAA_butn.image.sprite = spriteswap[1];
            yield return new WaitForSeconds(1);
            screenManager.Set(2);
        }
       
        //if(AppManager.Current_mode=="CSU")
        //{
        //    screenManager.Set(3);
        //}
        //else if(AppManager.Current_mode=="SAA")
        //{
        //    screenManager.Set(2);
        //}

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
            if(saa_Scroll.value>=0.5f)
            {
                saa_Txt[0].fontStyle = FontStyle.Normal;
                csu_Txt[0].fontStyle = FontStyle.Bold;
                saa_Scroll.value = 1;
               
                Current_mode = "CSU";
                exampleConversation.Set_Avatar(Current_mode);
                Onclick();
                saa_Scroll.value = 0;
            }

            //else
            //{
            //    saa_Scroll.value = 1;
            //}
           
        }
        else if(Current_mode=="CSU")
        {
           
            if(csu_Scroll.value<=0.5f)
            {
                saa_Txt[1].fontStyle = FontStyle.Bold;
                csu_Txt[1].fontStyle = FontStyle.Normal;
                csu_Scroll.value = 0;
                //screenManager.Set(2);
                Current_mode = "SAA";
                exampleConversation.Set_Avatar(Current_mode);
                Onclick();
                csu_Scroll.value = 1;

            }
            //else
            //{
            //    csu_Scroll.value=0;
            //}
        }
    }
    public void Stop_avtranim()
    {
        audioSource.Stop();
    }
}