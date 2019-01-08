using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class clickscr : MonoBehaviour {

    // Use this for initialization
    public GameObject cambuttn;
    public RawImage img;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void onclick()
    {
        cambuttn.SetActive(true);
        img.texture = null;
    }
}
