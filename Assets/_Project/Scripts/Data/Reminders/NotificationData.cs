using System;
using System.Collections.Generic;
using QuickEngine.Extensions;
using UnityEngine;
using VoxelBusters.NativePlugins;

namespace App.Data.Reminders
{
    [Serializable]
    public struct NotificationInfo
    {
        public string id;
        public DateTime fireDate;
        public eNotificationRepeatInterval repeatInterval;
    }

    [Serializable]
    public class NotificationData
    {
        public bool isFortnight;

        /// <summary>
        /// The list of notifications id linked to current data
        /// </summary>
        public List<string> idArray = new List<string>();

        public Dictionary<string, NotificationInfo> notificationsDict = new Dictionary<string, NotificationInfo>();

        /// <summary>
        /// Allow to calculate each date when the notification should execute 
        /// </summary>
        public eNotificationRepeatInterval repeatInterval;

        /// <summary>
        /// keep cache parent group id in case if we will need to edit reminder in a past dates, or if group data deleted and need to clean up all last history data
        /// </summary>
        public string parentGroupId;

        public string title;
        public string message;

        /// <summary>
        /// the date and time when notification should execute 1st time
        /// </summary>
        public DateTime fireDate;

        public event Action OnDataUpdate;


        /// <summary>
        /// This methods used only within UnityEditor for test, when we need to track passed notifications, but can't receive DidReceiveNotification callback from ReminderManager
        /// </summary>
        /// <returns></returns>
        public List<NotificationInfo> GetPassedNotifications()
        {
            List<NotificationInfo> nInfos = null;
            for (int i = 0; i < idArray.Count; i++)
            {
                NotificationInfo info = notificationsDict[idArray[i]];
                if (info.fireDate.IsOlderDate(DateTime.Now))
                {
                    if (nInfos == null)
                    {
                        nInfos = new List<NotificationInfo>();
                    }

                    nInfos.Add(info);
                }
            }

            return nInfos;
        }

        /// <summary>
        /// Check notification each time on AppStart, must be called in ReminderData.
        /// </summary>
        public bool IsValid()
        {
            if (repeatInterval == eNotificationRepeatInterval.NONE && fireDate.IsOlderDate(DateTime.Now))
            {
                return false;
            }

            // check, should it be rescheduled or not
            if (isFortnight)
            {
                for (int i = 0; i < idArray.Count; i++)
                {
                    NotificationInfo info = notificationsDict[idArray[i]];

                    // check is there some outdated notifications (in case if wasn't removed by ReminderManager
                    if (info.fireDate.IsOlderDate(DateTime.Now))
                    {
                        // reschedule
                        UnregisterNotification(info.id);

                        SetNotification();
                    }
                }
            }

            return true;
        }

        public void SetNotification(DateTime fireDate, eNotificationRepeatInterval repeatInterval, string parentGroupId,
            bool isFortnight, string title, string message = null)
        {
            this.isFortnight = isFortnight;
            this.fireDate = fireDate;
            this.repeatInterval = repeatInterval;
            this.parentGroupId = parentGroupId;
            this.title = title;
            this.message = message;

            SetNotification();
        }

        /// <summary>
        /// Set notification using all local data.
        /// </summary>
        private void SetNotification()
        {
            TryFixInterval();

            // when we reschedule or change existing notification, fireDate can be in a past already, so each time we need to find next date when notification should be called
            DateTime nextFireDate = fireDate;

            // get start date diff in secs
            long startInSecs = (long)this.fireDate.Subtract(DateTime.Now).TotalSeconds;

            Debug.Log($"Set notification: title: {title}, parent id: {parentGroupId}. fireDate: {fireDate}, nextFireDate: {nextFireDate}, startInSecs: {startInSecs}");
            
            // if fireDate is in a past (past will return always 0, not -value)
            if (startInSecs <= 0)
            {
                nextFireDate = FindNextFireDate();
                startInSecs = (long)nextFireDate.Subtract(DateTime.Now).TotalSeconds;
            }

            // schedule 2 notifications upfront for each week, so 4 totally
            if (this.isFortnight)
            {
                long twoWeeksToSeconds = 2 * 7 * 24 * 60 * 60;
                int notificationCount = notificationsDict.Count;

                for (int i = notificationCount; i < 4; i++)
                {
                    Debug.Log($"Start in secs: {startInSecs}, twoWeeksToSeconds {twoWeeksToSeconds}, partial: {twoWeeksToSeconds * i}, total: {startInSecs + twoWeeksToSeconds * i}, and date format: {new TimeSpan(0, 0, (int)(startInSecs + twoWeeksToSeconds * i))}");
                    RegisterNotification(startInSecs + twoWeeksToSeconds * i, repeatInterval);
                }
            }
            else
            {
                RegisterNotification(startInSecs, this.repeatInterval);
            }

            OnDataUpdate?.Invoke();
        }

        public bool IsAssignedToDate(DateTime dateTime)
        {
            if (dateTime.IsSameDay(fireDate))
            {
                return true;
            }

            if (dateTime.IsLaterDate(fireDate))
            {
                // for fortnight notifications we should check 1st and by other logic
                if (isFortnight)
                {
                    float daysDifference = (float)dateTime.Midnight().Subtract(fireDate.Midnight()).TotalDays;
                                        
                    // we need just not even numbers
                    if ((int)daysDifference / 7 % 2 == 0 && dateTime.DayOfWeek == fireDate.DayOfWeek)
                    {
                        return true;
                    }
                }
                
                // check, does a day fit to requested day of week
                switch (repeatInterval)
                {
                    case eNotificationRepeatInterval.NONE:

                        break;

                    case eNotificationRepeatInterval.DAY:
                        return true;
                        break;

                    case eNotificationRepeatInterval.WEEK:
                        if (dateTime.DayOfWeek == fireDate.DayOfWeek)
                        {
                            return true;
                        }

                        break;

                    case eNotificationRepeatInterval.MONTH:
                        if (dateTime.Day == fireDate.Day)
                        {
                            return true;
                        }

                        break;

                    case eNotificationRepeatInterval.YEAR:
                        return dateTime.Day == fireDate.Day && dateTime.Month == fireDate.Month;
                        break;
                }
            }

            return false;
        }

        private void RegisterNotification(long startInSecs, eNotificationRepeatInterval repeatInterval)
        {
            // CrossPlatformNotification class it too huge to save using GameSave asset, but we need just some short data
            CrossPlatformNotification notification =
                ReminderManager.Instance.CreateNotification(title, startInSecs, repeatInterval);

            // cache notification
            string id = ReminderManager.Instance.ScheduleLocalNotification(notification);

            if (!notificationsDict.ContainsKey(id) && !idArray.Contains(id))
            {
                notificationsDict.Add(id,
                    new NotificationInfo {id = id, fireDate = fireDate, repeatInterval = repeatInterval});
                idArray.Add(id);
                Debug.Log(
                    $"Registering notification with title: {title}, parent group id: {parentGroupId}, reminder start fire date: {fireDate} and actual fire date: {DateTime.Now.AddSeconds(startInSecs)}, interval: {repeatInterval}, and id: {id}");
            }
            else
            {
                throw new Exception($"Notification with id <{id}> already registered and added to dict");
            }
        }

        public void UnregisterNotification(string id)
        {
            // Debug.Log($"Trying to UnregisterNotification with id: {id}, while array length is: {idArray.Count}, and dict length: {notificationsDict.Count}");
            if (notificationsDict.ContainsKey(id) && idArray.Contains(id))
            {
                NotificationInfo info = notificationsDict[id];
                
                notificationsDict.Remove(id);
                idArray.Remove(id);
                
                Debug.Log(
                    $"Unregistering notification with fire date: {info.fireDate}, interval: {info.repeatInterval}, and id: {id}");
            }
            else
            {
                Debug.LogWarning($"No such notification with id <{id}> in dict");
            }


            // and remove from notification service
            ReminderManager.Instance.CancelLocalNotification(id);
        }

        public void ChangeRepeatInterval(eNotificationRepeatInterval repeatInterval, bool isFortnight = false)
        {
            if (this.repeatInterval == repeatInterval)
            {
                Debug.LogWarning("Trying to change notification with a same repeat interval! Return.");
                return;
            }

            this.repeatInterval = repeatInterval;
            TryFixInterval();

            RemoveAllNotifications();
            SetNotification();

            OnDataUpdate?.Invoke();
        }

        public void ChangeFireDate(DateTime dateTime)
        {
            if (fireDate.Equals(dateTime))
            {
                Debug.LogWarning("Trying to change notification with a same date and time! Return.");
                return;
            }

            fireDate = dateTime.Date + fireDate.TimeOfDay;

            RemoveAllNotifications();
            SetNotification();
        }

        public void ChangeTime(DateTime dateTime)
        {
            if (fireDate.TimeOfDay == dateTime.TimeOfDay)
            {
                Debug.LogWarning("Trying to change notification time with a same time! Return.");
                return;
            }

            // proper way to change struct value
            fireDate = fireDate.Date + dateTime.TimeOfDay;

            RemoveAllNotifications();
            SetNotification();
        }

        public void ChangeTitle(string title)
        {
            if (this.title.Equals(title))
            {
                Debug.LogWarning("Trying to change notification with a same title! Return.");
                return;
            }

            this.title = title;

            RemoveAllNotifications();
            SetNotification();
        }

        public void ChangeMessage(string message)
        {
            if (this.message.Equals(message))
            {
                Debug.LogWarning("Trying to change notification with a same message! Return.");
                return;
            }

            this.message = message;

            RemoveAllNotifications();
            SetNotification();
        }

        public void Reset()
        {
            RemoveAllNotifications();
        }

        private void RemoveAllNotifications()
        {
            var tempArr = idArray.ToArray();
            for (int i = 0; i < tempArr.Length; i++)
            {
                UnregisterNotification(tempArr[i]);
            }

            tempArr = null;
            idArray.Clear();
            notificationsDict.Clear();
        }

        private DateTime FindNextFireDate()
        {
            DateTime nextFireDate = DateTime.MaxValue;
            ReminderManager.TimeDifference timeDifference =
                ReminderManager.GetDateTimeDifference(DateTime.Now, fireDate);

            // check, does a day fit to requested day of week
            switch (repeatInterval)
            {
                case eNotificationRepeatInterval.NONE:
                    if (isFortnight)
                    {
                        // 2 weeks = 14 days
                        int doubleWeeksAdd = Mathf.CeilToInt(timeDifference.days / 14f);
                        nextFireDate = fireDate.AddDays(doubleWeeksAdd * 14f);
                    }
                    else
                    {
                        throw new Exception(
                            "Shouldn't be the case when searching for next day for non-repeatable notification");
                    }

                    break;

                case eNotificationRepeatInterval.DAY:
                    nextFireDate = DateTime.Today.Tomorrow() + fireDate.TimeOfDay;
                    break;

                case eNotificationRepeatInterval.WEEK:
                    int weeksToAdd = Mathf.CeilToInt(timeDifference.days / 7f);
                    nextFireDate = fireDate.AddDays(weeksToAdd * 7);
                    break;

                case eNotificationRepeatInterval.MONTH:
                    nextFireDate = fireDate.AddMonths(1 + timeDifference.months);
                    break;

                case eNotificationRepeatInterval.YEAR:
                    nextFireDate = fireDate.AddYears(1 + timeDifference.years);
                    break;
            }

            return nextFireDate;
        }

        private void TryFixInterval()
        {
            if (isFortnight)
            {
                if (this.repeatInterval != eNotificationRepeatInterval.NONE)
                {
                    Debug.LogWarning(
                        $"Repeat interval for FORTNIGHT repetition must be NONE, but it's <{this.repeatInterval}>");
                    this.repeatInterval = eNotificationRepeatInterval.NONE;
                }
            }
        }

        public bool HasNotificationWithId(string id)
        {
            return idArray.Contains(id);
        }
    }
}