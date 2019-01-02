using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Onclick_score : MonoBehaviour {
    public GameObject display;
    public void OnClick()
    {
        if(AppManager.FirstTest&&AppManager.SecondTest)
        {
            display.gameObject.SetActive(false);
        }
        else
        {
            display.gameObject.SetActive(true);
        }
        transform.parent.gameObject.SetActive(false);

    }
}
