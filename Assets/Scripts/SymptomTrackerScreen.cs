﻿using UnityEngine;
using App.Data.SSA;
using System;
using MaterialUI;

public class SymptomTrackerScreen : TrackerScreen
{
    [Header("Notification mess...")] [SerializeField]
    protected GameObject _objectToShow;

    private void Start()
    {
        _progressController.OnBackClicked += SetPreviousQuestion;
        _trackerType = TrackerManager.TrackerType.Symptom;
    }

    public override void StartTracker()
    {
        // create new data, because now we don't need to modify existing data until it's been submitted by user in a last step
        _trackerData = new SymptomData(DateTime.Today);

        ScreenManager.Instance.Set(6);

        base.StartTracker();
    }

    public override void SubmitResults()
    {
        base.SubmitResults();
        
        ScreenManager.Instance.Set(4);
    }

    protected override void TryCompleteTracker()
    {
        base.TryCompleteTracker();
    }
}