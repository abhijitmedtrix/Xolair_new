using UnityEngine;
using App.Data.SSA;
using System;
using App.Data;
using App.Data.CSU;
using MaterialUI;

public class SymptomTrackerScreen : TrackerScreen
{
    [Header("Notification mess...")] [SerializeField]
    protected GameObject _objectToShow;

    private void Start()
    {
        _progressController.OnBackClicked += SetPreviousQuestion;
    }

    public override void StartTracker()
    {
        // create new data, because now we don't need to modify existing data until it's been submitted by user in a last step
        _trackerData = new SymptomData(DateTime.Today);

        ScreenManager.Instance.Set(6);

        base.StartTracker();
    }

    protected override void CompleteTracker()
    {
        base.CompleteTracker();

        // TODO - remove it later, not clear why was it used
        AppManager.FirstTest = true;
    }

    public override void SubmitAnswer()
    {
        base.SubmitAnswer();

        // TODO - make sure that should be used... 
        if (AppManager.Instance.CurrentMode == AppManager.Mode.SAA)
        {
            if (_trackerData != null)
            {
                if (_trackerData.GetScore() > 19)
                {
                    if (!AppManager.Saanotfn.Contains("PLEASE TAKE SYMPTOM TRACKER TEST"))
                    {
                        AppManager.Saanotfn.Add("PLEASE TAKE SYMPTOM TRACKER TEST");
                    }

                    if (_objectToShow != null)
                    {
                        _objectToShow.SetActive(true);
                    }
                }
            }
        }
    }
}