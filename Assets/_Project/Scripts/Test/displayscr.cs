using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class displayscr : MonoBehaviour {

    // Use this for initialization
    public Text name;
    public Text pass;

	void Start () 
    {
        name.text = ui_test.Name_dat;
        pass.text = ui_test.Password_dat;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
