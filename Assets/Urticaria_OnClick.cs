using UnityEngine.UI;
using UnityEngine;

public class Urticaria_OnClick : MonoBehaviour {

    
    private Transform parent;
    private void Start()
    {
        parent = transform.parent;
    }
    public void Onclick()
    {

        for (int i = 0; i < parent.childCount; i++)
        {

            parent.GetChild(i).GetComponent<Toggle>().isOn = false;
        }
        gameObject.GetComponent<Toggle>().isOn = true;



    }
}
