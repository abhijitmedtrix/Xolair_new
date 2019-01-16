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
        _dateText.text = DateTime.Today.ToString("d/M/yyyy  ddd");

        // set time
        _timeText.text = string.Format(_initialTimeText, DateTime.Now.ToString("hh:mm", CultureInfo.InvariantCulture),
            DateTime.Now.ToString("tt", CultureInfo.InvariantCulture));

        // listen for minute change to update UI
        UpdateManager.OnMinuteChange += UpdateManagerOnOnMinuteChange;
    }

    private void OnDestroy()
    {
        UpdateManager.OnMinuteChange -= UpdateManagerOnOnMinuteChange;
    }

    private void UpdateManagerOnOnMinuteChange()
    {
        _timeText.text = DateTime.Today.ToString("tt", CultureInfo.InvariantCulture);
    }
}