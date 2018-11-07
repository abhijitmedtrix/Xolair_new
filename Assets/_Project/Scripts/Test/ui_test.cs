using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ui_test : MonoBehaviour {


    public InputField Name;
   
    public InputField Password;
    public static string Name_dat;
    public static string Password_dat;
	void Start () 
    {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnClick()
    {
        Name_dat = Name.text;
        Password_dat = Password.text;
        SceneManager.LoadScene(1);
        
    }
}
