using System;
using UnityEngine;

public class LineSpecs : MonoBehaviour
{
    private float _radius;
    private float _angle;
    private float _startAngle;
    private LineRenderer _lineRenderer;

    public void SpawnLine(float lineWidth, Color lineColor, float radius, float angle, float startAngle)
    {
        _radius = radius;
        _angle = angle;
        _startAngle = startAngle;
        _lineRenderer = GetComponent<LineRenderer>();
        SetUpLine(lineWidth, lineColor);
        DrawLine();
    }

    private void DrawLine()
    {
        System.Collections.Generic.List<Vector3> positions = new System.Collections.Generic.List<Vector3>();
        
        float startAngle = _startAngle;
        float endAngle = _angle;
        
        int segmentPoints = 10;
        for (int j = 0; j <= segmentPoints; j++)
        {
            float t = j / (float)segmentPoints;
            float angle = Mathf.Lerp(startAngle, endAngle, t);
            float rad = Mathf.Deg2Rad * angle;
            float x = Mathf.Sin(rad) * _radius;
            float y = Mathf.Cos(rad) * _radius;
            positions.Add(new Vector3(x, y, 0));
        }
        
        _lineRenderer.positionCount = positions.Count;
        _lineRenderer.SetPositions(positions.ToArray());
    }

    private void SetUpLine(float lineWidth, Color lineColor)
    {
        _lineRenderer.useWorldSpace = false;
        _lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        _lineRenderer.startColor = lineColor;
        _lineRenderer.endColor = lineColor;
        _lineRenderer.startWidth = lineWidth;
        _lineRenderer.endWidth = lineWidth;
    }
}