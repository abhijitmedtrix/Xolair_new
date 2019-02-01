using UnityEngine;
using App.Data.CSU;
using System;
using MaterialUI;

public class UASTrackerScreen : TrackerScreen
{
    private void Start()
    {
        _progressController.OnBackClicked += SetPreviousQuestion;
        _trackerType = TrackerManager.TrackerType.UAS;
    }
    
    public override void SubmitResults()
    {
        base.SubmitResults();
        
        ScreenManager.Instance.Set(15);
    }

    public override void StartTracker()
    {
        // create new data, because now we don't need to modify existing data until it's been submitted by user in a last step
        _trackerData = new UASData(DateTime.Today);
        
        ScreenManager.Instance.Set(16);

        base.StartTracker();
    }
}