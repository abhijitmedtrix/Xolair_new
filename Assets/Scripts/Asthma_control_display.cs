using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using App.Data.SSA;
using UnityEngine.UI;
using System;

public class Asthma_control_display : MonoBehaviour
{
    public static AsthmaData asthmacurrentdat;
    private AsthmaData asthmaData;
    [SerializeField]
    private List<SymptomData.QuestionData> Question_Data_List;
    SymptomData.QuestionData temp, Current_Question_dat;
    [SerializeField]
    private GameObject Score_panel;
    [SerializeField]
    private Text Question_text,Score_txt;
    [SerializeField]
    private Transform _child;
    [SerializeField] private Button acqButtn;
    [SerializeField] private MaterialUI.ScreenManager screenManager;
   

    [SerializeField]
    private GameObject Option_set_1;
   
    // Use this for initialization
    void Start () 
    {
        //PlayerPrefs.DeleteAll();
	}

    public void Set_Questions()
    {
       // if(PlayerPrefs.GetInt("ACQtaken")!=DateTime.Today.DayOfYear)
       // {
            asthmaData = TrackerManager.GetData(DateTime.Today, TrackerManager.TrackerType.Asthma) as AsthmaData;
            asthmaData.set_indx();
            Question_text.text = asthmaData.GetQuestion().question;
            Reset_buttn(Option_set_1);
           // PlayerPrefs.SetInt("ACQtaken", DateTime.Today.DayOfYear);
            screenManager.Set(5);
            //acqButtn.interactable = false;
       // }




    }
    public void Next_Question(int x)
    {
        try
        {
            var question = asthmaData.GetQuestion();

            Question_text.text = asthmaData.SetAnswer(question, x).question;
            Reset_buttn(Option_set_1);
        }
        catch(Exception ex)
        {
            if(ex!=null)
            {
                asthmacurrentdat = asthmaData;
                TrackerManager.UpdateEntry(DateTime.Today, asthmaData);
                string score= (asthmaData.GetScore()/36f).ToString();
                if(score.Length>1)
                {
                    Score_txt.text = score.Substring(0,3);
                   
                }
                else
                {
                    Score_txt.text = score;

                }
                //Score_txt.text =score[0].ToString();

                Score_panel.SetActive(true);
                AppManager.SecondTest = true;
            }
        }

        // current_question++;

    }
    public void Score_Controller(int indx)
    {

        //Total_Score += Question_Data_List[current_question].answersOption[indx].points;

    }

    private void Reset_buttn(GameObject obj)
    {
       // Debug.Log(obj.transform.childCount);
        for (int i = 0; i < obj.transform.childCount ; i++)
        {
            _child = obj.transform.GetChild(i);
            _child.GetComponent<Toggle>().isOn = false;
            //Debug.Log(_child.GetChild(1).name);

            _child.GetChild(1).GetComponent<Text>().text = "         "+asthmaData.GetQuestion().answersOption[i].description;
            //Debug.Log(i);

        }
    }

}
