using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphController : MonoBehaviour
{
    [SerializeField] protected GameObject _pointPrefab;
    [SerializeField] protected RectTransform _pointsContainer;
    [SerializeField] protected Text[] _labels;

    private List<GameObject> pointsList = new List<GameObject>();

    public void UpdateLabels(float maxValue)
    {
        // 1st label is always 0
        float splitValue = maxValue / (_labels.Length - 1);
        
        for (int i = 0; i < _labels.Length; i++)
        {
            _labels[i].text = (i * splitValue).ToString();
        }
    }
    
    public void SetPoints()
    {
        // remove all old points
        for (int i = 0; i < pointsList.Count; i++)
        {
            ObjectPool.Destroy(pointsList[i]);
        }
        
        pointsList.Clear();

        // add new
        for (int i = 0; i < 10; i++)
        {
            GameObject point = ObjectPool.Instantiate(_pointPrefab, Vector3.zero, Quaternion.identity, _pointsContainer);
            pointsList.Add(point);
        }
    }
}