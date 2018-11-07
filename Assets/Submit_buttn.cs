using UnityEngine.UI;
using UnityEngine;

public class Submit_buttn : MonoBehaviour
{
    public GameObject[] option;
    public Symptom_Tracker_Display symptom_Tracker_Display;
    public void Onclick( )
    {
        if (option[0].activeSelf)
        {
            for (int i = 0; i < option[0].transform.childCount; i++)
            {
                if (option[0].transform.GetChild(i).GetComponent<Toggle>().isOn)
                {
                    symptom_Tracker_Display.Next_Question(System.Int32.Parse(option[0].transform.GetChild(i).name));
                    option[0].transform.GetChild(i).GetComponent<Toggle>().isOn = false;

                }
            }
        }
        else if(option[1].activeSelf)
        {
            for (int i = 0; i < option[1].transform.childCount; i++)
            {
                if (option[1].transform.GetChild(i).GetComponent<Toggle>().isOn)
                {
                    symptom_Tracker_Display.Next_Question(System.Int32.Parse(option[1].transform.GetChild(i).name));
                    option[1].transform.GetChild(i).GetComponent<Toggle>().isOn = false;
                }

            }

        }

        }


}
