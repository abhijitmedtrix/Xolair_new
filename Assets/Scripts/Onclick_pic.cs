using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Onclick_pic : MonoBehaviour {

    // Use this for initialization
    [SerializeField]
    private GameObject[] buttons;
    [SerializeField] private Cam_Control cam_Control;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnClick()
    {
        // Debug.Log("coming");
        cam_Control.Setbutn(gameObject);
        if (gameObject.name=="0")
        {
            
            buttons[1].SetActive(true);
            gameObject.SetActive(false);
        }
        else if(gameObject.name=="1")
        {
            buttons[0].SetActive(true);
            gameObject.SetActive(false);
        }
        //else if (gameObject.name == "2")
        //{
        //    buttons[3].SetActive(true);
        //    gameObject.SetActive(false);
        //}
        //else if (gameObject.name == "3")
        //{
        //    buttons[2].SetActive(true);
        //    gameObject.SetActive(false);
        //}
     
    }

}
