using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using App.Data.SSA;
using System;
using UnityEngine.UI;

public class Symptom_Tracker_Display : MonoBehaviour {
    public SymptomData symptomData;
    [SerializeField]
    private List<SymptomData.QuestionData> Question_Data_List;
    SymptomData.QuestionData temp,Current_Question_dat;

    [SerializeField]
    private Text Question_text;
    private int current_optn=0,current_question=0;
    
    public static int Total_Score;

    [SerializeField]
    private GameObject  Button_set_1,Button_set_2;

    private bool Isclicked;
	// Use this for initialization
	void Start () {
        
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
        try
        {
            var question = symptomData.GetQuestion();

            Question_text.text = symptomData.SetAnswer(question, x).question;

            if (symptomData.SetAnswer(question, x).answersOption.Length == 3)
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
        catch(Exception ex)
        {
            if(ex!=null)
            {
                TrackerManager.UpdateEntry(DateTime.Today, symptomData);
                Debug.Log("coming");
                Debug.Log(symptomData.GetScore());

            }

        }
               
   
      }
    //public void Score_Controller(int indx)
    //{

    //    Total_Score+=Question_Data_List[current_question].answersOption[indx].points;

    //}

    private void Reset_buttn(GameObject obj)
    {
        for (int i = 0; i < obj.transform.childCount ;i++)
        {
            obj.transform.GetChild(i).GetComponent<Toggle>().isOn = false;

        }
    }
    // Update is called once per frame
    void Update () 
    {
       
	}
}
