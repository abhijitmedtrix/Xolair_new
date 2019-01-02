using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpandCollapse : MonoBehaviour {


    public GameObject Expand;

    public GameObject Shrink;

    public GameObject ExpandUI;

    public GameObject CollapsedUI;

	// Use this for initialization
	void Start () {

        Expand.gameObject.SetActive(false);
        ExpandUI.gameObject.SetActive(false);
		
	}


    public void OnCollapse(){


        Shrink.gameObject.SetActive(false);
        CollapsedUI.gameObject.SetActive(false);


        Expand.gameObject.SetActive(true);
        ExpandUI.gameObject.SetActive(true);


    }



    public void OnExpand()
    {


        Shrink.gameObject.SetActive(true);
        CollapsedUI.gameObject.SetActive(true);


        Expand.gameObject.SetActive(false);
        ExpandUI.gameObject.SetActive(false);


    }

}
