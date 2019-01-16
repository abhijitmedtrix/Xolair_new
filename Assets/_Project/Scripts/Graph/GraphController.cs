using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphController : MonoBehaviour
{
    [SerializeField] protected GameObject _graphLabelPrefab;
    [SerializeField] protected RectTransform _labelsContainer;
    [SerializeField] protected RectTransform _thisRectTransform;
    [SerializeField] protected List<Text> _labels;
    [SerializeField] protected Camera _camera;
    [SerializeField] protected GraphRenderer _graphRenderer;
    [SerializeField] protected RawImage _graphImage;

    protected int _daysToShow;
    protected int _maxValue;
    protected Vector3[] _graphPoints;
    protected float _totalWidth => _graphPoints.Length;

    private void Awake()
    {
        if (_thisRectTransform == null)
        {
            _thisRectTransform = (RectTransform) this.transform;
        }
    }

    public void Initialize(PatientJournalScreen.GraphScruct[] datas, bool useLinesOnly, bool useSmoothGraph,
        int maxValue, int daysToShow, int numOfLabels)
    {
        _graphPoints = new Vector3[datas.Length];

        // format datas to Vector3[]
        for (int i = 0; i < datas.Length; i++)
        {
            _graphPoints[i] = new Vector3(i, datas[i].interpolatedScore, 0);
        }

        _daysToShow = daysToShow;
        _maxValue = maxValue;

        // orthographic size must fit to max value
        _camera.orthographicSize = _maxValue / 2f;

        // and define width by setting proper aspect to cover all days to show
        _camera.aspect = (float) _daysToShow / _maxValue;

        // update camera position
        _camera.transform.localPosition = new Vector3(0, _camera.orthographicSize, -1);

        // setup GraphRenderer before assigning the texture
        _graphRenderer.UpdateGraph(_graphPoints, useLinesOnly, useSmoothGraph, _daysToShow, _maxValue);

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
        Vector3 localPos = _camera.transform.localPosition;
        localPos.x = _totalWidth * sliderValue;
        _camera.transform.localPosition = localPos;
    }
}