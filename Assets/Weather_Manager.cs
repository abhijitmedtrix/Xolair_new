using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public class Weather_Manager : MonoBehaviour
{
    
    // Use this for initialization
    public List<JSONObject> Temp_json, Forecast_data;
    public DateTime datetime;
    private int temp_day, x,hour;
    [SerializeField]
    private Text City_Name, Temperature, Date;
    [SerializeField]
    private GameObject[] Forecast_obj;
    [SerializeField]
    private Text[] Currentinfo;
    [SerializeField]
    private Text Current_descrptn;
    [SerializeField]
    private string Current_temp;
    [SerializeField]
    private Text[] CSUtx_wthr,SAAtx_wthr;
    [SerializeField]
    private string lat, lon;
    void Start()
    {

        Forecast_data = new List<JSONObject>();
        StartCoroutine(Current_weather());
        temp_day = DateTime.Now.Day;
        StartCoroutine(Forecast_Weather());
        hour = DateTime.Now.Hour / 3;

      //  Debug.Log(hour);
    }
    private class weather
    {
        public int id;
        public string main;
        public string description;
        public string icon;

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.X))
        {
            Debug.Log(Forecast_data.Count);
        }

    }
    IEnumerator Current_weather()
    {
        if (!Input.location.isEnabledByUser)
            yield break;

        // Start service before querying location
        Input.location.Start();

        // Wait until service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        //// Service didn't initialize in 20 seconds
        //if (maxWait < 1)
        //{
        //    print("Timed out");
        //    yield break;
        //}

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            print("Unable to determine device location");
            yield break;
        }
        else
        {
            lat = Input.location.lastData.latitude.ToString();
            lon = Input.location.lastData.longitude.ToString();
            // Access granted and location value could be retrieved
            print("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
        }
        WWW weathhercall = new WWW("http://api.openweathermap.org/data/2.5/weather?lat=" + lat + "&lon=" + lon + "&units=metric&mode=json&appid=5de13b9151d283cec75a02e7f177fef4");
        yield return weathhercall;
       // Debug.Log (weathhercall.text);
        JSONObject jsonobj = new JSONObject(weathhercall.text);
        Debug.Log (jsonobj.GetField("main").GetField("temp"));

        City_Name.text = jsonobj.GetField("name").ToString().Replace('"', ' ');
        if(jsonobj.GetField("main").GetField("temp").ToString().Length>=3)
        {
            Current_temp = jsonobj.GetField("main").GetField("temp").ToString().Remove(2) ;
        }
        else
        {
            Current_temp = jsonobj.GetField("main").GetField("temp").ToString();
        }

                Temperature.text = Current_temp + "°" + " " + "C";

                Currentinfo[0].text = Current_temp + "°" + " " + "C";
        //if (jsonobj.GetField("main").GetField("temp").ToString().Length >= 4)
        //{
        //    Debug.Log(jsonobj.GetField("main").GetField("temp").ToString());
        //    Current_temp = jsonobj.GetField("main").GetField("temp").ToString().Remove(3) + "  " + "C";
        //    Temperature.text = Current_temp;

        //    Currentinfo[0].text = Current_temp;

        //}
        //else
        //{
        //    Current_temp = jsonobj.GetField("main").GetField("temp").ToString() + "  " + "C";
        //    Temperature.text = Current_temp;
        //    Currentinfo[0].text = Current_temp;
        //}
        //string check;
        Date.text = DateTime.Now.DayOfWeek + "," + DateTime.Now.ToString("MMMM") + "  " + DateTime.Now.Day;

            Currentinfo[1].text = DateTime.Now.Day + " " + DateTime.Now.ToString("MMMM");
            Currentinfo[2].text = DateTime.Now.DayOfWeek.ToString().Remove(3);
         //  Debug.Log(jsonobj.GetField("weather").ToString());
        string[] temp_array = jsonobj.GetField("weather").ToString().Split(',');
        temp_array = temp_array[2].Split(':');
        
       Current_descrptn.text = temp_array[1].Replace('"',' ');
        CSUtx_wthr[0].text= temp_array[1].Replace('"', ' ');
        CSUtx_wthr[1].text = Current_temp+ "°"+" "+"C";
        SAAtx_wthr[0].text = temp_array[1].Replace('"', ' ');
        SAAtx_wthr[1].text = Current_temp + "°" + " " + "C";
    }



    IEnumerator Forecast_Weather()
    {
        if (!Input.location.isEnabledByUser)
            yield break;

        // Start service before querying location
        Input.location.Start();

        // Wait until service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        //// Service didn't initialize in 20 seconds
        //if (maxWait < 1)
        //{
        //    print("Timed out");
        //    yield break;
        //}

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            print("Unable to determine device location");
            yield break;
        }
        else
        {
            lat = Input.location.lastData.latitude.ToString();
            lon = Input.location.lastData.longitude.ToString();
            // Access granted and location value could be retrieved
           print("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
        }
        WWW weathhercall = new WWW("http://api.openweathermap.org/data/2.5/forecast?lat="+lat+"&lon="+lon+"&units=metric&appid=5de13b9151d283cec75a02e7f177fef4");
        yield return weathhercall;
      //  Debug.Log(weathhercall.text);
        JSONObject jsonobj = new JSONObject(weathhercall.text);
        //Debug.Log (jsonobj.GetField("list").list[0]);
        Temp_json = jsonobj.GetField("list").list;
       // Debug.Log(Temp_json.Count);
        foreach (JSONObject json in Temp_json)
        {
            //Debug.Log(json.GetField ("dt_txt"));
            string s = json.GetField("dt_txt").ToString();

            string[] date = s.Split(' ');
            //Debug.Log (date[0]);
            date[0] = date[0].Replace('"', ' ');
            string[] day_st = date[0].Split('-');
            //Debug.Log (day_st[2]);
            int day = int.Parse(day_st[2]);
            if (temp_day != day)
            {
                x++;
                if (x == hour)
                {
                    Forecast_data.Add(json);
                    if(hour==8)
                    {
                        x = 0;
                    }

                }
                else if (x == 8)
                {
                    x = 0;
                }

            }

        }
        for (int i = 0,x=0; i < 3;i++) 
        {

            x++;
            Forecast_obj[i].transform.GetChild(0).GetComponent<Text>().text = DateTime.Now.AddDays(x).DayOfWeek.ToString();
            string[] eee1 = Forecast_data[i].GetField("weather").ToString().Split(',');
            string[] temp = eee1[2].Split(':');
            string temp1 = temp[1].Replace('"',' ');
            Forecast_obj[i].transform.GetChild(1).GetComponent<Text>().text = temp1;

        }
        
    }
    private int local_indx=0;
    public void Show_Weather(int x)
    {
    
        local_indx = x+1;
        if (x<=2)
        {
          //  Debug.Log(local_indx);
            //string[] eee1 = Forecast_data[x].GetField("weather").ToString().Split(',');
            //string[] temp = eee1[2].Split(':');
            //string temp1 = temp[1].Replace('"', ' ');
            Currentinfo[0].text = Forecast_data[x].GetField("main").GetField("temp").ToString().Remove(2) + "°" + " " + "C";
            Currentinfo[1].text = DateTime.Now.AddDays(local_indx).Day + " " + DateTime.Now.AddDays(local_indx).ToString("MMMM");
            Currentinfo[2].text = DateTime.Now.AddDays(local_indx).DayOfWeek.ToString().Remove(3);

        }
        else
        {
            Currentinfo[0].text = Current_temp + "°" + " " + "C";
            Currentinfo[1].text = DateTime.Now.Day + " " + DateTime.Now.ToString("MMMM");
            Currentinfo[2].text = DateTime.Now.DayOfWeek.ToString().Remove(3);
        }
    
    }
  


}
