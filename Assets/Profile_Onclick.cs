using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Profile_Onclick : MonoBehaviour {
    [SerializeField]private GameObject gameobj;
    public void OnClick()
    {
        if(!gameobj.active)
        {
            gameobj.SetActive(true);
        }
        else
        {
            gameobj.SetActive(false);
        }
    }
}
