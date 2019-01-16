using UnityEngine;
using App.Data.CSU;
using System;
using MaterialUI;

public class UASTrackerScreen : TrackerScreen
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
        _trackerData = new UASData(DateTime.Today);
        
        ScreenManager.Instance.Set(16);

        base.StartTracker();
    }

    protected override void CompleteTracker()
    {
        base.CompleteTracker();

        // TODO - remove it later, not clear why was it used
        AppManager.SecondTest = true;
    }

    public override void SubmitAnswer()
    {
        base.SubmitAnswer();

        // TODO - make sure that should be used... 
        if (AppManager.Instance.CurrentMode == AppManager.Mode.CSU)
        {
            if (_trackerData.GetScore() >= 6)
            {
                Debug.Log("going");
                if (!AppManager.cusnotfn.Contains("PLEASE TAKE UAS TEST"))
                {
                    Debug.Log("saving");
                    AppManager.cusnotfn.Add("PLEASE TAKE UAS TEST");
                }

                if (_objectToShow != null)
                {
                    _objectToShow.SetActive(true);
                }
            }
        }
    }
}