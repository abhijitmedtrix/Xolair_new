using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Show_Images : MonoBehaviour {

    // Use this for initialization
    [SerializeField] public  int currentindx=0;
    [SerializeField] private RawImage rawImage;
	void Start () 
    {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public  void set()
    {
        rawImage.texture = AppManager.Images[0];
        currentindx = 0;
    }
    public void next()
    {
        currentindx++;
        if (currentindx < AppManager.Images.Count)
        {
            rawImage.texture = AppManager.Images[currentindx];

        }
        else
        {
            currentindx = AppManager.Images.Count - 1;
        }
    }
    public void back()
    {
        currentindx--;
        if (currentindx >=0)
        {
            rawImage.texture = AppManager.Images[currentindx];

        }
        else
        {
            currentindx = 0;
        }

    }
}
