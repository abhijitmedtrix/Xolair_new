
using UnityEngine;
using UnityEngine.UI;

public class Submit_ACT : MonoBehaviour {
    public GameObject option;
    public Asthma_control_display asthma_Control_Display;
	public void OnClick()
    {
        if (option.activeSelf)
        {
            Debug.Log("coming");
            for (int i = 0; i < option.transform.childCount; i++)
            {
                if (option.transform.GetChild(i).GetComponent<Toggle>().isOn)
                {
                    asthma_Control_Display.Next_Question(System.Int32.Parse(option.transform.GetChild(i).name));
                    option.transform.GetChild(i).GetComponent<Toggle>().isOn = false;

                }
            }
        }

    }
}
