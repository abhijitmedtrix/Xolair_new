using UnityEngine;
using App.Data.SSA;
using System;
using MaterialUI;

public class AsthmaControlTrackerScreen : TrackerScreen
{
    private void Start()
    {
        _trackerType = TrackerManager.TrackerType.Asthma;
    }

    public override void StartTracker()
    {
        // create new data, because now we don't need to modify existing data until it's been submitted by user in a last step
        _trackerData = new AsthmaData(DateTime.Today);
        
        ScreenManager.Instance.Set(5);

        base.StartTracker();
    }
    
    public override void SubmitResults()
    {
        base.SubmitResults();
        
        ScreenManager.Instance.Set(4);
    }

    public override void SubmitAnswer()
    {
        base.SubmitAnswer();

        if (_trackerData != null)
        {
            if (_trackerData.GetScore() >= 1)
            {
                if (!AppManager.Saanotfn.Contains("PLEASE VISIT A PHYSICIAN"))
                {
                    AppManager.Saanotfn.Add("PLEASE VISIT A PHYSICIAN");
                }
            }
        }
    }
}