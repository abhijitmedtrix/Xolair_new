using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputValue : MonoBehaviour {

    public InputField Myname;
    public InputField age;
    public InputField gender;

    public Text Name;
    public Text Age;
    public Text Gender;

    public Text CSUName;
    public Text CSUAge;
    public Text CSUGender;
    
    public void SetText()
    {
        
        Name.text = Myname.text;
        Age.text = age.text;
        Gender.text = gender.text;

        CSUName.text = Myname.text;
        CSUAge.text = age.text;
        CSUGender.text = gender.text;

    }

	
}
