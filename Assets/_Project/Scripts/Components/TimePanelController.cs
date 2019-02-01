using System;
using System.Globalization;
using DTools;
using UnityEngine;
using UnityEngine.UI;

public class TimePanelController : MonoBehaviour
{
    [SerializeField] protected Text _dateText, _timeText;
    protected string _initialTimeText;

    private void Start()
    {
        _initialTimeText = _timeText.text;

        // set the date
        string dayInShortCapitals = DateTime.Today.ToString("ddd").ToUpper();
        string date = DateTime.Today.ToString("d/M/yyyy");
        _dateText.text = $"{dayInShortCapitals}, {date}";

        // set time
        UpdateManagerOnOnMinuteChange();

        // listen for minute change to update UI
        UpdateManager.OnMinuteChange += UpdateManagerOnOnMinuteChange;
    }

    private void OnDestroy()
    {
        UpdateManager.OnMinuteChange -= UpdateManagerOnOnMinuteChange;
    }

    private void UpdateManagerOnOnMinuteChange()
    {
        _timeText.text = string.Format(_initialTimeText, DateTime.Now.ToString("hh:mm", CultureInfo.InvariantCulture),
            DateTime.Now.ToString("tt", CultureInfo.InvariantCulture));
    }
}