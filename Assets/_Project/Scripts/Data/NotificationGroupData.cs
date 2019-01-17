using System;
using System.Collections.Generic;
using UnityEngine;
using VoxelBusters.NativePlugins;

namespace App.Data.Reminders
{
    /// <summary>
    /// Because of our complex structure NotificationData repeatInterval value is not actual, that's why we will need to setup all notifications independently without repeating. Android got limit of 50, and iOS limit of 60.
    /// Each app start need to check, which new notification should be added to device OS.
    /// </summary>
    [Serializable]
    public class NotificationGroupData
    {
        public string title;
        
        public DateTime endDate = DateTime.MaxValue;
        public int circleLifeTime = -1;

        /// <summary>
        /// Can be not actual interval but the type of notification, for example "each 3rd day/week/month".
        /// </summary>
        public eNotificationRepeatInterval repeatOption;
        
        public List<NotificationData> notificationDatas;
    }
}