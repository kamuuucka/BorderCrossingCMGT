using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class SplineGenerator : MonoBehaviour
{
    [SerializeField] private float radius = 5f; // Radius of the circle
    [SerializeField] private int numberOfKnots = 10; // Number of knots (control points)
    
    private SplineContainer _splineContainer;
    private SplineExtrude _splineExtrude;
    private Spline _spline;

    private void Start()
    {
        // Create a new SplineContainer if not already attached to the GameObject
        _splineContainer = GetComponent<SplineContainer>();
        _splineExtrude = GetComponent<SplineExtrude>();
        if (_splineContainer == null)
        {
            _splineContainer = gameObject.AddComponent<SplineContainer>();
        }

        CreateCircularSpline();

        foreach (var knot in _splineContainer.Spline.Knots)
        {
            
        }
    }

    private void CreateCircularSpline()
    {
        _splineContainer.Spline.Clear();

        // Calculate the angle increment for each knot
        float angleIncrement = 360f / numberOfKnots;

        for (int i = 0; i < numberOfKnots; i++)
        {
            // Convert angle to radians
            float angleInRadians = Mathf.Deg2Rad * i * angleIncrement;

            // Calculate the position of each knot based on the radius
            float x = Mathf.Cos(angleInRadians) * radius;
            float z = Mathf.Sin(angleInRadians) * radius;

            // Create a knot at the calculated position with auto tangents
            var knot = new BezierKnot(new Vector3(x, 0, z));

            // Add the knot to the spline
            _splineContainer.Spline.Add(knot);
        }

        // Close the spline loop to form a perfect circle
        _splineContainer.Spline.Closed = true;

        _splineContainer.Spline.SetTangentMode(TangentMode.AutoSmooth);
    }
}
