using System.Collections.Generic;
using UnityEngine;
using App.Data.CSU;
using UnityEngine.UI;
using System;
using System.IO;
using App.Utils;

public class Urticaria_display : MonoBehaviour
{
    private UASData uasData;
    private List<UASData.QuestionData> Question_Data_List;
    UASData.QuestionData temp, Current_Question_dat;
    [SerializeField] private GameObject Score_panel;
    [SerializeField] private Text Points_txt;
    [SerializeField] private Text Question_text;

    private Transform _child;

    //public static int Total_Score;

    [SerializeField] private GameObject Option_set_1;

    // Use this for initialization
    void Start()
    {
        Set_Questions();
    }

    public void Set_Questions()
    {
        uasData = TrackerManager.GetData(DateTime.Today, TrackerManager.TrackerType.UAS) as UASData;
        Question_text.text = uasData.GetQuestion().question;
        Reset_buttn(Option_set_1);
    }

    public void Next_Question(int x)
    {
        try
        {
            var question = uasData.GetQuestion();

            Question_text.text = uasData.SetAnswer(question, x).question;


            Reset_buttn(Option_set_1);
        }
        catch (Exception ex)
        {
            if (ex != null)
            {
                TrackerManager.UpdateEntry(DateTime.Today, uasData);
                Score_panel.SetActive(true);
                Points_txt.text = "Score: " + uasData.GetScore().ToString();
            }
        }
    }


    private void Reset_buttn(GameObject obj)
    {
        // Debug.Log(obj.transform.childCount);
        for (int i = 0; i < obj.transform.childCount; i++)
        {
            _child = obj.transform.GetChild(i);
            _child.GetComponent<Toggle>().isOn = false;
            //Debug.Log(_child.GetChild(1).name);

            _child.GetChild(1).GetComponent<Text>().text = uasData.GetQuestion().answersOption[i].description;
        }
    }
}