using System;
using System.Collections.Generic;
using QuickEngine.Extensions;
using UnityEngine;
using VoxelBusters.NativePlugins;

namespace App.Data.Reminders
{
    [Serializable]
    public class NotificationData
    {
        [Serializable]
        public enum Status
        {
            Undefined,
            Scheduled,
            Completed
        }

        /// <summary>
        /// The list of notifications id linked to current data
        /// </summary>
        public List<string> idArray;

        /// <summary>
        /// this field can differ to <fireDate> because user can setup reminder in Monday for Wednesday
        /// </summary>
        public DateTime registrationDate;

        public Status status;

        /// <summary>
        /// Allow to calculate each date when the notification should execute 
        /// </summary>
        public eNotificationRepeatInterval repeatInterval;

        /// <summary>
        /// the date&time when notification should execute 1st time
        /// </summary>
        public DateTime fireDate;

        // by default never end
        public DateTime endDate = DateTime.MaxValue;

        public List<DayOfWeek> daysOfWeek;

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