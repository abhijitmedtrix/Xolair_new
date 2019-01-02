using System;
using UnityEngine;
using UnityEngine.UI;
public class Trackerclick : MonoBehaviour 
{
    [SerializeField] private Button [] ui;
    public void Onclick()
    {
        Debug.Log("coming");
        Debug.Log(PlayerPrefs.GetInt("CSUtaken"));
        if (AppManager.Current_mode == "CSU")
        {
            if (PlayerPrefs.GetInt("CSUtaken") == DateTime.Today.DayOfYear)
            {
                if (ui[0].gameObject != null)
                {
                    ui[0].interactable = false;

                }

            }
            if (PlayerPrefs.GetInt("UAStaken") == DateTime.Today.DayOfYear)
            {
                if (ui[1].gameObject != null)
                {
                    ui[1].interactable = false;

                }
            }
        }
        else if (AppManager.Current_mode == "SAA")
        {
            if (PlayerPrefs.GetInt("SAAtaken") == DateTime.Today.DayOfYear)
            {
                if (ui[2].gameObject != null)
                {
                    ui[2].interactable = false;

                }
            }
            if (PlayerPrefs.GetInt("ACQtaken") == DateTime.Today.DayOfYear)
            {
                if (ui[3].gameObject != null)
                {
                    ui[3].interactable = false;

                }
            }
        }
    }
	}

