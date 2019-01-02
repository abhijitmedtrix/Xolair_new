using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Csu_onclick : MonoBehaviour {
    public GameObject display;
    // Use this for initialization
    public void OnClick()
    {
        AppManager.FirstTest = true;

        if (AppManager.FirstTest && AppManager.SecondTest)
        {
            display.gameObject.SetActive(false);
        }
        else
        {
            display.gameObject.SetActive(true);
        }
       // transform.parent.gameObject.SetActive(false);

    }
}
