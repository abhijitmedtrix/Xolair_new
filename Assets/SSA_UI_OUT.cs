using UnityEngine;
using UnityEngine.UI;

public class SSA_UI_OUT : MonoBehaviour {

    // Use this for initialization
    [SerializeField]
    private InputField inputField;
    [SerializeField]
    private InputValue inputValue;

    public Text  Answer_txt;
    [SerializeField]
    public GameObject answerparent;

   // public ExampleConversation exampleConversation;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
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
