using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class SplitSplineGenerator : MonoBehaviour
{
    private float _radius;
    private int _splitCount;
    private float _smoothing;
    private float _lineWidth;
    private Material _material;

    private const int NumberOfKnots = 10;
    private SplineContainer _splineContainer;
    
    public void GenerateSplitSpline(float radius, int splitCount, float smoothing, float lineWidth, Material material)
    {
        _radius = radius;
        _splitCount = splitCount;
        _smoothing = smoothing;
        _lineWidth = lineWidth;
        _material = material;
        _splineContainer = GetComponent<SplineContainer>();
        if (_splineContainer == null)
        {
            _splineContainer = gameObject.AddComponent<SplineContainer>();
        }
        
        CreateCircularSpline();
        
        List<SplineContainer> splitSplines = SplitSpline(_splineContainer);

        foreach (var spline in splitSplines)
        {
            ExtrudeSpline(spline);
        }
    }

    private void ExtrudeSpline(SplineContainer spline)
    {
        var splineExtrude = spline.GetComponent<SplineExtrude>();
        splineExtrude.Container = spline;
        var mesh = spline.GetComponent<MeshFilter>();
        mesh.mesh = new Mesh();
        var meshRenderer = spline.GetComponent<MeshRenderer>();
        meshRenderer.material = _material;
        splineExtrude.Radius = _lineWidth;
    }

    /// <summary>
    /// Create a spline that is a circle. Needs to have at least 10 knots.
    /// </summary>
    private void CreateCircularSpline()
    {
        _splineContainer.Spline.Clear();

        var segmentAngle = 360f / NumberOfKnots;

        //Create knots around the circle
        for (var i = 0; i < NumberOfKnots; i++)
        {
            var angleInRadians = Mathf.Deg2Rad * i * segmentAngle;

            var x = Mathf.Cos(angleInRadians) * _radius;
            var z = Mathf.Sin(angleInRadians) * _radius;

            var knot = new BezierKnot(new Vector3(x, 0, z));
            _splineContainer.Spline.Add(knot);
        }
        
        //Close the spline to make sure it's a full circle
        _splineContainer.Spline.Closed = true;
        _splineContainer.Spline.SetTangentMode(TangentMode.AutoSmooth);
    }

    /// <summary>
    /// Split spline into segments that will create the same circle as the original spline.
    /// Needed to create our graph that can be sliced depending on the user's answers.
    /// </summary>
    /// <param name="originalContainer">Original circular spline</param>
    /// <returns>List of SplineContainers</returns>
    private List<SplineContainer> SplitSpline(SplineContainer originalContainer)
    {
        var splitContainers = new List<SplineContainer>();
        var originalSpline = originalContainer.Spline;

        var splitLength = 1f / _splitCount;
        for (var i = 0; i < _splitCount; i++)
        {
            var newObj = new GameObject($"SplineSegment_{i + 1}");
            newObj.transform.SetParent(transform);
            var newObjScale= newObj.transform.localScale;
            newObjScale.y = 0.01f;
            newObj.transform.localScale = newObjScale;
            var newContainer = newObj.AddComponent<SplineContainer>();
            var newSpline = newContainer.Spline;
            newObj.AddComponent<SplineExtrude>();

            AddSplineSegment(originalSpline, newSpline, i * splitLength, (i + 1) * splitLength, newContainer.transform);
            
            splitContainers.Add(newContainer);
        }

        return splitContainers;
    }


    /// <summary>
    /// Creates new spline segment extracted from the original spline based on a specific range, ensuring that the shape resembles the original one.
    /// </summary>
    /// <param name="originalSpline">Original Spline which acts as a base for the segments.</param>
    /// <param name="newSpline">New Spline where the segment will be created.</param>
    /// <param name="startT">Start of the segment (based on the knots on the original Spline).</param>
    /// <param name="endT">End of the segment (based on the knots on the original Spline).</param>
    /// <param name="newSplineTransform">Transform of the new Spline.</param>
    private void AddSplineSegment(Spline originalSpline, Spline newSpline, float startT, float endT, Transform newSplineTransform)
    {
        //Calculating how many sample points should be in one segment. The number of knots is divided by 3 to reduce the density of knots while remaining the original shape.
        //To make segment smoother, change 3 to smaller number.
        var samplePoints = Mathf.CeilToInt(NumberOfKnots / _smoothing);

        for (var i = 0; i < samplePoints; i++)
        {
            //Calculate the normalised point of the segment. Point ranges between start and end of the segment. Depends on the samplePoints.
            var t = Mathf.Lerp(startT, endT, i / (float)(samplePoints - 1));
            
            //Find positions along the original Spline.
            Vector3 worldPosition = originalSpline.EvaluatePosition(t);
            //Find tangents of those positions on the original Spline.
            Vector3 tangent = originalSpline.EvaluateTangent(t);
            
            Vector3 localPosition = newSplineTransform.InverseTransformPoint(worldPosition);
            Quaternion rotation = tangent != Vector3.zero ? Quaternion.LookRotation(tangent) : Quaternion.identity;
            
            BezierKnot knot = new BezierKnot(localPosition, tangent, -tangent, rotation);
            newSpline.Add(knot);
        }

        newSpline.SetTangentMode(TangentMode.AutoSmooth);
    }

}
