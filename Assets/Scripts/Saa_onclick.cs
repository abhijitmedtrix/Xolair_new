using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saa_onclick : MonoBehaviour {
    [SerializeField]  private PatientJournalScreen patientJournalScreen;
    public void OnClick()
    {
        if(gameObject.tag=="ACT")
        {
            patientJournalScreen._TrackerType = TrackerManager.TrackerType.Asthma;
        }
        else if(gameObject.tag=="ST")
        {
            patientJournalScreen._TrackerType = TrackerManager.TrackerType.Symptom;
        }
    }
}
