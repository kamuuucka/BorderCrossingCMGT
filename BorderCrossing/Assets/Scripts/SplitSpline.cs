using UnityEngine;
using UnityEngine.Splines;

public class SplitSpline : MonoBehaviour
{
    [SerializeField] private int splitIndex = 5; // Index at which to split the spline
    private SplineContainer splineContainer;

    private void Start()
    {
        // Retrieve the existing SplineContainer
        splineContainer = GetComponent<SplineContainer>();

        if (splineContainer != null)
        {
            // Get all knots
            var knots = GetAllKnots();

            // Split the spline into two
            SplitSplineAtIndex(knots, splitIndex);
        }
    }

    // Get all the knots from the spline
    private BezierKnot[] GetAllKnots()
    {
        int knotCount = splineContainer.Spline.Count;
        BezierKnot[] knots = new BezierKnot[knotCount];

        for (int i = 0; i < knotCount; i++)
        {
            knots[i] = splineContainer.Spline[i];
        }

        return knots;
    }

    // Split the spline into two at the specified index
    private void SplitSplineAtIndex(BezierKnot[] knots, int index)
    {
        // First part of the spline (from 0 to splitIndex)
        Spline firstSpline = new Spline();
        for (int i = 0; i <= index; i++)
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
    private void CreateNewSplineObject(string name, Spline spline)
    {
        GameObject newSplineObject = new GameObject(name);
        var newSplineContainer = newSplineObject.AddComponent<SplineContainer>();
        newSplineContainer.Spline = spline;
    }
}
