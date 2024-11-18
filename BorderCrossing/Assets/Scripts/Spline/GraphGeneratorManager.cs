using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(SplineGraphGenerator))]
public class GraphGeneratorManager : MonoBehaviour
{
    #region Exposed Variables

    [Tooltip("ScriptableObject where the answers will be saved")]
    [SerializeField] private BoundaryData boundaryData;
    [Tooltip("The slider that will act as a 'cut out' point for the graph to be desaturated.")]
    [SerializeField] private Slider slider;
    [Tooltip("The speed with which graph rotates after the question is answered.")] 
    [SerializeField] private float rotationSpeed = 15f;
    [Tooltip("How much greyed out should be the unused segments.")]
    [SerializeField] private float greyOutValue = 0.5f;
    [Space(10)] [Header("Events")] 
    [Tooltip("Actions that will be performed when the graph starts rotating")]
    [SerializeField] private UnityEvent onRotationStarted;
    [Tooltip("Actions that will be performed when the graph stops rotating")]
    [SerializeField] private UnityEvent onRotationFinished;

    #endregion

    #region Private Variables

    private SplineGraphGenerator _graphGenerator;
    private bool _rotating;
    private float _targetAngle;
    private int _activeSegment;

    #endregion
    

    private void Awake()
    {
        enabled = false;
    }

    private void OnEnable()
    {
        _graphGenerator = GetComponent<SplineGraphGenerator>();
        slider.maxValue = _graphGenerator.layersNumber - 1;
        if (boundaryData == null) Debug.LogError("No boundary data assigned!");
        else boundaryData.data.Clear();
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
        GreyOutUnusedParts(_activeSegment, (int)slider.value + 1);
        _activeSegment++;
        _targetAngle = transform.eulerAngles.y + _graphGenerator.stepAngle;
        _rotating = true;
        if (boundaryData != null) boundaryData.data.Add((int)slider.value);
        onRotationStarted?.Invoke();
    }

    /// <summary>
    /// Animation of the rotation of the circle
    /// </summary>
    private void RotateSmoothly()
    {
        var currentAngle = transform.eulerAngles.y;
        var newAngle = Mathf.MoveTowardsAngle(currentAngle, _targetAngle, rotationSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, newAngle,0 );

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
        var segmentsWithLayers = _graphGenerator.chunksWithLayers;
        for (var j = startValue; j < segmentsWithLayers[segment].segments.Count; j++)
        {
            var line = segmentsWithLayers[segment].segments[j].GetComponent<MeshRenderer>();
            Debug.Log(line);
            line.material.color = ReduceSaturation(line.material.color);
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
