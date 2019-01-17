using UnityEngine;
using App.Data.SSA;
using System;
using MaterialUI;

public class AsthmaControlTrackerScreen : TrackerScreen
{
    [Header("Notification mess...")] [SerializeField]
    protected GameObject _objectToShow;

    public override void StartTracker()
    {
        // create new data, because now we don't need to modify existing data until it's been submitted by user in a last step
        _trackerData = new AsthmaData(DateTime.Today);
        
        ScreenManager.Instance.Set(5);

        base.StartTracker();
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

                if (_objectToShow != null)
                {
                    _objectToShow.SetActive(true);
                }
            }
        }
    }
}