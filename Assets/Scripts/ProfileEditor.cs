using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfileEditor : MonoBehaviour {

    public Text Nameinput, Namesave,Nameholder;
    public Text Ageinput,Ageholder;
    public InputField Name, Age;
    public Text Update_info;
    //public Text Genderinput,Gendersave,Genderholder;
    
	// Use this for initialization
	void Start () 
    {
        Nameinput.text = PlayerPrefs.GetString("UserName");
        Ageinput.text = PlayerPrefs.GetString("Age");
       // Genderinput.text = PlayerPrefs.GetString("Age");

	}
	
    public void Nameset()
    {
        PlayerPrefs.SetString("UserName",Nameholder.text);
        Namesave.text = PlayerPrefs.GetString("UserName");
    }
    //public void Genderset()
    //{
       
    //    PlayerPrefs.SetString("Gender", Genderholder.text);
    //    //Gendersave.text = PlayerPrefs.GetString("Gender");
    //}
    public void Ageset()
    {
        PlayerPrefs.SetString("Age", Ageholder.text);
        //Agesave.text = PlayerPrefs.GetString("Age");
    }
    public void submit()
    {
        Namesave.text = PlayerPrefs.GetString("UserName");
       // Gendersave.text = PlayerPrefs.GetString("Gender");
        //Agesave.text = PlayerPrefs.GetString("Age");
        Update_info.gameObject.SetActive(true);
        Invoke("delay", 1f);
     
    }
    public void show()
    {
        Name.text = null;
        Age.text = null;
        Update_info.gameObject.SetActive(false);
        Nameinput.text = PlayerPrefs.GetString("UserName");
        Ageinput.text = PlayerPrefs.GetString("Age");
     
        gameObject.SetActive(true);

    }
    public void delay()
    {
        gameObject.SetActive(false);
    }
}
 