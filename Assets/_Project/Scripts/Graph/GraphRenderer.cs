using System.Collections.Generic;
using System.Linq;
using Draw2DShapesLite;
using UnityEngine;

public class GraphRenderer : MonoBehaviour
{
    [SerializeField] protected bool _useLinesOnly;
    [SerializeField] protected Camera _camera;
    [SerializeField] protected Draw2D _drawer;
    [SerializeField] protected iTweenPath _iTweenPath;
    [SerializeField] protected bool _useSmoothGraph;
    [SerializeField] protected LineRenderer _lineRenderer;
    [SerializeField] protected MeshFilter _meshFilter;
    [SerializeField] protected RenderTexture _renderTexture;

    protected int _maxValue;
    protected int _daysToShow;

    protected Vector3[] _graphPoints;
    protected Vector3[] _basePoints = new Vector3[2];
    protected Vector3[] _smoothedPoints = new Vector3[100];

    private void Start()
    {
        // SetupTexture();
    }

    private void SetupTexture()
    {
        if (_renderTexture != null)
        {
            Destroy(_renderTexture);
        }

        _renderTexture = new RenderTexture(Screen.width, Mathf.FloorToInt(Screen.width / _camera.aspect), 0,
            RenderTextureFormat.ARGB32);
    }

    public void UpdateGraph(Vector3[] scorePoints, bool useLinesOnly, bool useSmoothGraph,
        int daysToShow, int maxValue)
    {
        SetupTexture();
        
        _useLinesOnly = useLinesOnly;
        _useSmoothGraph = useSmoothGraph;

        // base points for mesh bottom left and bottom right points
        _basePoints[0] = new Vector3(scorePoints[scorePoints.Length - 1].x, -0.0001f, 0);
        _basePoints[1] = new Vector3(0, -0.0001f, 0);

        // Debug.Log($"daysToShow: {daysToShow}, maxValue: {maxValue}");
        _daysToShow = daysToShow;
        _maxValue = maxValue;
        
        UpdateGraph(scorePoints);
    }

    public void UpdateGraph(Vector3[] scorePoints)
    {
        _graphPoints = scorePoints;

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
        }

        // setup points
        List<Vector3> points = new List<Vector3>(_basePoints);
        if (_useSmoothGraph)
        {
            points.AddRange(_smoothedPoints.ToList());
        }
        else
        {
            points.AddRange(_graphPoints.ToList());
        }

        if (_useLinesOnly)
        {
            _meshFilter.gameObject.SetActive(false);

            _lineRenderer.positionCount = _graphPoints.Length;
            _lineRenderer.SetPositions(_graphPoints);

            // CalculateLineMeshPoints(ref points);
            // DrawMesh(points);
        }
        else
        {
            _meshFilter.gameObject.SetActive(true);
            // update mesh
            _drawer.vertices = points;
            _drawer.MakeMesh();
        }
    }

    public RenderTexture GetRenderTexture()
    {
        return _renderTexture;
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

    // TEST DRAW LINE MESH BY LINE RENDERER
    private void DrawMesh(List<Vector3> points)
    {
        Vector3[] verticies = new Vector3[points.Count];

        for (int i = 0; i < verticies.Length; i++)
        {
            verticies[i] = points[i];
        }

        int[] triangles = new int[((points.Count / 2) - 1) * 6];

        //Works on linear patterns tn = bn+c
        int position = 6;
        for (int i = 0; i < (triangles.Length / 6); i++)
        {
            triangles[i * position] = 2 * i;
            triangles[i * position + 3] = 2 * i;

            triangles[i * position + 1] = 2 * i + 3;
            triangles[i * position + 4] = (2 * i + 3) - 1;

            triangles[i * position + 2] = 2 * i + 1;
            triangles[i * position + 5] = (2 * i + 1) + 2;
        }


        Mesh mesh = _meshFilter.mesh;
        mesh.Clear();
        mesh.vertices = verticies;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }

    private void CalculateLineMeshPoints(ref List<Vector3> points)
    {
        points.Clear();
        GameObject caret = null;
        caret = new GameObject("Lines");

        Vector3 left, right; // A position to the left of the current line

        // For all but the last point
        for (var i = 0; i < _lineRenderer.positionCount - 1; i++)
        {
            caret.transform.position = _lineRenderer.GetPosition(i);
            caret.transform.LookAt(_lineRenderer.GetPosition(i + 1));
            right = caret.transform.position + transform.right * _lineRenderer.startWidth / 2;
            left = caret.transform.position - transform.right * _lineRenderer.startWidth / 2;
            points.Add(left);
            points.Add(right);
        }

        // Last point looks backwards and reverses
        caret.transform.position = _lineRenderer.GetPosition(_lineRenderer.positionCount - 1);
        caret.transform.LookAt(_lineRenderer.GetPosition(_lineRenderer.positionCount - 2));
        right = caret.transform.position + transform.right * _lineRenderer.startWidth / 2;
        left = caret.transform.position - transform.right * _lineRenderer.startWidth / 2;
        points.Add(left);
        points.Add(right);
        Destroy(caret);
    }
}