using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CSU_UI_OUT : MonoBehaviour {

    [SerializeField]
    private InputField inputField;
    [SerializeField]
    private InputValue inputValue;

    public Text  Answer_txt;
    [SerializeField]
    public GameObject answerparent;
    void Start () 
    {

	}
    public void set()
    {
        AppManager.Current_mode = "CSU";
        //Welcome_txt.text = "Hai " + AppManager.UserName;
    }

    public void ShowAnswer()
    {
        answerparent.SetActive(true);
        // exampleConversation.AskQuestion(inputField.text);

    }
    public void close()
    {
        answerparent.SetActive(false);
        answerparent.transform.GetChild(0).GetComponent<Text>().text = "";
    }
}
