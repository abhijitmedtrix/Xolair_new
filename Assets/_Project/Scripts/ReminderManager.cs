using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using App.Data.Reminders;
using BayatGames.SaveGamePro;
using DoozyUI;
using Opencoding.CommandHandlerSystem;
using QuickEngine.Extensions;
using UnityEngine;
using UnityEngine.Events;
using VoxelBusters.NativePlugins;

public class ReminderManager : MonoSingleton<ReminderManager>
{
    [SerializeField] protected Sprite _clockIcon;
    
    public struct TimeDifference
    {
        public int years;
        public int months;
        public int days;
        public int hours;
        public int minutes;
        public int seconds;
    }

    private List<ReminderData> _reminders = new List<ReminderData>();
    private const string _ACTUAL_REMINDERS_FILE_PATH = "notifications.dat";
    private const string _DEFAULT_REMINDERS_ADDED_KEY = "defaultRemindersAdded";
    private const string _DEFAULT_SAA_ST_KEY = "SAA_ST";
    private const string _DEFAULT_SAA_ACT_KEY = "SAA_ACT";

    public static event Action<List<ReminderData>> OnRemindersUpdate;

    protected void Awake()
    {
        CommandHandlers.RegisterCommandHandlers(this);

        // Subscribe to save and load events.
        SaveGame.OnSaved += SaveGame_OnSaved;
        SaveGame.OnLoaded += SaveGame_OnLoaded;
    }

    protected void Start()
    {
        Debug.Log("Start in ReminderManager");
        RegisterNotificationTypes(NotificationType.Sound | NotificationType.Badge | NotificationType.Alert);
        
        // parse all notifications to data for best control
        if (SaveGame.Exists(_ACTUAL_REMINDERS_FILE_PATH))
        {
            _reminders = SaveGame.Load<List<ReminderData>>(_ACTUAL_REMINDERS_FILE_PATH);
        }

        if (!PlayerPrefs.HasKey(_DEFAULT_REMINDERS_ADDED_KEY))
        {
            // add default Symptom reminder
            ReminderData reminderData = new ReminderData(_DEFAULT_SAA_ST_KEY, DateTime.Today.Tomorrow(),
                DateTime.MaxValue,
                RepeatInterval.FORTNIGHT,
                "Complete symptom tracker test");
            reminderData.isDefault = true;
            reminderData.isActive = TrackerManager.GetLastSymptomData() != null;
            reminderData.SetupReminder();
            AddReminder(reminderData);

            // add default Asthma test reminder
            reminderData = new ReminderData(_DEFAULT_SAA_ACT_KEY, DateTime.Today.Tomorrow(), DateTime.MaxValue,
                RepeatInterval.WEEK,
                "Take asthma control test");
            reminderData.isDefault = true;
            reminderData.isActive = TrackerManager.GetLastAsthmaData() != null;
            reminderData.SetupReminder();

            AddReminder(reminderData);

            PlayerPrefs.SetInt(_DEFAULT_REMINDERS_ADDED_KEY, 1);

            SaveProgress(false);
        }

        for (int i = 0; i < _reminders.Count; i++)
        {
            _reminders[i].OnDataUpdate += OnReminderDataUpdate;
            _reminders[i].CheckReminderData();
        }
    }

    public void ActivateSymptomTrackerDefaultReminder(DateTime testCompleteDateTime)
    {
        ReminderData data =  _reminders.Find(x => x.id == _DEFAULT_SAA_ST_KEY);
        data.fireDate = testCompleteDateTime;
        data.SetActive(true);
    }
    
    public void ActivateAsthmaControlTestDefaultReminder(DateTime testCompleteDateTime)
    {
        ReminderData data =  _reminders.Find(x => x.id == _DEFAULT_SAA_ACT_KEY);
        data.fireDate = testCompleteDateTime;
        data.SetActive(true);
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


    #region API Methods

    public void RegisterNotificationTypes(NotificationType notificationTypes)
    {
        NPBinding.NotificationService.RegisterNotificationTypes(notificationTypes);
    }

    public NotificationType EnabledNotificationTypes()
    {
        return NPBinding.NotificationService.EnabledNotificationTypes();
    }

    public string ScheduleLocalNotification(CrossPlatformNotification notification)
    {
        return NPBinding.NotificationService.ScheduleLocalNotification(notification);
    }

    public void CancelLocalNotification(string notificationID)
    {
        NPBinding.NotificationService.CancelLocalNotification(notificationID);
    }

    public void CancelAllLocalNotifications()
    {
        NPBinding.NotificationService.CancelAllLocalNotification();
    }

    public void ClearNotifications()
    {
        NPBinding.NotificationService.ClearNotifications();
    }

    #endregion


    #region API Callback Methods

    private void DidLaunchWithLocalNotificationEvent(CrossPlatformNotification notification)
    {
        Debug.Log(
            $"Application did launch with local notification with id: {notification.GetNotificationID()}, fire date: {notification.FireDate}, interval: {notification.RepeatInterval}");

        // try to find ReminderData associated with current notification and check, should we update data or not (for example for 1 time notifications what must be removed from the NotificationData)
    }

    private void DidReceiveLocalNotificationEvent(CrossPlatformNotification notification)
    {
        Debug.Log(
            $"Application received local notification with id: {notification.GetNotificationID()}, fire date: {notification.FireDate}, interval: {notification.RepeatInterval}");

        // try to find ReminderData associated with current notification and check, should we update data or not (for example for 1 time notifications what must be removed from the NotificationData)
        // Also we must keep tracking of past reminders events to show it in a past dates
        ReminderData data = GetReminderByNotificationId(notification.GetNotificationID());
        if (data != null)
        {
            Debug.Log($"This notification is a part of reminder by id: {data.id} and title: {data.title}");
            data.AddToHistory(notification.FireDate);
        }
        else
        {
            Debug.LogWarning("There is no reminder data found for current notification!");            
            return;
        }

        SaveProgress(false);
        
        // show popup notifications
        UIManager.NotificationManager.ShowNotification(
            "TwoOptionsIconUINotification",
            -1,
            false,
            "",
            "Take asthma control test",
            _clockIcon,
            new string[] {"No", "Yes"},
            new string[] {"Not completed", "Completed"},
            new UnityAction[]
            {
                null,
                () => data.SetDone(true)
            },
            () => SaveProgress(false)
        );
    }

    #endregion


    #region SaveGame callbacks

    /// <summary>
    /// Save Event.
    /// </summary>
    /// <param name="identifier">Identifier.</param>
    /// <param name="value">Value.</param>
    /// <param name="settings">Settings.</param>
    private void SaveGame_OnSaved(string identifier, object value, SaveGameSettings settings)
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
    private void SaveGame_OnLoaded(string identifier, object result, Type type, object defaultValue,
        SaveGameSettings settings)
    {
        Debug.LogFormat("{0} Loaded Successfully", identifier);
    }

    #endregion

    
    private void OnReminderDataUpdate(ReminderData reminderData)
    {
        // save each time some important changes been made. NOTE - make sure event is not triggered without true need.
        SaveProgress(false);
    }

    
    [CommandHandler(Description = "Remove all reminders")]
    public void RemoveAllReminders()
    {
        for (int i = 0; i < _reminders.Count; i++)
        {
            _reminders[i].Reset();
        }

        _reminders.Clear();
        SaveGame.Delete(_ACTUAL_REMINDERS_FILE_PATH);
        PlayerPrefs.DeleteKey(_DEFAULT_REMINDERS_ADDED_KEY);

        OnRemindersUpdate?.Invoke(GetAllReminders());
    }

    public void AddReminder(ReminderData data)
    {
        _reminders.Add(data);

        SaveProgress(true);
    }

    /// <summary>
    /// Deleting reminder means it still will be cached in to show in calendar history, but will not be visible in ReminderScreen and can't be restored. At least for now.
    /// </summary>
    /// <param name="data"></param>
    public void DeleteReminder(string id)
    {
        ReminderData data = _reminders.Find(x => x.id == id);
        if (data == null)
        {
            Debug.LogWarning("Can't find reminder by id: " + id);
        }
        else
        {
            data.Delete();

            SaveProgress(true);
        }
    }

    public void SaveProgress(bool triggerEvent)
    {
        SaveGame.Save(_ACTUAL_REMINDERS_FILE_PATH, _reminders);
        if (triggerEvent)
        {
            OnRemindersUpdate?.Invoke(GetAllReminders());
        }
    }

    public List<ReminderData> GetAllReminders(bool includeInactive = true, bool includeDeleted = false)
    {
        if (includeInactive)
        {
            return includeDeleted ? _reminders : _reminders.FindAll(x => !x.isDeleted);
        }

        return includeDeleted
            ? _reminders.FindAll(x => x.isActive)
            : _reminders.FindAll(x => x.isActive && !x.isDeleted);
    }

    public List<ReminderData> GetRemindersByDate(DateTime dateTime, bool includePast)
    {
        return _reminders.Where(x => x.HasNotificationByDate(dateTime, includePast)).ToList();
    }

    public ReminderData GetReminderByNotificationId(string id)
    {
        return _reminders.Find(x => x.HasNotificationWithId(id));
    }

    public ReminderData CreateSimpleTemplateReminder()
    {
        // make pretty time like 12:00am
        DateTime nowFixed = DateTime.Today.Date +
                            DateTime.ParseExact("03:00 PM", "hh:mm tt", CultureInfo.InvariantCulture).TimeOfDay;
        return new ReminderData(Guid.NewGuid().ToString(), nowFixed, DateTime.MaxValue,
            RepeatInterval.ONCE, "");
    }

    public CrossPlatformNotification CreateNotification(string title, long fireAfterSec,
        eNotificationRepeatInterval repeatInterval)
    {
        // User info
        IDictionary userInfo = new Dictionary<string, string>();
        // userInfo["data"] = "custom data";

        CrossPlatformNotification.iOSSpecificProperties _iosProperties =
            new CrossPlatformNotification.iOSSpecificProperties();
        _iosProperties.HasAction = false;
        _iosProperties.AlertAction = null;

        CrossPlatformNotification.AndroidSpecificProperties _androidProperties =
            new CrossPlatformNotification.AndroidSpecificProperties();
        _androidProperties.ContentTitle = title;
        _androidProperties.TickerText = title;
        _androidProperties.LargeIcon =
            "App-Icon.png"; //Keep the files in Assets/PluginResources/Android or Common folder.

        CrossPlatformNotification notification = new CrossPlatformNotification();
        notification.AlertBody = title; //On Android, this is considered as ContentText
        notification.FireDate = DateTime.Now.AddSeconds(fireAfterSec);
        notification.RepeatInterval = repeatInterval;
        notification.SoundName =
            "Notification.mp3"; //Keep the files in Assets/PluginResources/Android or iOS or Common folder.
        notification.UserInfo = userInfo;
        notification.iOSProperties = _iosProperties;
        notification.AndroidProperties = _androidProperties;

        return notification;
    }

    public static TimeDifference GetDateTimeDifference(DateTime dateA, DateTime dateB)
    {
        int days;
        int months;
        int years;

        int fird = dateA.Day;
        int lasd = dateB.Day;

        int firm = dateA.Month;
        int lasm = dateB.Month;

        if (fird >= lasd)
        {
            days = fird - lasd;
            if (firm >= lasm)
            {
                months = firm - lasm;
                years = dateA.Year - dateB.Year;
            }
            else
            {
                months = (firm + 12) - lasm;
                years = dateA.AddYears(-1).Year - dateB.Year;
            }
        }
        else
        {
            days = (fird + 30) - lasd;
            if ((firm - 1) >= lasm)
            {
                months = (firm - 1) - lasm;
                years = dateA.Year - dateB.Year;
            }
            else
            {
                months = (firm - 1 + 12) - lasm;
                years = dateA.AddYears(-1).Year - dateB.Year;
            }
        }

        if (days < 0)
        {
            days = 0 - days;
        }

        if (months < 0)
        {
            months = 0 - months;
        }

        TimeSpan ts = dateA.Subtract(dateB);

        TimeDifference td = new TimeDifference
        {
            years = years,
            months = months,
            days = days,
            hours = ts.Hours,
            minutes = ts.Minutes,
            seconds = ts.Seconds
        };
        // Debug.Log($"Years: {years}, Months: {months}, Days: {days}, Hours: {ts.Hours}, Minutes: {ts.Minutes}, Seconds: {ts.Seconds}");

        return td;
    }
}