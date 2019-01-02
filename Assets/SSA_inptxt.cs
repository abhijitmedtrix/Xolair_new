
using UnityEngine;
using UnityEngine.UI;

public class SSA_inptxt : MonoBehaviour {
    //[SerializeField]
    //private GameObject obj;
    [SerializeField] private Text question_Txt;
    [SerializeField] private ExampleConversation exampleConversation;
    [SerializeField] private InputField inputField;
    public void click()
    {
       // obj.SetActive(true);

        if (inputField.text.Length > 0)
        {
            question_Txt.text = inputField.text;
            exampleConversation.AskQuestion(inputField.text);
            inputField.text = "";
           
        }



            }
}
