using System;
using System.Collections.Generic;
using QuickEngine.Extensions;
using UnityEngine;
using VoxelBusters.NativePlugins;

namespace App.Data.Reminders
{
    [Serializable]
    public enum NotificationStatus
    {
        Undefined,
        Completed
    }
    
    [Serializable]
    public class NotificationData
    {
        /// <summary>
        /// The list of notifications id linked to current data
        /// </summary>
        public List<string> idArray;

        /// <summary>
        /// this field can differ to <fireDate> because user can setup reminder in Monday for Wednesday
        /// </summary>
        public DateTime registrationDate;

        /// <summary>
        /// Allow to calculate each date when the notification should execute 
        /// </summary>
        public eNotificationRepeatInterval repeatInterval;

        // by default never end
        public DateTime endDate = DateTime.MaxValue;
        
        /// <summary>
        /// keep cache parent group id in case if we will need to edit reminder in a past dates, or if group data deleted and need to clean up all last history data
        /// </summary>
        public string parentGroupId;

        /// <summary>
        /// Title can be changed when reminder parent group data modified, but we need to cache it anyway just in case if parent group will be removed
        /// </summary>
        public string title;
        
        public NotificationStatus status;
        
        /// <summary>
        /// the date and time when notification should execute 1st time
        /// </summary>
        public DateTime fireDate;
        
        public bool IsAssignedToDate(DateTime dateTime)
        {
            if (dateTime.IsSameDay(fireDate))
            {
                return true;
            }

            if (dateTime.IsLaterDate(fireDate))
            {
                // check, does a day fit to requested day of week
            }

            return false;
        }

        public void AddDayOfWeek(DayOfWeek dayOfWeek, eNotificationRepeatInterval interval, int repeatAfter)
        {
            
        }

        public void ChangeRepeatInterval(eNotificationRepeatInterval interval)
        {
            // proper way to change struct value
            // fireDate = fireDate.Date + dateTime.TimeOfDay;
        }

        public void ChangeTime(DateTime dateTime)
        {
            // proper way to change struct value
            fireDate = fireDate.Date + dateTime.TimeOfDay;
        }
    }

    public static class NotificationDataConstants
    {
        public const int MAX_WEEKS_REPEAT = 52;
        public const int MAX_MONTHS_REPEAT = 11;
    }
}