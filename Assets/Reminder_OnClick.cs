using UnityEngine.UI;
using UnityEngine;

public class Reminder_OnClick : MonoBehaviour {
    private Transform Parent;
    private int currentvalue;
    private void Start()
    {
        Parent = gameObject.transform.parent;
       // Debug.Log(Parent.GetComponent<Text>().text);
    }
    public void OnClick()
    {
        switch(Parent.name)
        {
            case "Hour":
                {
                    if(gameObject.name=="Up")
                    {
                        currentvalue = int.Parse(Parent.GetComponent<Text>().text);
                        currentvalue++;
                       
                        if (currentvalue>12)
                        {
                            currentvalue = 1;
                            Parent.GetComponent<Text>().text = 0.ToString()+currentvalue.ToString();

                        }
                        else
                        {
                            if(currentvalue<10)
                            {
                                Parent.GetComponent<Text>().text = 0.ToString() + currentvalue.ToString();
                            }
                            else
                            {
                                Parent.GetComponent<Text>().text = currentvalue.ToString();
                            }

                        }
                    }
                    else if(gameObject.name=="Down")
                    {
                        currentvalue = int.Parse(Parent.GetComponent<Text>().text);
                        currentvalue--;
                        if (currentvalue <1 )
                        {
                            currentvalue = 12;
                            Parent.GetComponent<Text>().text = currentvalue.ToString();

                        }
                        else
                        {
                            if(currentvalue<10)
                            {
                                Parent.GetComponent<Text>().text = 0.ToString()+currentvalue.ToString();
                            }
                            else
                            {
                                Parent.GetComponent<Text>().text = currentvalue.ToString();
                            }
                         
                         
                        }
                    }
                    break;
                }
            case "Minute":
                {
                    if (gameObject.name == "Up")
                    {
                        currentvalue = int.Parse(Parent.GetComponent<Text>().text);
                        currentvalue++;
                        if (currentvalue > 60)
                        {
                            currentvalue = 0;
                            Parent.GetComponent<Text>().text = 0.ToString()+currentvalue.ToString();

                        }
                        else
                        {
                            if (currentvalue < 10)
                            {
                                Parent.GetComponent<Text>().text = 0.ToString() + currentvalue.ToString();
                            }
                            else
                            {
                                Parent.GetComponent<Text>().text = currentvalue.ToString();
                            }

                        }

                    }
                    else if (gameObject.name == "Down")
                    {
                        currentvalue = int.Parse(Parent.GetComponent<Text>().text);
                        currentvalue--;
                        if (currentvalue < 0)
                        {
                            currentvalue = 60;
                            Parent.GetComponent<Text>().text = currentvalue.ToString();

                        }
                        else
                        {
                            if (currentvalue < 10)
                            {
                                Parent.GetComponent<Text>().text = 0.ToString() + currentvalue.ToString();
                            }
                            else
                            {
                                Parent.GetComponent<Text>().text = currentvalue.ToString();
                            }

                        }
                    }
                    break;
                }
            case "Day":
                {
                    if (gameObject.name == "Up")
                    {
                        if(Parent.GetComponent<Text>().text=="AM")
                        {
                            Parent.GetComponent<Text>().text = "PM";
                        }
                        else if(Parent.GetComponent<Text>().text == "PM")
                        {
                            Parent.GetComponent<Text>().text = "AM";
                        }

                      
                    }
                    else if (gameObject.name == "Down")
                    {
                        if (Parent.GetComponent<Text>().text == "AM")
                        {
                            Parent.GetComponent<Text>().text = "PM";
                        }
                        else if (Parent.GetComponent<Text>().text == "PM")
                        {
                            Parent.GetComponent<Text>().text = "AM";
                        }

                    }

                    break;
                }

        }


    }
}
