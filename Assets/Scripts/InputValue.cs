using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MaterialUI;
public class InputValue : MonoBehaviour {

    public InputField Myname;
    public InputField age;
    //public InputField gender;
    public InputField password;
    public Text Name;
    public Text Age;
   // public Text Gender;
    [SerializeField]
    private ScreenManager screenManager;
    public Text CSUName;
    public Text CSUAge;
   // public Text CSUGender;
    
    private void Awake()
    {
        //PlayerPrefs.DeleteAll();
       
    }
    private void Start()
    {
        if (PlayerPrefs.HasKey("UserName") || PlayerPrefs.HasKey("Gender") || PlayerPrefs.HasKey("Age"))
        {
            AppManager.UserName = PlayerPrefs.GetString("UserName");
            Name.text = PlayerPrefs.GetString("UserName");
           //Age.text = PlayerPrefs.GetString("Age");
        //text = PlayerPrefs.GetString("Age");

            CSUName.text = PlayerPrefs.GetString("UserName");
           // CSUAge.text = PlayerPrefs.GetString("Age");
          //CSUGender.text = PlayerPrefs.GetString("Age");
            screenManager.Set(1);
        }
    }
    public void SetText()
    {
        AppManager.UserName = Myname.text.ToLower();
       // AppManager.Gender = gender.text.ToLower();
        AppManager.Age = age.text.ToLower();
        

        if(AppManager.UserName.Length>=3&&AppManager.Password==password.text.ToLower()&&AppManager.Age.Length!=0)

        {
            PlayerPrefs.SetString("UserName",AppManager.UserName);
           // PlayerPrefs.SetString("Gender",AppManager.Gender);
            PlayerPrefs.SetString("Age", AppManager.Age);
            Name.text = Myname.text;
            //Age.text = age.text;
           // Gender.text = gender.text;

            CSUName.text = Myname.text;
           // CSUAge.text = age.text;
           // CSUGender.text = gender.text;
            screenManager.Set(1);


        }


    }

	
}
