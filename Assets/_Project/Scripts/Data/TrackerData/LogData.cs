using System;
using UnityEngine;
using App.Data.SSA;
using App.Data.CSU;

namespace App.Data
{
    public class LogData
    {
        public DateTime date;
        
        // SSA
        public SymptomData symptomData;
        public AsthmaData asthmaData;

        // CSU
        public CSUData csuData;
        public UASData uasData;

        public Action OnUpdate;

        public void UpdateData(SymptomData data)
        {
            symptomData = data;
            OnUpdate?.Invoke();
        }

        public void UpdateData(AsthmaData data)
        {
            asthmaData = data;
            OnUpdate?.Invoke();
        }

        public void UpdateData(CSUData data)
        {
            csuData = data;
            OnUpdate?.Invoke();
        }

        public void UpdateData(UASData data)
        {
            uasData = data;
            OnUpdate?.Invoke();
        }

        public QuestionBasedTrackerData TryGetData(TrackerManager.TrackerType type)
        {
            switch (type)
            {
                case TrackerManager.TrackerType.Asthma:
                    return asthmaData;

                case TrackerManager.TrackerType.Symptom:
                    return symptomData;

                case TrackerManager.TrackerType.CSU:
                    return csuData;

                case TrackerManager.TrackerType.UAS:
                    return uasData;
            }

            return null;
        }

        public void DeleteLocalContent()
        {
            if (csuData != null)
            {
                csuData.DeletePhotos();
            }
        }
    }
}