using System.Linq;
using UnityEngine;
using UnityEngine.Splines;

public class SplitSpline : MonoBehaviour
{
    [SerializeField] private int splitIndex = 5; // Index at which to split the spline
    private SplineContainer _splineContainer;

    private void OnEnable()
    {
        // Retrieve the existing SplineContainer
        _splineContainer = GetComponent<SplineContainer>();

        if (_splineContainer != null)
        {
            Debug.Log("I am splitting the spline!");
            // Get all knots
            var knots = GetAllKnots();

            // Split the spline into two
            SplitSplineAtIndex(knots, splitIndex);
        }
    }

    // Get all the knots from the spline
    private BezierKnot[] GetAllKnots()
    {
        var knotCount = _splineContainer.Spline.Count;
        var knots = new BezierKnot[knotCount+1];

        for (var i = 0; i < knotCount; i++)
        {
            knots[i] = _splineContainer.Spline[i];
        }

        //Add the first knot at the end to make sure that it's a full circle
        knots[knotCount] = _splineContainer.Spline[0];

        return knots;
    }

    //Split the spline into two at the specified index
    private void SplitSplineAtIndex(BezierKnot[] knots, int index)
    {
        // First part of the spline (from 0 to splitIndex)
        var firstSpline = new Spline();
        for (var i = 0; i <= index; i++)
        {
            firstSpline.Add(knots[i]);
        }
    
        // Second part of the spline (from splitIndex to end)
        Spline secondSpline = new Spline();
        for (int i = index; i < knots.Length; i++)
        {
            secondSpline.Add(knots[i]);
        }
    
        // Optionally create new GameObjects to attach the split splines
        CreateNewSplineObject("First Spline", firstSpline);
        CreateNewSplineObject("Second Spline", secondSpline);
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
