using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Onclick_score : MonoBehaviour {
    public GameObject display;
    public void OnClick()
    {
        //if(AppManager.FirstTest&&AppManager.SecondTest)
        //{
        //    display.gameObject.SetActive(false);
        //}
        //else
        //{
        //    display.gameObject.SetActive(true);
        //}
        if(AppManager.Current_mode=="SAA")
        {
            if (Symptom_Tracker_Display.Symcurrentdat != null)
            {
                if (Symptom_Tracker_Display.Symcurrentdat.GetScore() > 19)
                {
                    if (!AppManager.Saanotfn.Contains("PLEASE TAKE SYMPTOM TRACKER TEST"))
                    {
                        AppManager.Saanotfn.Add("PLEASE TAKE SYMPTOM TRACKER TEST");
                    }
                    display.gameObject.SetActive(true);
                }
            }
            if(Asthma_control_display.asthmacurrentdat!=null)
            {
                if (Asthma_control_display.asthmacurrentdat.GetScore() >= 1)
                {
                    if (!AppManager.Saanotfn.Contains("PLEASE VISIT A PHYSICIAN"))
                    {
                        AppManager.Saanotfn.Add("PLEASE VISIT A PHYSICIAN");
                    }
                    display.gameObject.SetActive(true);
                }
            }
           
        }
        else if(AppManager.Current_mode=="CSU")
        {
            if(Urticaria_display.current_uasdat.GetScore()>=6)
            {
                Debug.Log("going");
                if(!AppManager.cusnotfn.Contains("PLEASE TAKE UAS TEST"))
                {
                    Debug.Log("saving");
                    AppManager.cusnotfn.Add("PLEASE TAKE UAS TEST");
                }
                display.gameObject.SetActive(true);
            }
           
        }
        transform.parent.gameObject.SetActive(false);

    }
}
