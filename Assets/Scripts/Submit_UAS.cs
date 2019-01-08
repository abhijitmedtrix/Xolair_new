using UnityEngine.UI;
using UnityEngine;

public class Submit_UAS : MonoBehaviour {

    public GameObject option;
    public Urticaria_display urticaria_Display;
    public void OnClick()
    {
        if (option.activeSelf)
        {
            //Debug.Log("coming");
            for (int i = 0; i < option.transform.childCount; i++)
            {
                if (option.transform.GetChild(i).GetComponent<Toggle>().isOn)
                {
                    urticaria_Display.Next_Question(System.Int32.Parse(option.transform.GetChild(i).name));
                    option.transform.GetChild(i).GetComponent<Toggle>().isOn = false;

                }
            }
        }

    }
}
