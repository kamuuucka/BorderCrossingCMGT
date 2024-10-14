using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(GraphGenerator))]
public class GraphManager : MonoBehaviour
{
    [SerializeField] private BoundaryData boundaryData;
    [Tooltip("The speed with which graph rotates after the question is answered.")] 
    [SerializeField] private float rotationSpeed = 15f;
    [Tooltip("How much greyed out should be the unused segments.")]
    [SerializeField] private float greyOutValue = 0.5f;
    [Space(10)] [Header("Events")] 
    [Tooltip("Actions that will be performed when the graph starts rotating")]
    [SerializeField] private UnityEvent onRotationStarted;
    [Tooltip("Actions that will be performed when the graph stops rotating")]
    [SerializeField] private UnityEvent onRotationFinished;
    
    private Slider _slider;
    private GraphGenerator _graphGenerator;
    private int _activeQuestion;
    private bool _rotating;
    private float _stepAngle;
    private float _targetAngle;

    private void Awake()
    {
        enabled = false;
    }

    private void OnEnable()
    {
        _graphGenerator = GetComponent<GraphGenerator>();
        _slider = _graphGenerator.Slider;
        _stepAngle = _graphGenerator.StepAngle;
        boundaryData.data.Clear();
    }

    private void Update()
    {
        if (_rotating)
        {
            RotateSmoothly();
        }
    }
    
    /// <summary>
    /// Perform the rotation of the graph
    /// </summary>
    public void RotateSegment()
    {
        GreyOutUnusedParts(_activeQuestion, (int)_slider.value + 1);
        _activeQuestion++;
        boundaryData.data.Add((int)_slider.value);
        _targetAngle = transform.eulerAngles.z + _stepAngle;
        _rotating = true;
        
        onRotationStarted?.Invoke();
    }

    /// <summary>
    /// Animation of the rotation of the circle
    /// </summary>
    private void RotateSmoothly()
    {
        var currentAngle = transform.eulerAngles.z;
        var newAngle = Mathf.MoveTowardsAngle(currentAngle, _targetAngle, rotationSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, 0, newAngle);

        if (!Mathf.Approximately(newAngle, _targetAngle)) return;
        
        _rotating = false;
        onRotationFinished?.Invoke();
    }

    /// <summary>
    /// Greys out the unused parts of the segment after the vote has been made
    /// </summary>
    /// <param name="segment">Segment on which the operation is performed.</param>
    /// <param name="startValue">Value from which the operation should be performed.</param>
    private void GreyOutUnusedParts(int segment, int startValue)
    {
        var segmentsWithLayers = _graphGenerator.SegmentWithLayers;
        for (var j = startValue; j < segmentsWithLayers[segment].segments.Count; j++)
        {
            var line = segmentsWithLayers[segment].segments[j].GetComponent<LineRenderer>();
            line.startColor = ReduceSaturation(line.startColor);
            line.endColor = ReduceSaturation(line.endColor);
        }
    }

    /// <summary>
    /// Reduce the saturation of the color
    /// </summary>
    /// <param name="color">Color that should be reduced</param>
    /// <returns></returns>
    private Color ReduceSaturation(Color color)
    {
        // Convert the color from RGB to HSV
        Color.RGBToHSV(color, out float hue, out float saturation, out float value);

        // Reduce the saturation by the given factor
        saturation *= greyOutValue;

        // Convert it back to RGB
        return Color.HSVToRGB(hue, saturation, value);
    }
}
