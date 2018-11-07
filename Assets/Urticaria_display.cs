
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
    [SerializeField]
    private GameObject Score_panel;
    [SerializeField]
    private Text Points_txt;
    [SerializeField]
    private Text Question_text;
  
    private Transform _child;

    //public static int Total_Score;

    [SerializeField]
    private GameObject Option_set_1;
    // Use this for initialization
    void Start()
    {
        Set_Questions();
    }

    public void Set_Questions()
    {

        uasData = new UASData();
        Current_Question_dat = uasData.GetQuestion();
        Question_Data_List = uasData.questionDataList;
        Question_text.text = Question_Data_List[0].question;
        Reset_buttn(Option_set_1);



    }
    public void Next_Question(int x)
    {
        try
        {
            temp = uasData.GetQuestion();
            //Debug.Log(temp.question);
            Current_Question_dat = uasData.SetAnswer(temp, x);
            //Debug.Log(uasData.GetScore());
            Question_text.text = Current_Question_dat.question;
           // Debug.Log("coming");
            Reset_buttn(Option_set_1);
        }
        catch(Exception ex)
        {
           if(ex!=null)
            {
                JSONObject jSONObject=uasData.FormatToJson();
                string Data_file =jSONObject.Print();
                //Debug.Log(jSONObject.Count);
                //Debug.Log(jSONObject.GetField("date"));
                //Debug.Log(jSONObject.Print());
                //Debug.Log(jSONObject.GetField("option"));
                //Debug.Log("no questions");
              
                Score_panel.SetActive(true);
                Points_txt.text ="Score: "+uasData.GetScore().ToString() ;
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
        Score_panel.SetActive(false);
        // Debug.Log(obj.transform.childCount);
        for (int i = 0; i < obj.transform.childCount; i++)
        {
            _child = obj.transform.GetChild(i);
            _child.GetComponent<Toggle>().isOn = false;
            //Debug.Log(_child.GetChild(1).name);

            _child.GetChild(1).GetComponent<Text>().text = Current_Question_dat.answersOption[i].description;


        }
    }
}
