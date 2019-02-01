using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using QuickEngine.Extensions;
using VoxelBusters.NativePlugins;

namespace App.Data.Reminders
{
    [Serializable]
    public enum RepeatInterval
    {
        ONCE,
        DAY,
        WEEK,
        FORTNIGHT,
        MONTH,
        YEAR
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
        public string message;

        /// <summary>
        /// Should be active by default
        /// </summary>
        public bool isActive = true;

        /// <summary>
        /// We still need to keep this param, because we do not remove any reminder from the cache to show it in a calendar past days.
        /// </summary>
        public bool isDeleted;

        public bool isDefault;
        public bool isDone;

        /// <summary>
        /// this field can differ to <fireDate> because user can setup reminder in Monday for Wednesday
        /// </summary>
        public readonly DateTime registrationDate;
        public AppMode appMode;

        public DateTime fireDate;
        public DateTime endDate = DateTime.MaxValue;
        public RepeatInterval repeatInterval;
        public List<NotificationData> notifications = new List<NotificationData>();
        public List<DateTime> historyDates = new List<DateTime>();

        /// <summary>
        /// Cache selected days of week to repeat (if WEEK or FORTNIGHT selected) 
        /// </summary>
        public DayOfWeek[] daysOfWeek;

        public event Action<ReminderData> OnDataUpdate;

        public ReminderData(ReminderData targetData)
        {
            this.registrationDate = targetData.registrationDate;
            this.appMode = targetData.appMode;
            CopyData(targetData);
        }

        public ReminderData(AppMode appMode, string id, DateTime fireDate, DateTime endDate, RepeatInterval repeatInterval, string title,
            string message = null,
            DayOfWeek[] daysOfWeek = null)
        {
            // unchangeable value
            registrationDate = DateTime.Now;
            this.appMode = appMode;

            this.id = id;
            this.fireDate = fireDate;
            this.repeatInterval = repeatInterval;
            this.title = title;
            this.message = message;
            this.daysOfWeek = daysOfWeek;
        }

        /// <summary>
        /// Important to call to save Reminder changes and write data.
        /// </summary>
        public void SetupReminder()
        {
            RemoveAllNotifications();
            
            NotificationData data;

            eNotificationRepeatInterval notificationRepeatInterval = eNotificationRepeatInterval.NONE;
            // covert to VoxelBuster plugin data format
            switch (this.repeatInterval)
            {
                case RepeatInterval.ONCE:
                    notificationRepeatInterval = eNotificationRepeatInterval.NONE;
                    break;
                case RepeatInterval.DAY:
                    notificationRepeatInterval = eNotificationRepeatInterval.DAY;
                    break;
                case RepeatInterval.WEEK:
                    notificationRepeatInterval = eNotificationRepeatInterval.WEEK;
                    break;
                case RepeatInterval.FORTNIGHT:
                    notificationRepeatInterval = eNotificationRepeatInterval.NONE;
                    break;
                case RepeatInterval.MONTH:
                    notificationRepeatInterval = eNotificationRepeatInterval.MONTH;
                    break;
            }

            // when user set multiple days of week, need to add notification for each of the day 
            if (daysOfWeek != null && daysOfWeek.Length > 1)
            {
                int fireDateIndex = (int) fireDate.DayOfWeek;

                for (int i = 0; i < daysOfWeek.Length; i++)
                {
                    data = new NotificationData();
                    int index = (int) daysOfWeek[i];

                    DateTime notificationFireDate = fireDate.AddDays(index - fireDateIndex);

                    data.SetNotification(notificationFireDate, notificationRepeatInterval, id,
                        this.repeatInterval == RepeatInterval.FORTNIGHT, title, message);

                    // cache it
                    notifications.Add(data);
                }
            }
            //  set new notification - in most cases use simple logic
            else
            {
                data = new NotificationData();
                data.SetNotification(fireDate, notificationRepeatInterval, id,
                    this.repeatInterval == RepeatInterval.FORTNIGHT, title, message);

                // cache it
                notifications.Add(data);
            }

            OnDataUpdate?.Invoke(this);
        }

        public void CheckReminderData()
        {
            Debug.Log($"Reminder with title {title} has been loaded. Notifications count: {notifications.Count}, mode: {appMode}");

            // check, is there any updates for completedDates
            // set yesterday by default 
            DateTime lastCompletedDate = DateTime.Today.AddDays(-1);

            // if value is not > 0, that means nothing to check
            int daysToCheck = DateTime.Today.Subtract(lastCompletedDate).Days;

            for (int i = 0; i < daysToCheck; i++)
            {
                DateTime dateTime = lastCompletedDate.AddDays(i + 1);

                // if there are some notifications which could be triggered these period
                if (HasNotificationByDate(dateTime))
                {
                    // inform user
                    // TODO - not needed for now
                }
            }

            // probably not needed because that can be triggered from ReminderManager in OnLocalNotificationReceived callback and now we need it only for tests in editor
            if (Application.isEditor)
            {
                // also check for outdated notification and reschedule some if needed
                for (int i = 0; i < notifications.Count; i++)
                {
                    // try to find outdated notifications to add to history
                    List<NotificationInfo> infos = notifications[i].GetPassedNotifications();
                    if (infos != null)
                    {
                        for (int j = 0; j < infos.Count; j++)
                        {
                            AddToHistory(infos[j].fireDate);
                        }
                    }
                }

                ValidateNotifications();

                OnDataUpdate?.Invoke(this);
            }
        }


        #region Individual data change

        public void ChangeDaysOfWeek(DayOfWeek[] daysOfWeek)
        {
            if (repeatInterval != RepeatInterval.WEEK || repeatInterval != RepeatInterval.FORTNIGHT)
            {
                Debug.LogWarning(
                    $"Can't set days of week while interval doesn't set to WEEK or FORTNIGHT. Now it's <{repeatInterval}>");
            }

            this.daysOfWeek = daysOfWeek;

            // remove old notifications
            RemoveAllNotifications();

            // set new, updated one
            SetupReminder();
        }

        public void ChangeRepeatInterval(RepeatInterval repeatInterval, DayOfWeek[] daysOfWeek = null)
        {
            this.repeatInterval = repeatInterval;
            this.daysOfWeek = daysOfWeek;

            if (repeatInterval != RepeatInterval.WEEK || repeatInterval != RepeatInterval.FORTNIGHT)
            {
                Debug.LogWarning(
                    $"Can't set days of week while interval doesn't set to WEEK or FORTNIGHT. Now it's <{repeatInterval}>");
            }

            // remove old notifications
            RemoveAllNotifications();

            // set new, updated one
            SetupReminder();
        }

        public void ChangeFireDate(DateTime dateTime)
        {
            fireDate = dateTime;

            // remove old notifications
            RemoveAllNotifications();

            // set new, updated one
            SetupReminder();
        }

        public void ChangeFireTime(DateTime dateTime)
        {
            // proper way to change struct value
            fireDate = fireDate.Date + dateTime.TimeOfDay;

            // remove old notifications
            RemoveAllNotifications();

            // set new, updated one
            SetupReminder();
        }

        public void ChangeEndDate(DateTime dateTime)
        {
            endDate = dateTime;

            // remove old notifications
            RemoveAllNotifications();

            // set new, updated one
            SetupReminder();
        }

        public void ChangeTitle(string title)
        {
            this.title = title;

            // remove old notifications
            RemoveAllNotifications();

            // set new, updated one
            SetupReminder();
        }

        public void ChangeMessage(string message)
        {
            this.message = message;

            // remove old notifications
            RemoveAllNotifications();

            // set new, updated one
            SetupReminder();
        }

        /// <summary>
        /// Toggle reminder active state.
        /// </summary>
        /// <param name="active"></param>
        public void SetActive(bool active)
        {
            // return if value already the same
            if (active == isActive) return;

            isActive = active;

            // if reminder been restarted
            if (isActive)
            {
                SetupReminder();
            }
            // if user turned off the reminder
            else
            {
                RemoveAllNotifications();
            }
        }

        public void SetDone(bool done)
        {
            isDone = done;
        }

        #endregion


        public void AddToHistory(DateTime dateTime)
        {
            // add entry to th history
            historyDates.Add(dateTime);

            ValidateNotifications();

            OnDataUpdate?.Invoke(this);
        }

        /// <summary>
        /// Loop over all notifications and make sure each of them is actual/valid, otherwise remove it. 
        /// </summary>
        private void ValidateNotifications()
        {
            // because of notification list will be modified runtime, create it's copy temporary
            NotificationData[] tempArr = notifications.ToArray();
            
            for (int i = 0; i < tempArr.Length; i++)
            {
                // if any notification is passed and can't be rescheduled
                if (!tempArr[i].IsValid())
                {
                    notifications.Remove(tempArr[i]);
                }
            }

            // if there is no active notification anymore
            if (notifications.Count <= 0)
            {
                // deactivate
                SetActive(false);
            }
        }

        public void Reset()
        {
            RemoveAllNotifications();
        }

        public bool HasNotificationByDate(DateTime dateTime, bool includePast = false)
        {
            if (includePast)
            {
                for (int i = 0; i < historyDates.Count; i++)
                {
                    Debug.Log($"Reminder {title} history date: {historyDates[i]}");
                    if (historyDates[i].IsSameDay(dateTime))
                    {
                        return true;
                    }
                }
            }

            return GetNotificationByDate(dateTime) != null;
        }

        public NotificationData GetNotificationByDate(DateTime dateTime)
        {
            return notifications.FirstOrDefault(x => x.IsAssignedToDate(dateTime));
        }

        public bool HasNotificationWithId(string notificationId)
        {
            return notifications.Find(x => x.HasNotificationWithId(notificationId)) != null;
        }

        public void Delete()
        {
            SetActive(false);
            isDeleted = true;
        }

        private void RemoveAllNotifications()
        {
            for (int i = 0; i < notifications.Count; i++)
            {
                notifications[i].Reset();
            }

            notifications.Clear();
        }

        public void CopyData(ReminderData targetData)
        {
            this.isActive = targetData.isActive;
            this.isDeleted = targetData.isDeleted;
            this.isDefault = targetData.isDefault;
            this.isDone = targetData.isDone;

            this.id = targetData.id;
            this.title = targetData.title;
            this.message = targetData.message;

            this.fireDate = targetData.fireDate;
            this.endDate = targetData.endDate;
            this.repeatInterval = targetData.repeatInterval;
            this.daysOfWeek = targetData.daysOfWeek;
        }

        public override bool Equals(object obj)
        {
            ReminderData compareTo = obj as ReminderData;
            return this.registrationDate == compareTo.registrationDate
                   &&
                   this.isActive == compareTo.isActive
                   &&
                   this.isDeleted == compareTo.isDeleted
                   &&
                   this.isDefault == compareTo.isDefault
                   &&
                   this.isDone == compareTo.isDone
                   &&
                   this.id == compareTo.id
                   &&
                   this.title == compareTo.title
                   &&
                   this.message == compareTo.message
                   &&
                   this.fireDate == compareTo.fireDate
                   &&
                   this.endDate == compareTo.endDate
                   &&
                   this.repeatInterval == compareTo.repeatInterval
                   &&
                   (this.daysOfWeek == compareTo.daysOfWeek
                    ||
                    this.daysOfWeek.Length == compareTo.daysOfWeek.Length &&
                    this.daysOfWeek.SequenceEqual(compareTo.daysOfWeek));
        }
    }
}