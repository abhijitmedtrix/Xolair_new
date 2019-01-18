using System;
using System.Collections;
using System.Collections.Generic;
using App.Data.Reminders;
using BayatGames.SaveGamePro;
using UnityEngine;
using VoxelBusters.NativePlugins;

public class ReminderManager : MonoSingleton<ReminderManager>
{
    private List<ReminderData> _actualReminderGroups = new List<ReminderData>();

    // private List<ReminderData> _actualNotifications = new List<ReminderData>();
    // private List<NotificationData> _historyReminders = new List<NotificationData>();

    private const string _ACTUAL_REMINDERS_FILE_PATH = "notifications.dat";
    // private const string _HISTORY_REMINDERS_FILE_PATH = "notificationsHistory.dat";

    #region Unity Methods

    private void Awake()
    {
        // Subscribe to save and load events.
        SaveGame.OnSaved += SaveGame_OnSaved;
        SaveGame.OnLoaded += SaveGame_OnLoaded;
    }

    protected void Start()
    {
        RegisterNotificationTypes(NotificationType.Sound | NotificationType.Badge | NotificationType.Alert);

        // parse all notifications to data for best control
        if (SaveGame.Exists(_ACTUAL_REMINDERS_FILE_PATH))
        {
            _actualReminderGroups = SaveGame.Load<List<ReminderData>>(_ACTUAL_REMINDERS_FILE_PATH);
        }

        /*
        // parse all notifications to data for best control
        if (SaveGame.Exists(_HISTORY_REMINDERS_FILE_PATH))
        {
            _historyReminders = SaveGame.Load<List<NotificationData>>(_HISTORY_REMINDERS_FILE_PATH);
        }

        // set yesterday by default because there is no way how actual reminder can 
        DateTime lastHistoryData = DateTime.Today.AddDays(-1);

        // find last history data
        if (_historyReminders.Count > 0)
        {
            lastHistoryData = _historyReminders[0].fireDate;
        }

        // if value is not > 0, that means nothing to check
        int daysToCheck = DateTime.Today.Subtract(lastHistoryData).Days;

        // checking actual notification try to find some which can be replaced to
        List<NotificationData> oldReminders = new List<NotificationData>();
        for (int i = 0; i < daysToCheck; i++)
        {
            DateTime dt = lastHistoryData.AddDays(i + 1);
            oldReminders.AddRange(GetRemindersByDate(dt));
        }
        
        // now convert actual reminders to old (history) reminders
        
        // and remove reminder data from the group
        
        // save changes if was done
        if (oldReminders.Count > 0)
        {
            
        }
        */
    }

    private List<NotificationData> GetRemindersByDate(DateTime dateTime)
    {
        List<NotificationData> list = new List<NotificationData>();
        
        for (int i = 0; i < _actualReminderGroups.Count; i++)
        {
            list.AddRange(_actualReminderGroups[i].GetRemindersByDate(dateTime));
        }

        return list;
    }

    protected void OnEnable()
    {
        // Register for callbacks
        NotificationService.DidLaunchWithLocalNotificationEvent += DidLaunchWithLocalNotificationEvent;
        NotificationService.DidReceiveLocalNotificationEvent += DidReceiveLocalNotificationEvent;
    }

    protected void OnDisable()
    {
        // Un-Register from callbacks
        NotificationService.DidLaunchWithLocalNotificationEvent -= DidLaunchWithLocalNotificationEvent;
        NotificationService.DidReceiveLocalNotificationEvent -= DidReceiveLocalNotificationEvent;
    }

    #endregion

    #region API Methods

    private void RegisterNotificationTypes(NotificationType notificationTypes)
    {
        NPBinding.NotificationService.RegisterNotificationTypes(notificationTypes);
    }

    private NotificationType EnabledNotificationTypes()
    {
        return NPBinding.NotificationService.EnabledNotificationTypes();
    }

    private string ScheduleLocalNotification(CrossPlatformNotification notification)
    {
        return NPBinding.NotificationService.ScheduleLocalNotification(notification);
    }

    private void CancelLocalNotification(string notificationID)
    {
        NPBinding.NotificationService.CancelLocalNotification(notificationID);
    }

    private void CancelAllLocalNotifications()
    {
        NPBinding.NotificationService.CancelAllLocalNotification();
    }

    private void ClearNotifications()
    {
        NPBinding.NotificationService.ClearNotifications();
    }

    #endregion


    #region API Callback Methods

    private void DidLaunchWithLocalNotificationEvent(CrossPlatformNotification notification)
    {
        Debug.Log("Application did launch with local notification.");
    }

    private void DidReceiveLocalNotificationEvent(CrossPlatformNotification notification)
    {
        Debug.Log("Application received local notification.");
    }

    #endregion


    #region SaveGame callbacks

    /// <summary>
    /// Save Event.
    /// </summary>
    /// <param name="identifier">Identifier.</param>
    /// <param name="value">Value.</param>
    /// <param name="settings">Settings.</param>
    void SaveGame_OnSaved(string identifier, object value, SaveGameSettings settings)
    {
        Debug.LogFormat("{0} Saved Successfully", identifier);
    }

    /// <summary>
    /// Load Event.
    /// </summary>
    /// <param name="identifier">Identifier.</param>
    /// <param name="result">Result.</param>
    /// <param name="type">Type.</param>
    /// <param name="defaultValue">Default value.</param>
    /// <param name="settings">Settings.</param>
    void SaveGame_OnLoaded(string identifier, object result, Type type, object defaultValue, SaveGameSettings settings)
    {
        Debug.LogFormat("{0} Loaded Successfully", identifier);
    }

    #endregion


    public List<NotificationData> GetAllNotifications(DateTime date)
    {
        // try to find all notifications for particular date


        return null;
    }

    public CrossPlatformNotification CreateNotification(long fireAfterSec, eNotificationRepeatInterval repeatInterval)
    {
        // User info
        IDictionary userInfo = new Dictionary<string, string>();
        userInfo["data"] = "custom data";

        CrossPlatformNotification.iOSSpecificProperties _iosProperties =
            new CrossPlatformNotification.iOSSpecificProperties();
        _iosProperties.HasAction = true;
        _iosProperties.AlertAction = "alert action";

        CrossPlatformNotification.AndroidSpecificProperties _androidProperties =
            new CrossPlatformNotification.AndroidSpecificProperties();
        _androidProperties.ContentTitle = "content title";
        _androidProperties.TickerText = "ticker ticks over here";
        _androidProperties.LargeIcon =
            "NativePlugins.png"; //Keep the files in Assets/PluginResources/Android or Common folder.

        CrossPlatformNotification notification = new CrossPlatformNotification();
        notification.AlertBody = "alert body"; //On Android, this is considered as ContentText
        notification.FireDate = System.DateTime.Now.AddSeconds(fireAfterSec);
        notification.RepeatInterval = repeatInterval;
        notification.SoundName =
            "Notification.mp3"; //Keep the files in Assets/PluginResources/Android or iOS or Common folder.
        notification.UserInfo = userInfo;
        notification.iOSProperties = _iosProperties;
        notification.AndroidProperties = _androidProperties;

        return notification;
    }
}