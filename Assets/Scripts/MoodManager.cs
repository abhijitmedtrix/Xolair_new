using System;
using UnityEngine;
using UnityEngine.UI;

public class MoodManager : MonoBehaviour {
    [SerializeField]
    private string[] insanelyGreat, great, veryGood, good, okay, soSo, meh, bad, veryBad, couldnotWorse,moodTitle;
    [SerializeField]
    private Sprite[] smiley;
    [SerializeField]
    private Text currentMood,message ;
    [SerializeField]
    private Image smileyMood;
    [SerializeField]
    private Slider slider;
    private int random;

    public void setMood()
    {

        //1
        random = UnityEngine.Random.Range(0, 12);
        if(slider.value>=0f&&slider.value<=0.033f)
        {
            slider.value = 0f;
            updateUi(0);
            message.text =insanelyGreat[random];
            saveMood(0);
            
        }

        //2

        else if(slider.value>=0.033f&&slider.value<=0.118f)
        {
            slider.value = 0.066f;
            updateUi(1);
            message.text = great[random];
            saveMood(1);
        }

        //3

        else if(slider.value>=0.118f&&slider.value<=0.23f)
        {
            slider.value = 0.17f;
            updateUi(2);
            message.text = veryGood[random];
            saveMood(2);
        }

        //4

        else if(slider.value>=0.23f&&slider.value<=0.345f)
        {
            slider.value = 0.29f;
            updateUi(3);
            message.text = good[random];
            saveMood(3);
        }

        //5

        else if(slider.value>=0.345f&&slider.value<=.455f)
        {
            slider.value = 0.4f;
            updateUi(4);
            message.text = okay[random];
            saveMood(4);
        }

        //6

        else if(slider.value>=.455f&&slider.value<=.565f)
        {
            slider.value = .51f;
            updateUi(5);
            message.text = soSo[random];
            saveMood(5);
        }

        //7

        else if(slider.value>=.565f&&slider.value<=.675f)
        {
            slider.value = .62f;
            updateUi(6);
            message.text = meh[random];
            saveMood(6);
        }

        //8

        else if(slider.value>=.675f&&slider.value<=.79f)
        {
            slider.value = .73f;
            updateUi(7);
            message.text = bad[random];
            saveMood(7);
        }

        //9

        else if(slider.value>=.79f&&slider.value<=0.925f)
        {
            slider.value = .85f;
            updateUi(8);
            message.text = veryBad[random];
            saveMood(8);
        }
        else
        {
            slider.value = 1f;
            updateUi(9);
            message.text = couldnotWorse[random];
            saveMood(9);
        }
    }
    private void updateUi(int indx)
    {
        smileyMood.sprite = smiley[indx];
        currentMood.text = moodTitle[indx];

    }
    private void saveMood(int value)
    {
        Debug.Log(DateTime.Today.Day.ToString() + DateTime.Today.Month.ToString() + DateTime.Today.Year);
        PlayerPrefs.SetInt(DateTime.Today.Day.ToString() + DateTime.Today.Month.ToString() + DateTime.Today.Year, value);
    }
}
