using System.Collections.Generic;
using System.Linq;
using Draw2DShapesLite;
using UnityEngine;

public class GraphRenderer : MonoBehaviour
{
    [SerializeField] protected Camera _camera;
    [SerializeField] protected Draw2D _drawer;
    [SerializeField] protected iTweenPath _iTweenPath;
    [SerializeField] protected bool _useSmoothGraph;

    // protected Path _graphPath;
    protected Vector3[] _graphPoints;
    protected Vector3[] _basePoints = new Vector3[2];
    protected Vector3[] _smoothedPoints = new Vector3[100];
    [SerializeField]
    protected RenderTexture _renderTexture;

    protected int _maxValue;
    protected int _daysToShow;

    public void Init(int maxValue, int daysToShow)
    {
        if (_renderTexture != null)
        {
            Destroy(_renderTexture);
        }

        _graphPoints = new Vector3[daysToShow];

        _basePoints[0] = new Vector3(daysToShow - 1, -0.0001f, 0);
        _basePoints[1] = new Vector3(0, -0.0001f, 0);

        Debug.Log($"daysToShow: {daysToShow}, maxValue: {maxValue}");
        _daysToShow = daysToShow;
        _maxValue = maxValue;

        // update camera position
        _camera.transform.position = new Vector3((_daysToShow - 1) / 2f, _maxValue / 2f, -1);

        // set the size of the viewing volume you'd like the orthographic Camera to pick up (5)
        _camera.orthographicSize = _maxValue / 2f;

        // set the orthographic Camera Viewport size and position
        _camera.aspect = (float) (_daysToShow - 1) / _maxValue;

        // set new texture 
        _renderTexture = new RenderTexture(Screen.width, Mathf.FloorToInt(Screen.width / _camera.aspect), 0,
            RenderTextureFormat.ARGB32);
        _camera.targetTexture = _renderTexture;
    }

    public void UpdateGraph(List<PatientJournalScreen.ScoreStruct[]> values)
    {
        // format points to Vector3[]
        for (int i = 0; i < values.Count; i++)
        {
            // Debug.Log("Value: " + values[i]);
            _graphPoints[i] = new Vector3(i, values[i][0].score + values[i][1].score, 0);
        }

        // draw a smooth spline using these points
        _iTweenPath.nodes = _graphPoints.ToList();

        if (_useSmoothGraph)
        {
            for (int i = 0; i < _smoothedPoints.Length; i++)
            {
                _smoothedPoints[i] = iTween.PointOnPath(_graphPoints, 1f / _smoothedPoints.Length * i);
                // _smoothedPoints[i] = _iTweenPath.PathGetPoint(1f / 50 * i);
                
                // using smooth graph we cant allow any y value < 0 otherwise graph mesh will not be drawn
                _smoothedPoints[i].y = Mathf.Clamp(_smoothedPoints[i].y, 0, _maxValue);
            }

            /*
            _graphPath = new Path(PathType.CatmullRom, _graphPoints, 5, Color.red);
            _smoothedPoints = new Vector3[50];
            GameObject dummy = new GameObject("Dummy");
            dummy.transform.position = _graphPoints[0];
            Tweener t = dummy.transform.DOPath(_graphPath, 1);
            t.ForceInit();
            
            for (int i = 0; i < 50; i++)
            {
                _smoothedPoints[i] = t.PathGetPoint(1f / 50 * i);
            }
    
            t.Kill();
            */
        }

        // update mesh
        // _drawer.vertices = _points.ToList();
        List<Vector3> points = new List<Vector3>(_basePoints);
        if (_useSmoothGraph)
        {
            points.AddRange(_smoothedPoints.ToList());
        }
        else
        {
            points.AddRange(_graphPoints.ToList());
        }

        _drawer.vertices = points;
        _drawer.MakeMesh();
    }

    public RenderTexture GetRenderTexture()
    {
        return _camera.targetTexture;
    }

    public Vector3 GetLastDayPointViewportPosition()
    {
        Vector3 point = _graphPoints[_graphPoints.Length - 1];
        if (point.y <= 0) point.y = 0;

        // return (Vector3)RectTransformUtility.WorldToScreenPoint(_camera, point);
        return _camera.WorldToViewportPoint(point);
    }
    
    private void OnDrawGizmos()
    {
        if (Application.isPlaying && _graphPoints != null)
        {
            /*
            Vector3[] smoothedPoints = Curver.MakeSmoothCurve(_graphPoints, 1);
            for (int i = 0; i < smoothedPoints.Length; i++)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(smoothedPoints[i], 0.05f);
            }
            */

            for (int i = 0; i < _graphPoints.Length; i++)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawSphere(_graphPoints[i], 0.075f);
            }

            if (_useSmoothGraph && _smoothedPoints != null)
            {
                for (int i = 0; i < _smoothedPoints.Length; i++)
                {
                    Gizmos.color = Color.yellow;
                    Gizmos.DrawSphere(_smoothedPoints[i], 0.1f);
                }
            }
        }
    }
}