using UnityEngine.UI;
using UnityEngine;

public class onclick : MonoBehaviour {

    // Use this for initialization
    public Symptom_Tracker_Display symptom_display;
 
    [SerializeField]
    private Transform parent;
    private void Start()
    {
        parent = transform.parent;
    }
    public void Onclick()
    {

        for (int i = 0; i < parent.childCount;i++)
        {

            parent.GetChild(i).GetComponent<Toggle>().isOn=false;
        }
        gameObject.GetComponent<Toggle>().isOn = true;
        

    }
}
