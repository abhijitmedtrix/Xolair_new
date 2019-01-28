using System.Collections;
using System.Collections.Generic;
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
        random = Random.Range(0, 12);
        if(slider.value>=0f&&slider.value<=0.033f)
        {
            slider.value = 0f;
            updateUi(0);
            message.text =insanelyGreat[random];
            
        }

        //2

        else if(slider.value>=0.033f&&slider.value<=0.118f)
        {
            slider.value = 0.066f;
            updateUi(1);
            message.text = great[random];
        }

        //3

        else if(slider.value>=0.118f&&slider.value<=0.23f)
        {
            slider.value = 0.17f;
            updateUi(2);
            message.text = veryGood[random];
        }

        //4

        else if(slider.value>=0.23f&&slider.value<=0.345f)
        {
            slider.value = 0.29f;
            updateUi(3);
            message.text = good[random];
        }

        //5

        else if(slider.value>=0.345f&&slider.value<=.455f)
        {
            slider.value = 0.4f;
            updateUi(4);
            message.text = okay[random];
        }

        //6

        else if(slider.value>=.455f&&slider.value<=.565f)
        {
            slider.value = .51f;
            updateUi(5);
            message.text = soSo[random];
        }

        //7

        else if(slider.value>=.565f&&slider.value<=.675f)
        {
            slider.value = .62f;
            updateUi(6);
            message.text = meh[random];
        }

        //8

        else if(slider.value>=.675f&&slider.value<=.79f)
        {
            slider.value = .73f;
            updateUi(7);
            message.text = bad[random];
        }

        //9

        else if(slider.value>=.79f&&slider.value<=0.925f)
        {
            slider.value = .85f;
            updateUi(8);
            message.text = veryBad[random];
        }
        else
        {
            slider.value = 1f;
            updateUi(9);
            message.text = couldnotWorse[random];
        }
    }
    private void updateUi(int indx)
    {
        smileyMood.sprite = smiley[indx];
        currentMood.text = moodTitle[indx];

    }
}
