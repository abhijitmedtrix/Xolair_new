using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gazetest : MonoBehaviour {

    // Use this for initialization
    public Material mat;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnMouseEnter()
    {
        Debug.Log("enter");
        mat.color = Color.blue;
    }

    public void OnMouseExit()
    {
        Debug.Log("leave");
        mat.color = Color.white;
    }
}
