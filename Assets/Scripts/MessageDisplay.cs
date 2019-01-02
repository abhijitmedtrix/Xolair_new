using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageDisplay : MonoBehaviour {


    public TextAsset[] TextFile;
   
    public Text theText;
    public GameObject displayer;
    public string[] sad,dull,happy,awesome;
    public GameObject textBox;

    public int CurrentLines;

    public int EndLines;
	// Use this for initialization
	void Start () {

        displayer.gameObject.SetActive(false);
        sad = (TextFile[0].text.Split('\n'));
        dull = (TextFile[1].text.Split('\n'));
        happy = (TextFile[2].text.Split('\n'));
        awesome = (TextFile[3].text.Split('\n'));

        


    }


    public void OnClick(string emotion)
    {

        displayer.gameObject.SetActive(true);

        //theText.text = sad[CurrentLines];
        switch(emotion)
        {
            case "sad":
                {
                    theText.text = sad[Random.Range(0, sad.Length - 1)];
                    break;
                }
            case "dull":
                {
                    theText.text = dull[Random.Range(0, dull.Length - 1)];
                    break;
                   
                }
            case "happy":
                {
                    theText.text = happy[Random.Range(0, happy.Length - 1)];
                    break;
                }
            case "awesome":
                {
                    theText.text = awesome[Random.Range(0, awesome.Length - 1)];
                    break;
                }

        }

    }



}
