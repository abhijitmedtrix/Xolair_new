using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageDisplay : MonoBehaviour {


    public TextAsset TextFile;

    public Text theText;
    public GameObject displayer;
    public string[] textLines;
    public GameObject textBox;

    public int CurrentLines;

    public int EndLines;
	// Use this for initialization
	void Start () {

        displayer.gameObject.SetActive(false);

        if(TextFile != null){

            textLines = (TextFile.text.Split('\n'));
        }

        if(EndLines == 0)
        {

            EndLines = textLines.Length - 1;

        }
		
	}


    public void OnClick()
    {

        displayer.gameObject.SetActive(true);

        theText.text = textLines[CurrentLines];


    }



}
