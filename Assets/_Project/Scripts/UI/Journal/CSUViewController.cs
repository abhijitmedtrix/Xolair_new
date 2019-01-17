using System.Collections.Generic;
using App.Data.CSU;
using UnityEngine;
using UnityEngine.UI;

public class CSUViewController : MonoBehaviour
{
    [SerializeField] protected Text[] _hivesStats;
    [SerializeField] protected Text[] _itchesStats;
    [SerializeField] protected CanvasGroup _canavsGroup;
    
    protected List<Text[]> _stats = new List<Text[]>();

    private void Awake()
    {
        _stats.Add(_hivesStats);
        _stats.Add(_itchesStats);
    }

    public void UpdateData(CSUData data)
    {
        if (data == null)
        {
            // show empty fields
            for (int i = 0; i < _hivesStats.Length; i++)
            {
                _hivesStats[i].text = _itchesStats[i].text = string.Empty;
            }

            _canavsGroup.alpha = 0.5f;
        }
        else
        {
            for (int i = 0; i < data.questionDataList.Count; i++)
            {
                var bodyParts = EnumUtil.GetValues<BodyPart>();
    
                int counter = 0;
                foreach (BodyPart bodyPart in bodyParts)
                {
                    // Debug.Log("BodyPart: "+bodyPart);
                    _stats[i][counter].text = data.GetAnswerDescription(i, bodyPart);
                    counter++;
                }
            }
            _canavsGroup.alpha = 1f;
        }
    }
}