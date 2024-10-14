using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LineGenerator : MonoBehaviour
{
    [Tooltip("Texture that you want to apply to the line material.")] [SerializeField]
    private Texture2D lineTexture;

    [Tooltip("How many points should the arch have. Decides on the arch's smoothness.")] [SerializeField]
    private int archPoints = 10;

    private float _radius;
    private float _endAngle;
    private float _startAngle;
    private LineRenderer _lineRenderer;

    private void OnEnable()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    /// <summary>
    /// Spawn the specific line.
    /// </summary>
    /// <param name="lineWidth">Width of the line</param>
    /// <param name="lineColor">Color of the line</param>
    /// <param name="radius">Radius of the full circle</param>
    /// <param name="startAngle">Start angle of the line</param>
    /// <param name="endAngle">End angle of the line</param>
    /// <param name="linePosition">Position of the line</param>
    public void SpawnLine(float lineWidth, Color lineColor, float radius, float startAngle, float endAngle,
        Vector3 linePosition)
    {
        SetUpLine(lineWidth, lineColor);
        DrawLine(radius, startAngle, endAngle, linePosition);
    }

    /// <summary>
    /// Draws an arched line.
    /// </summary>
    /// <param name="radius">Radius of the full circle</param>
    /// <param name="startAngle">Start angle of the line</param>
    /// <param name="endAngle">End angle of the line</param>
    /// <param name="linePosition">Position of the line</param>
    private void DrawLine(float radius, float startAngle, float endAngle, Vector3 linePosition)
    {
        List<Vector3> positions = new();

        for (var j = 0; j <= archPoints; j++)
        {
            var t = j / (float)archPoints;
            var angle = Mathf.Lerp(startAngle, endAngle, t);
            var rad = Mathf.Deg2Rad * angle;
            var x = Mathf.Sin(rad) * radius;
            var y = Mathf.Cos(rad) * radius;
            positions.Add(new Vector3(linePosition.x + x, linePosition.y + y, linePosition.z));
        }

        _lineRenderer.positionCount = positions.Count;
        _lineRenderer.SetPositions(positions.ToArray());
    }

    /// <summary>
    /// Set up the line's specifics.
    /// </summary>
    /// <param name="lineWidth">Width of the line</param>
    /// <param name="lineColor">Color of the line</param>
    private void SetUpLine(float lineWidth, Color lineColor)
    {
        _lineRenderer.useWorldSpace = false;
        //Shader must be similar to Sprites/Default to change the colours through code
        _lineRenderer.material = new Material(Shader.Find("Sprites/Default"))
        {
            mainTexture = lineTexture
        };
        _lineRenderer.startColor = lineColor;
        _lineRenderer.endColor = lineColor;
        _lineRenderer.startWidth = lineWidth;
        _lineRenderer.endWidth = lineWidth;
    }
}