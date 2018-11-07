using UnityEngine.UI;
using UnityEngine;


public class CSU_Display : MonoBehaviour {
    [SerializeField]
    private Slider Itch_slider;
    [SerializeField]
    private Slider Hive_slider;
    [SerializeField]
    private int value;
    [SerializeField]
    private GameObject[] Head_Itches, Middle_Itches,Torso_Itches;
    [SerializeField]
    private GameObject[] Images,Images1; 
    [SerializeField]
    private GameObject[] Head_Hives,Middle_Hives,Torso_Hives;
    [SerializeField]
    private GameObject[] Buttons_1, Buttons_2;
    [SerializeField]
    private int X;
    [SerializeField]
    private enum Select_Mode
    {
        None,
        Mild,
        Moderate,
        Severe
    };
   
    Select_Mode Mode;
    void Start()
    {
        Mode = Select_Mode.None;

    }

    public void Reset()
    {
       

    }
    public void Next(GameObject obj)
    {
        switch(obj.tag)
        {
            case "next_1":
                {

                    break;
                }

            case "next_2":
                {

                    break;
                }
            case "next_3":
                {

                    break;
                }

        }

    }
    public void OnSliderExit_1()
    {
        X = 0;

        if (Itch_slider.value >= 0f && Itch_slider.value < 0.18f)
        {
            Mode = Select_Mode.None;
           

        }
        else if (Itch_slider.value > 0.18f && Itch_slider.value < 0.5f)
        {
            Mode = Select_Mode.Mild;

        }
        else if (Itch_slider.value > 0.5f && Itch_slider.value < 0.83f)
        {
            Mode = Select_Mode.Moderate;

        }
        else if (Itch_slider.value > 0.83f)
        {

            Mode = Select_Mode.Severe;
        }

        switch (Mode)
        {
            case Select_Mode.None:
                {
                    Itch_slider.value = 0f;
                    if (Images[0].active)
                    {
                        foreach(GameObject g in Head_Itches)
                        {
                            g.SetActive(false);

                        }
                    }
                    else if (Images[1].active)
                    {
                        foreach (GameObject g in Middle_Itches)
                        {
                            g.SetActive(false);

                        }
                    }
                    else if (Images[2].active)
                    {
                        foreach (GameObject g in Torso_Itches)
                        {
                            g.SetActive(false);

                        }
                    }
                    break;
                }
            case Select_Mode.Mild:
                {
                    Itch_slider.value = 0.36f;
                    if (Images[0].active)
                    {
                        for (int i = 0; i<Head_Itches.Length;i++)
                        {
                            if (i == 0)
                            {
                                Head_Itches[i].SetActive(true);
                            }
                            else
                            {
                                Head_Itches[i].SetActive(false);
                            }

                        }
                    }
                    else if (Images[1].active)
                    {
                        for (int i = 0; i < Middle_Itches.Length; i++)
                        {
                            if (i == 0||i== 3)
                            {
                                Middle_Itches[i].SetActive(true);
                            }
                            else
                            {
                                Middle_Itches[i].SetActive(false);
                            }

                        }
                    }
                    else if (Images[2].active)
                    {
                        for (int i = 0; i < Torso_Itches.Length;i++)
                        {
                            if(i==0)
                            {
                                Torso_Itches[i].SetActive(true);
                            }
                            else
                            {
                                Torso_Itches[i].SetActive(false);
                            }
                        }
                    }

                    break;
                }
            case Select_Mode.Moderate:
                {
                    Itch_slider.value = 0.66f;
                    if (Images[0].active)
                    {
                        for (int i = 0; i < Head_Itches.Length; i++)
                        {
                            if (i==1)
                            {
                                Head_Itches[i].SetActive(true);
                            }
                            else
                            {
                                Head_Itches[i].SetActive(false);
                            }

                        }
                    }
                    else if (Images[1].active)
                    {
                        for (int i = 0; i < Middle_Itches.Length; i++)
                        {
                            if ( i ==1||i==4)
                            {
                                Middle_Itches[i].SetActive(true);
                            }
                            else
                            {
                                Middle_Itches[i].SetActive(false);
                            }

                        }
                    }
                    else if (Images[2].active)
                    {
                        for (int i = 0; i < Torso_Itches.Length; i++)
                        {
                            if (i==1)
                            {
                                Torso_Itches[i].SetActive(true);
                            }
                            else
                            {
                                Torso_Itches[i].SetActive(false);
                            }
                        }
                    }
                    break;
                }
            case Select_Mode.Severe:
                {
                    Itch_slider.value = 1f;
                    if (Images[0].active)
                    {
                        for (int i = 0; i < Head_Itches.Length; i++)
                        {
                            if( i==2)
                            {
                                Head_Itches[i].SetActive(true);
                            }
                            else
                            {
                                Head_Itches[i].SetActive(false);
                            }
                         
                        }
                    }
                    else if (Images[1].active)
                    {
                        for (int i = 0; i < Middle_Itches.Length; i++)
                        {
                            if(i==2||i==5)
                            {
                                Middle_Itches[i].SetActive(true);
                            }
                           
                            else
                            {
                                Middle_Itches[i].SetActive(false);
                            }
                        }
                    }
                    else if (Images[2].active)
                    {
                        for (int i = 0; i < Torso_Itches.Length; i++)
                        {
                            if(i==2)
                            {
                                Torso_Itches[i].SetActive(true);
                            }
                            else
                            {
                                Torso_Itches[i].SetActive(false);
                            }

                        }
                    }
                    break;
                }

        }
    }

    public void OnSliderExit_2()
    {



       // Debug.Log(Hive_slider.value);
        if (Hive_slider.value >= 0f && Hive_slider.value < 0.18f)
        {
            Mode = Select_Mode.None;

        }
        else if (Hive_slider.value > 0.18f && Hive_slider.value < 0.5f)
        {
            Mode = Select_Mode.Mild;

        }
        else if (Hive_slider.value > 0.5f && Hive_slider.value < 0.83f)
        {
            Mode = Select_Mode.Moderate;

        }
        else if (Hive_slider.value > 0.83f)
        {

            Mode = Select_Mode.Severe;
        }

        switch (Mode)
        {
            case Select_Mode.None:
                {
                    Hive_slider.value = 0f;
                    if (Images1[0].active)
                    {
                        for (int i = 0; i < Head_Hives.Length; i++)
                        {
                            Head_Hives[i].SetActive(false);

                        }
                    }
                    else if (Images1[1].active)
                    {
                        for (int i = 0; i < Middle_Hives.Length; i++)
                        {
                            Middle_Hives[i].SetActive(false);
                        }
                    }
                    else if (Images1[2].active)
                    {
                        for (int i = 0; i < Torso_Hives.Length; i++)
                        {
                            Torso_Hives[i].SetActive(false);

                        }
                    }
                    break;
                }
            case Select_Mode.Mild:
                {
                    Hive_slider.value = 0.36f;
                    if (Images1[0].active)
                    {
                        for (int i = 0; i < Head_Hives.Length; i++)
                        {
                            if(i<2)
                            {
                                Head_Hives[i].SetActive(true);
                            }
                            else
                            {
                                Head_Hives[i].SetActive(false);

                            }

                        }
                    }
                    else if (Images1[1].active)
                    {
                        for (int i = 0; i < Middle_Hives.Length; i++)
                        {
                            if(i<3)
                            {
                                Middle_Hives[i].SetActive(true);
                            }
                            else if(i>=(Middle_Hives.Length/2)&&i<((Middle_Hives.Length/2)+3))
                            {
                                Middle_Hives[i].SetActive(true);

                            }
                            else
                            {
                                Middle_Hives[i].SetActive(false);
                            }
                        }
                    }
                    else if (Images1[2].active)
                    {
                        for (int i = 0; i < Torso_Hives.Length; i++)
                        {
                            if (i < 2)
                            {
                                Torso_Hives[i].SetActive(true);
                            }
                            else if (i >= (Middle_Hives.Length / 2) && i < ((Middle_Hives.Length / 2) + 2))
                            {
                                Torso_Hives[i].SetActive(true);

                            }
                            else
                            {
                                Torso_Hives[i].SetActive(false);
                            }

                        }
                    }

                    break;
                }
            case Select_Mode.Moderate:
                {
                    Hive_slider.value = 0.66f;
                    if (Images1[0].active)
                    {
                        for (int i = 0; i < Head_Hives.Length; i++)
                        {
                            if (i < 4)
                            {
                                Head_Hives[i].SetActive(true);
                            }
                            else
                            {
                                Head_Hives[i].SetActive(false);

                            }

                        }
                    }
                    else if (Images1[1].active)
                    {
                        for (int i = 0; i < Middle_Hives.Length; i++)
                        {
                            if (i < 6)
                            {
                                Middle_Hives[i].SetActive(true);
                            }
                            else if (i >= (Middle_Hives.Length / 2) && i < ((Middle_Hives.Length / 2) + 6))
                            {
                                Middle_Hives[i].SetActive(true);

                            }
                            else
                            {
                                Middle_Hives[i].SetActive(false);
                            }
                        }
                    }
                    else if (Images1[2].active)
                    {
                        for (int i = 0; i < Torso_Hives.Length; i++)
                        {
                            if (i < 4)
                            {
                                Torso_Hives[i].SetActive(true);
                            }
                            else if (i >= (Torso_Hives.Length / 2) && i < ((Torso_Hives.Length / 2) + 4))
                            {
                                Torso_Hives[i].SetActive(true);

                            }
                            else
                            {
                                Torso_Hives[i].SetActive(false);
                            }

                        }
                    }
                    break;
                }
            case Select_Mode.Severe:
                {
                    Hive_slider.value = 1f;
                    if (Images1[0].active)
                    {
                        for (int i = 0; i < Head_Hives.Length; i++)
                        {
                            Head_Hives[i].SetActive(true);

                        }
                    }
                    else if (Images1[1].active)
                    {
                        for (int i = 0; i < Middle_Hives.Length; i++)
                        {
                            Middle_Hives[i].SetActive(true);
                        }
                    }
                    else if (Images1[2].active)
                    {
                        for (int i = 0; i < Torso_Hives.Length; i++)
                        {
                            Torso_Hives[i].SetActive(true);

                        }
                    }
                    break;
                }

        }
    }


}
