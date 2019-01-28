using System;
using System.Globalization;
using UnityEngine;

namespace App.Data
{
    public class BaseTrackerData
    {
        protected DateTime _dateTime = DateTime.MinValue;
        protected string _initialJson;
        protected JSONObject _jsonObject;
        
        /// <summary>
        /// Creating new data instance (adding new entry) we must add date.
        /// </summary>
        /// <param name="date"></param>
        public BaseTrackerData(DateTime date)
        {
            _dateTime = date.Date;
        }
        
        protected BaseTrackerData(string json)
        {
            _initialJson = json;
            _jsonObject = new JSONObject(_initialJson);
            _dateTime = DateTime.ParseExact(_jsonObject.GetField("date").str, "dd/MM/yyyy", CultureInfo.InvariantCulture).Date;
        }
        public BaseTrackerData()
        {

        }
        /// <summary>
        /// Returns fixed date (Only Date without time stamp)
        /// </summary>
        /// <returns></returns>
        public DateTime GetDate()
        {
            return _dateTime;
        }
        
        public virtual JSONObject FormatToJson()
        {
            JSONObject jsonObject = new JSONObject();
            jsonObject.AddField("date", _dateTime.ToString("dd/MM/yyyy"));
            return jsonObject;
        }
    }
}