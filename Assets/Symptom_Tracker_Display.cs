using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using App.Data.SSA;
using System;
using App.Data;
using UnityEngine.UI;

public class Symptom_Tracker_Display : MonoBehaviour
{
    public SymptomData symptomData;
    [SerializeField] private List<SymptomData.QuestionData> Question_Data_List;
    SymptomData.QuestionData temp, Current_Question_dat;

    [SerializeField] private GameObject Score_panel;
    [SerializeField] private Text Points_txt;
    [SerializeField] private Text Question_text;

    private int current_optn = 0, current_question = 0;

    public static int Total_Score;

    [SerializeField] private GameObject Button_set_1, Button_set_2;

    private bool Isclicked;

    // Use this for initialization
    void Start()
    {
        //symptomData = new SymptomData();

        // Debug.Log(Question_Data_List[3].question);
        //Debug.Log(Question_Data_List[3].answersOption[0].points);


        Set_Questions();
    }

    public void Set_Questions()
    {
        symptomData = TrackerManager.GetData(DateTime.Today, TrackerManager.TrackerType.Symptom) as SymptomData;
        Question_text.text = symptomData.GetQuestion().question;
        if (symptomData.GetQuestion().answersOption.Length == 3)
        {
            Reset_buttn(Button_set_2);
            Reset_buttn(Button_set_1);
            Button_set_1.SetActive(true);
            Button_set_2.SetActive(false);
        }
        else
        {
            Reset_buttn(Button_set_2);
            Reset_buttn(Button_set_1);
            Button_set_1.SetActive(false);
            Button_set_2.SetActive(true);
        }
    }

    public void Next_Question(int x)
    {
        // Debug.Log(x);

        //Debug.Log(temp.answersOption.Length);
        //Debug.Log(temp.question);
        var question = symptomData.GetQuestion();

        try
        {
            QuestionBasedTrackerData.QuestionData nextQuestion = symptomData.SetAnswer(question, x);
            Question_text.text = nextQuestion.question;

            if (nextQuestion.answersOption.Length == 3)
            {
                Reset_buttn(Button_set_2);
                Reset_buttn(Button_set_1);
                Button_set_1.SetActive(true);
                Button_set_2.SetActive(false);
            }
            else
            {
                Reset_buttn(Button_set_2);
                Reset_buttn(Button_set_1);
                Button_set_1.SetActive(false);
                Button_set_2.SetActive(true);
            }
        }
        catch (Exception ex)
        {
            TrackerManager.UpdateEntry(DateTime.Today, symptomData);
            Debug.Log("Score: "+symptomData.GetScore());
            Score_panel.SetActive(true);
            Points_txt.text = "Score: " + symptomData.GetScore();

            Debug.LogWarning(
                "Exception caught trying to get new question. Seems there is no more questions for this data. Ex: " +
                ex.Message);
        }
    }

    // public void Score_Controller(int indx)
    // {
    // Total_Score += Question_Data_List[current_question].answersOption[indx].points;
    // }

    private void Reset_buttn(GameObject obj)
    {
        for (int i = 0; i < obj.transform.childCount; i++)
        {
            obj.transform.GetChild(i).GetComponent<Toggle>().isOn = false;
        }
    }
}