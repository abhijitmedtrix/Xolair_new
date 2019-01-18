using System;
using System.Collections.Generic;
using UnityEngine;
using VoxelBusters.NativePlugins;

namespace App.Data.Reminders
{
    [Serializable]
    public enum RepeatInterval
    {
        NONE,
        DAY,
        WEEK,
        FORTHNIGHT,
        MONTH
    }
    
    /// <summary>
    /// Because of our complex structure NotificationData repeatInterval value is not actual, that's why we will need to setup all notifications independently without repeating. Android got limit of 50, and iOS limit of 60.
    /// Each app start need to check, which new notification should be added to device OS.
    /// </summary>
    [Serializable]
    public class ReminderData
    {
        public string id;
        public string title;

        public bool isActive;
        
        public DateTime endDate = DateTime.MaxValue;

        /// <summary>
        /// Can be not actual interval but the type of notification, for example "each 3rd day/week/month".
        /// </summary>
        public eNotificationRepeatInterval repeatOption;
        
        public List<NotificationData> remiderDatas;

        public event Action<ReminderData> OnDataUpdate;
        
        public List<NotificationData> GetRemindersByDate(DateTime dateTime)
        {
            List<NotificationData> list = new List<NotificationData>();

            for (int i = 0; i < remiderDatas.Count; i++)
            {
                
            }
            
            return list;
        }

        public void SetActive(bool active)
        {
            isActive = true;
        }
    }
}