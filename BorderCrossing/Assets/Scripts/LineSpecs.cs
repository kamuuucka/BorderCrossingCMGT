using System;
using System.Collections.Generic;
using UnityEngine;

public class LineSpecs : MonoBehaviour
{
    [SerializeField] private Material lineMaterial;
    [SerializeField] private Texture2D lineTexture;
    private float _radius;
    private float _endAngle;
    private float _startAngle;
    private LineRenderer _lineRenderer;

    public void SpawnLine(float lineWidth, Color lineColor, float radius, float angle, float startAngle, Transform startingPosition)
    {
        _radius = radius;
        _endAngle = angle;
        _startAngle = startAngle;
        _lineRenderer = GetComponent<LineRenderer>();
        SetUpLine(lineWidth, lineColor);
        DrawLine(startingPosition);
    }

    private void DrawLine(Transform startingPosition)
    {
        List<Vector3> positions = new();

        var segmentPoints = 10;
        for (int j = 0; j <= segmentPoints; j++)
        {
            var t = j / (float)segmentPoints;
            var angle = Mathf.Lerp(_startAngle, _endAngle, t);
            var rad = Mathf.Deg2Rad * angle;
            var x = Mathf.Sin(rad) * _radius;
            var y = Mathf.Cos(rad) * _radius;
            var position = startingPosition.position;
            positions.Add(new Vector3(position.x + x, position.y + y, position.z));
        }
        
        _lineRenderer.positionCount = positions.Count;
        _lineRenderer.SetPositions(positions.ToArray());
    }

    private void SetUpLine(float lineWidth, Color lineColor)
    {
        _lineRenderer.useWorldSpace = false;
        _lineRenderer.material = lineMaterial;
        _lineRenderer.material.mainTexture = lineTexture;
        _lineRenderer.startColor = lineColor;
        _lineRenderer.endColor = lineColor;
        _lineRenderer.startWidth = lineWidth;
        _lineRenderer.endWidth = lineWidth;

    }
}