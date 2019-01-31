using System;
using System.Collections.Generic;
using System.Collections;
using MaterialUI;
using UnityEngine.UI;
using UnityEngine;
using RogoDigital.Lipsync;
[Flags]
[Serializable]
public enum AppMode
{
    SAA = 1,
    CSU = 2
}

public class AppManager : MonoSingleton<AppManager>
{   
    public static bool firstTime=false;
    [SerializeField] private LipSyncData catdat;
    [SerializeField] private MaterialUI.ScreenManager screenManager;
    private DateTime _currentDate;
    [SerializeField] private Camera avatarCam;
    [SerializeField] private RawImage saarawImg, csurawImg;
    [SerializeField] private RenderTexture avatarTexture;
    [SerializeField] private GameObject CSU_not;
    [SerializeField] private GameObject SAA_not;
    [SerializeField] private GameObject Notification, Notification_CSU;
    [SerializeField] private GameObject avatar;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] public Text[] Saanotfn_txt, Csunotfn_text;
    [SerializeField] private Text[] currentinfo;

    public AppMode currentAppMode;

    public static Action<AppMode> OnModeChange;
    public static bool FirstTest, SecondTest;
    public static string User_name;
    public static string UserName, Gender, Age;
    public static string Password = "xolair";
    public static int AnswerIndx;

    public static List<string> Saanotfn = new List<string>();
    public static List<string> cusnotfn = new List<string>();
    private WaitForSeconds delay;
    private void Start()
    {
#if FINAL_BUILD
        Debug.unityLogger.logEnabled = false;
#else
        Debug.unityLogger.logEnabled = true;
#endif

        // make sure login screen is displayed first
        if (!Application.isEditor)
        {
            ScreenManager.Instance.Set(0);
        }

        // Debug.Log(UnityEngine.iOS.CalendarUnit.Year);
        //PlayerPrefs.DeleteAll();
        currentinfo[2].text =
            currentinfo[0].text = DateTime.Today.ToString("MMM") + " " + DateTime.Today.Day.ToString();
        currentinfo[3].text = currentinfo[1].text = DateTime.Today.ToString("ddd");
        createRenderTexture();
    }

    public void SetMode(AppMode appMode)
    {
        currentAppMode = appMode;
        OnModeChange?.Invoke(currentAppMode);

        if (currentAppMode == AppMode.SAA)
        {
            ScreenManager.Instance.Set(2);
        }
        else if (currentAppMode == AppMode.CSU)
        {
            ScreenManager.Instance.Set(3);
        }

        if (PlayerPrefs.HasKey("first"))
        {
            //avatar.GetComponent<LipSync>().Play(lipsync[UnityEngine.Random.Range(0, lipsync.Length - 1)]);
            LipSyncData currentdat =
                Resources.Load<LipSyncData>("General_datafiles/" + UnityEngine.Random.Range(1, 28).ToString());
            catdat = currentdat;
            avatar.GetComponent<LipSync>().Play(currentdat);
            if(!firstTime)
            {
                delay = new WaitForSeconds(0.5f);
                StartCoroutine(setMood());
            }

        }
        else
        {
            PlayerPrefs.SetString("first", "first");
            LipSyncData currentdat = Resources.Load<LipSyncData>("General_datafiles/" + "xen");
            catdat = currentdat;
            avatar.GetComponent<LipSync>().Play(currentdat);
            if(!firstTime)
            {
                delay = new WaitForSeconds(0.5f);
                StartCoroutine(setMood());
            }

        }
        firstTime = true;
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
       
    }

    public void Stop_avtranim()
    {
        audioSource.Stop();
    }
    private void createRenderTexture()
    {
        avatarTexture = new RenderTexture(Screen.width, Screen.height,16);
        avatarCam.targetTexture = avatarTexture;
        csurawImg.texture = avatarTexture;
        saarawImg.texture = avatarTexture;

    }
    //public void avatarSet()
    //{

    // if (PlayerPrefs.HasKey("first"))
    // {
    //     //avatar.GetComponent<LipSync>().Play(lipsync[UnityEngine.Random.Range(0, lipsync.Length - 1)]);
    //     LipSyncData currentdat =
    //         Resources.Load<LipSyncData>("General_datafiles/" + UnityEngine.Random.Range(1, 28).ToString());
    //        catdat = currentdat;
    //        avatar.GetComponent<LipSync>().Play(currentdat);
    //        delay = new WaitForSeconds(6f);
    //        StartCoroutine(setMood());
    // }
    // else
    // {
    //     PlayerPrefs.SetString("first", "first");
    //        LipSyncData currentdat  =Resources.Load<LipSyncData>("General_datafiles/" + "xen");
    //        catdat = currentdat;
    //     avatar.GetComponent<LipSync>().Play(currentdat);
    //        delay = new WaitForSeconds(6f);
    //        StartCoroutine(setMood());
    //    }
     
    //}
    IEnumerator setMood()
    {
        while(true)
        {
            yield return delay;
            if(!audioSource.isPlaying)
            {
                if(currentAppMode==AppMode.CSU)
                {
                    screenManager.Set(12);
                }
                else if(currentAppMode==AppMode.SAA)
                {
                    screenManager.Set(19);
                }
                yield break;
            }
        }
    }
}