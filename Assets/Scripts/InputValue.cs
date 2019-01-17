using UnityEngine;
using UnityEngine.UI;
using MaterialUI;

public class InputValue : MonoBehaviour
{
    [SerializeField] private ScreenManager screenManager;

    public InputField Myname;

    public InputField age;

    //public InputField gender;
    public InputField password;
    public Text Name;
    public Text Age;

    // public Text Gender;
    public Text CSUName;

    public Text CSUAge;
    // public Text CSUGender;

    private void Start()
    {
        if (PlayerPrefs.HasKey("UserName") || PlayerPrefs.HasKey("Gender") || PlayerPrefs.HasKey("Age"))
        {
            AppManager.UserName = PlayerPrefs.GetString("UserName");
            Name.text = PlayerPrefs.GetString("UserName");
            CSUName.text = PlayerPrefs.GetString("UserName");
            screenManager.Set(1);
        }
        else if (Application.isEditor)
        {
            screenManager.Set(1);
        }
    }

    public void SetText()
    {
        AppManager.UserName = Myname.text.ToLower();

        // AppManager.Gender = gender.text.ToLower();
        AppManager.Age = age.text.ToLower();
        if (AppManager.UserName.Length >= 3 && AppManager.Password == password.text.ToLower() &&
            AppManager.Age.Length != 0)

        {
            PlayerPrefs.SetString("UserName", AppManager.UserName);
            // PlayerPrefs.SetString("Gender",AppManager.Gender);
            PlayerPrefs.SetString("Age", AppManager.Age);
            Name.text = Myname.text;
            CSUName.text = Myname.text;
            screenManager.Set(1);
        }
    }
}