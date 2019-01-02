using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaqManager : MonoBehaviour {

    // Use this for initialization
    public List<GameObject> answers;
    public GameObject Question;
    public void set(int x)
    {
        Question.SetActive(false);

        for (int i = 0; i < answers.Count; i++)
        {
           
            answers[i].SetActive(false);
        }
        answers[x].SetActive(true);
    }
    public void OnAnswer(int x)
    {
        for (int i = 0; i < answers.Count; i++)
        {
            answers[i].SetActive(false);
        }
        answers[x].SetActive(true);
    }
    public void OnAnswer_tap()
    {
        for (int i = 0; i < answers.Count;i++)
        {
            answers[i].SetActive(false);
        }
        Question.SetActive(true);

    }
}
