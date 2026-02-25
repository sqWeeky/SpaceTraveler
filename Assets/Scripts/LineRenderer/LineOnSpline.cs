using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Splines;

[RequireComponent(typeof(UnityEngine.LineRenderer))]
public class LineOnSpline : MonoBehaviour
{
    [SerializeField] private SplineContainer _splineContainer;
    [SerializeField] private int _segmentsPerKnot = 10;
    [SerializeField] private bool _updateInTime = false;

    private UnityEngine.LineRenderer _lineRenderer;
    private Spline _spline;

    private void Awake()
    {
        _lineRenderer = GetComponent<UnityEngine.LineRenderer>();

        if (_splineContainer == null)
            return;

        DrawLine();
    }

    private void Update()
    {
        if (_updateInTime)
            DrawLine();
    }

    private void DrawLine()
    {
        if (_splineContainer == null || _splineContainer.Spline == null) return;

        _spline = _splineContainer.Spline;

        if (_spline.Count < 2) return;

        int totalPoints = (_spline.Count - 1) * _segmentsPerKnot + 1;
        _lineRenderer.positionCount = totalPoints;
        int pointIndex = 0;

        for (int i = 0; i < _spline.Count - 1; i++)
        {
            for (int j = 0; j < _segmentsPerKnot; j++)
            {
                float t = j / (float)_segmentsPerKnot;
                Vector3 point = _splineContainer.EvaluatePosition(i + t);
                _lineRenderer.SetPosition(pointIndex++, point);
            }
        }

        Vector3 lastPos = _splineContainer.transform.TransformPoint(_spline[^1].Position);
        _lineRenderer.SetPosition(pointIndex, lastPos);
    }

    [Button]
    private void OnLineDraw()
    {
        if (_lineRenderer == null) 
            _lineRenderer = GetComponent<UnityEngine.LineRenderer>();

        DrawLine();
    }
}