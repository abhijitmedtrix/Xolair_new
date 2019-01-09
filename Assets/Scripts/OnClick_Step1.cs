using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClick_Step1 : MonoBehaviour {
    [SerializeField]
    private GameObject[] Head_butn,Middle_butn,Torso_butn;
    [SerializeField]
    private GameObject[] Images;

    

    public void OnClick(GameObject obj)
    {
        switch(obj.tag)
        {
            case "Head":
                {
                    for (int i = 0; i < Images.Length;i++)
                    {
                        if(i==0||i==3)
                        {
                            Images[i].SetActive(true);
                        }
                        else
                        {
                            Images[i].SetActive(false);
                        }

                    }
                    //Images[0].SetActive(true);
                    //Images[1].SetActive(false);
                    //Images[2].SetActive(false);
                    //Head_butn[0].SetActive(true);
                    //Head_butn[1].SetActive(false);
                    int x = 0;
                    for (int i = 0; i < Head_butn.Length;i++)
                    {
                        x++;
                      //  Debug.Log(x);
                        if(i==0||i==2)
                        {
                            Head_butn[i].SetActive(true);
                            Middle_butn[i].SetActive(false);
                            Torso_butn[i].SetActive(false);
                        }
                        else 
                        {
                            Head_butn[i].SetActive(false);
                            Middle_butn[i].SetActive(true);
                            Torso_butn[i].SetActive(true);

                        }

                    }

                    break;
                }
            case "Middle":
                {
                    for (int i = 0; i < Images.Length; i++)
                    {
                        if (i == 1 || i == 4)
                        {
                            Images[i].SetActive(true);
                        }
                        else
                        {
                            Images[i].SetActive(false);
                        }

                    }
                   
                    //Middle_butn[0].SetActive(true);
                    //Middle_butn[1].SetActive(false);
                    for (int i = 0; i < Head_butn.Length; i++)
                    {
                        if(i==0||i==2)
                        {
                            Middle_butn[i].SetActive(true);
                            Head_butn[i].SetActive(false);
                            Torso_butn[i].SetActive(false);

                        }
                        else
                        {
                            Middle_butn[i].SetActive(false);
                            Head_butn[i].SetActive(true);
                            Torso_butn[i].SetActive(true);

                        }
                      

                    }

                    break;
                }
            case "Torso":
                {
                    for (int i = 0; i < Images.Length; i++)
                    {
                        if (i == 2 || i == 5)
                        {
                            Images[i].SetActive(true);
                        }
                        else
                        {
                            Images[i].SetActive(false);
                        }

                    }
                    for (int i = 0; i < Head_butn.Length; i++)
                    {
                        if (i == 0||i==2)
                        {
                            Torso_butn[i].SetActive(true);
                            Head_butn[i].SetActive(false);
                            Middle_butn[i].SetActive(false);
                        }
                        else 
                        {
                            Torso_butn[i].SetActive(false);
                            Head_butn[i].SetActive(true);
                            Middle_butn[i].SetActive(true);
                        }
                       

                    }
                    break;
                }

        }


    }
}
