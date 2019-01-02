using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public class Calendar_OnClick : MonoBehaviour {

    // Use this for initialization
    [SerializeField]
    private GameObject highlight;
   // public List<string> dat;
    string key;
    int count;
    [SerializeField]private GameObject[] notfncard;
    [SerializeField]
    private Calander_Manager calander_Manager;
    [SerializeField] private Scrollbar csu, uas, saa, acq,xolairshot;
	void Start () {
        count = 1;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnClick()
    {
       // calander_Manager.Reset_reminder();
       // dat.Clear();
        highlight.transform.position = transform.position;
        Calander_Manager.Reminder_Day = int.Parse(gameObject.tag);
        // key = AppManager.Current_mode + "," + Calander_Manager.Reminder_Day.ToString() + "/" + Calander_Manager.Reminder_Month.ToString() + "/" + Calander_Manager.Reminder_Year.ToString() +","+ count.ToString();
        key = AppManager.Current_mode + "," +gameObject.tag.ToString()+ "/" + Calander_Manager.Current_Month.ToString() + "/" + Calander_Manager.Current_Year.ToString()  ;
        Debug.Log(key);
        if(PlayerPrefs.HasKey(key))
        {
            Debug.Log("coming");
            switch(AppManager.Current_mode)
            {
                case"CSU":
                    {
                        csu.value = 0;
                        xolairshot.value = 0;
                        uas.value = 0;
                       
                        break;
                    }
                case "SAA":
                    {
                        saa.value = 0;
                        acq.value = 0;
                        xolairshot.value = 0;
                        break;
                    }
            }
            foreach(GameObject x in notfncard)
            {
                x.SetActive(true);
              

                x.transform.GetChild(0).GetComponent<Text>().text = gameObject.tag.ToString();
                x.transform.GetChild(1).GetComponent<Text>().text = gameObject.transform.parent.name;
                string[] temp =PlayerPrefs.GetString(key).Split(',');
                x.transform.GetChild(2).GetComponent<Text>().text = temp[temp.Length - 2] + temp[temp.Length - 1];
                

            }
            DateTime Today = new DateTime(Calander_Manager.Current_Year, Calander_Manager.Current_Month, Int32.Parse(gameObject.tag));
            switch(AppManager.Current_mode)
            {
                case "CSU":
                    {
                        if(Today.DayOfYear==PlayerPrefs.GetInt("CSUtaken"))
                        {
                            csu.value = 1;
                            xolairshot.value = 1;
                        }
                        if(Today.DayOfYear==PlayerPrefs.GetInt("UAStaken"))
                        {
                            uas.value = 1;
                            xolairshot.value = 1;
                        }
                        break;
                    }
                case "SAA":
                    {
                        if(Today.DayOfYear==PlayerPrefs.GetInt("SAAtaken"))
                        {
                            saa.value = 1;
                            xolairshot.value = 1;
                        }
                        if(Today.DayOfYear==PlayerPrefs.GetInt("ACQtaken"))
                        {
                            acq.value = 1;
                            xolairshot.value = 1;
                        }
                        break;
                    }
            }
        }
        else
        {
            foreach (GameObject x in notfncard)
            {
                x.SetActive(false);
            }
        }
       // Debug.Log(key);
      //  Debug.Log(PlayerPrefs.GetString(key));
        //while(PlayerPrefs.HasKey(key))
        //{
           
        //    dat.Add(PlayerPrefs.GetString(key));
        //    Debug.Log("going"+dat[0]);

        //    count++;
        //    key = AppManager.Current_mode + "," + gameObject.tag + "/" + Calander_Manager.Current_Month.ToString() + "/" + Calander_Manager.Current_Year.ToString() ;

           
        //}
        //int x = Mathf.Clamp(dat.Count, 1, 3);
        //if (dat.Count != 0)
        //{
        //    for (int i = 0; i < dat.Count; i++)
        //    {
        //        string[] temp = dat[i].Split(',');
        //        string[] date = temp[1].Split('/');
        //        string[] time = temp[2].Split('/');
        //        if (i < calander_Manager.Reminder_list.Count)
        //        {

        //            calander_Manager.Reminder_list[i].transform.GetChild(0).GetComponent<Text>().text = date[0];
        //            calander_Manager.Reminder_list[i].transform.GetChild(2).GetComponent<Text>().text = temp[3];
        //            calander_Manager.Reminder_list[i].transform.GetChild(3).GetComponent<Text>().text = temp[2];
        //            calander_Manager.Reminder_list[i].SetActive(true);
        //        }


        //    }
        //}
            //count = 1;
    }
    private void set()
    {

    }
}
