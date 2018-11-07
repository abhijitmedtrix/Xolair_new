using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Csu_OnClick : MonoBehaviour {

    // Use this for initialization
    public CSU_Display csu_display;
public void OnClick()
    {
        csu_display.Next(gameObject);

    }
}
