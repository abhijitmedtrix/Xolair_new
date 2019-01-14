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
        _trackerData = TrackerManager.GetData(DateTime.Today, TrackerManager.TrackerType.Asthma) as AsthmaData;
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