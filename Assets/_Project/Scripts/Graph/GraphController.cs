using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphController : MonoBehaviour
{
    [SerializeField] protected GameObject _graphLabelPrefab;
    [SerializeField] protected RectTransform _labelsContainer;
    [SerializeField] protected RectTransform _thisRectTransform;
    [SerializeField] protected Camera _camera;
    [SerializeField] protected GraphRenderer _graphRenderer;
    [SerializeField] protected RawImage _graphImage;

    protected int _validPointsCounter;
    protected List<Text> _labels = new List<Text>();
    protected int _daysToShow;
    protected int _maxValue;
    // protected Vector3[] _graphPoints;
    protected List<Vector3> _graphPoints = new List<Vector3>();
    protected float _totalWidth;

    private void Awake()
    {
        if (_thisRectTransform == null)
        {
            _thisRectTransform = (RectTransform) this.transform;
        }
    }

    public void Initialize(PatientJournalScreen.GraphData[] datas, bool createMesh, bool useSmoothGraph,
        int maxValue, int daysToShow, int numOfLabels)
    {
        _graphPoints.Clear();
        _totalWidth = datas.Length;
        
        // _graphPoints = new Vector3[datas.Length];

        // format datas to Vector3[]
        // _validPointsCounter = 0;
        for (int i = 0; i < datas.Length; i++)
        {
            if (!datas[i].dontUseInGraph)
            {
                _graphPoints.Add(new Vector3(i, datas[i].interpolatedScore, 0));
                // _validPointsCounter++;
            }
        }
        if (_graphPoints.Count < 2) return;

        _daysToShow = daysToShow;
        _maxValue = maxValue;

        // orthographic size must fit to max value
        _camera.orthographicSize = _maxValue / 2f;

        // and define width by setting proper aspect to cover all days to show
        _camera.aspect = (float) _daysToShow / _maxValue;

        // update camera position
        _camera.transform.localPosition = new Vector3(0, _camera.orthographicSize, -1);

        // setup GraphRenderer before assigning the texture
        _graphRenderer.UpdateGraph(_graphPoints, createMesh, useSmoothGraph, _daysToShow, _maxValue);

        // set new texture 
        _camera.targetTexture = _graphRenderer.GetRenderTexture();
        _graphImage.texture = _graphRenderer.GetRenderTexture();

        UpdateLabels(_maxValue, numOfLabels);
    }

    private void UpdateLabels(float maxValue, int numOfLabels)
    {
        // 1st label is always 0
        float splitValue = maxValue / (numOfLabels - 1);

        // remove all labels
        for (int i = 0; i < _labels.Count; i++)
        {
            ObjectPool.Destroy(_labels[i].gameObject);
        }

        _labels.Clear();

        float anchorInterval = 1f / (numOfLabels - 1);

        for (int i = 0; i < numOfLabels; i++)
        {
            Text label = ObjectPool.Instantiate(_graphLabelPrefab.gameObject, Vector3.zero, Quaternion.identity,
                _labelsContainer).GetComponent<Text>();
            _labels.Add(label);

            // if it's a decimal
            if (maxValue < numOfLabels)
            {
                label.text = (i * splitValue).ToString("F1");
            }
            else
            {
                label.text = Mathf.RoundToInt(i * splitValue).ToString();
            }

            label.rectTransform.anchorMin = label.rectTransform.anchorMax = new Vector2(1, i * anchorInterval);
            label.rectTransform.anchoredPosition = Vector2.zero;
        }
    }

    public void UpdateCameraView(float sliderValue)
    {
        // we need at least 2 real data points to render
        if (_graphPoints.Count < 2) return;

        Vector3 localPos = _camera.transform.localPosition;
        localPos.x = _totalWidth * sliderValue;
        _camera.transform.localPosition = localPos;
    }
}