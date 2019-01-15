using System.Collections.Generic;
using App.Data.CSU;
using UnityEngine;
using UnityEngine.UI;

public class CSUViewController : MonoBehaviour
{
    [SerializeField] protected Text[] _hivesStats;
    [SerializeField] protected Text[] _itchesStats;

    protected List<Text[]> _stats = new List<Text[]>();

    private void Awake()
    {
        _stats.Add(_hivesStats);
        _stats.Add(_itchesStats);
    }

    public void UpdateData(CSUData data)
    {
        for (int i = 0; i < data.questionDataList.Count; i++)
        {
            var bodyParts = EnumUtil.GetValues<BodyPart>();

            int counter = 0;
            foreach (BodyPart bodyPart in bodyParts)
            {
                Debug.Log("BodyPart: "+bodyPart);
                _stats[i][counter].text = data.GetAnswerDescription(0, bodyPart);
                counter++;
            }
        }
    }
}