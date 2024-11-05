using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;

public class SplitSpline : MonoBehaviour
{
    [SerializeField] private int splitIndex = 5; // Index at which to split the spline
    private SplineContainer _splineContainer;
    private int _totalSplines;
    private int _radius = 10;

    private void OnEnable()
    {
        // Retrieve the existing SplineContainer
        _splineContainer = GetComponent<SplineContainer>();

        if (_splineContainer != null)
        {
            Debug.Log("I am splitting the spline!");
            // Get all knots
            var knots = GetAllKnots(_splineContainer.Spline);

            // Split the spline into two
            //SplitSplineAtIndex(knots, splitIndex);

            var newPoints = new Spline();
            for (int i = 0; i < splitIndex; i++)
            {
                var x = transform.position.x + _radius * Math.Cos(360f / splitIndex * (i + 1) * Mathf.Deg2Rad);
                var z = transform.position.z + _radius * Math.Sin(360f / splitIndex * (i + 1) * Mathf.Deg2Rad);

                var knot = new BezierKnot(new float3((float)x, transform.position.y, (float)z));
                newPoints.Add(knot);
                
            }
            
            newPoints.Closed = true;
            CreateNewSplineObject("newPoints", newPoints);
            SplitSplineAtIndex(GetAllKnots(newPoints), 1);
            _splineContainer.Spline.SetTangentMode(TangentMode.AutoSmooth);
        }
        
        
    }

    // Get all the knots from the spline
    private BezierKnot[] GetAllKnots(Spline spline)
    {
        var knotCount = spline.Count;
        var knots = new BezierKnot[knotCount + 1];

        for (var i = 0; i < knotCount; i++)
        {
            knots[i] = spline[i];
        }

        //Add the first knot at the end to make sure that it's a full circle
        knots[knotCount] = spline[0];

        return knots;
    }

    //Split the spline into two at the specified index
    private void SplitSplineAtIndex(BezierKnot[] knots, int index)
    {
        if (knots.Length == 1) return;
        // First part of the spline (from 0 to splitIndex)
        var firstSpline = new Spline();
        for (var i = 0; i <= index; i++)
        {
            firstSpline.Add(knots[i]);
        }

        var listKnots = knots.ToList();
        listKnots.RemoveAt(0);
        knots = listKnots.ToArray();

        // Optionally create new GameObjects to attach the split splines
        CreateNewSplineObject($"Spline{_totalSplines}", firstSpline);
        _totalSplines++;

        SplitSplineAtIndex(knots, 1);
    }


    // Create a new GameObject and add the split spline to it
    private void CreateNewSplineObject(string newSplineName, Spline spline)
    {
        var newSplineObject = new GameObject(newSplineName);
        newSplineObject.transform.parent = transform;
        var newSplineContainer = newSplineObject.AddComponent<SplineContainer>();
        newSplineContainer.Spline = spline;
    }
}