using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GraphGenerator : MonoBehaviour
{
    [Header("Graph specifications")] 
    [Tooltip("Width of the layer in graph.")] 
    [SerializeField] private float lineWidth = 1.6f;

    [Tooltip("Colors of each layer in the graph. Starting from middle. This also defines how many layers are in the graph.")]
    [SerializeField] private List<Color> layersColors;

    [Tooltip("The speed with which graph rotates after the question is answered.")] 
    [SerializeField] private float rotationSpeed = 15f;

    [Tooltip("How much greyed out should be the unused segments.")]
    [SerializeField] private float greyOutValue = 0.5f;
    
    [Space(20)] [Header("Necessary objects")] 
    [Tooltip("Slider to vote on the graph.")] 
    [SerializeField] private Slider slider;

    [Tooltip("Data with the prompts")] 
    [SerializeField] private StringData prompts;

    [Tooltip("Data to save the positions of answers to")]
    [SerializeField] private BoundaryData boundaryData;

    [Tooltip("LineSpecs prefab. Necessary to create separate fragments of graph.")] 
    [SerializeField] private LineGenerator segmentPrefab;

    [Tooltip("Actions that will be performed when the graph starts rotating")]
    [SerializeField] private UnityEvent onRotationStarted;
    [Tooltip("Actions that will be performed when the graph stops rotating")]
    [SerializeField] private UnityEvent onRotationFinished;
    
    private readonly List<SegmentWithLayer> _segmentsWithLayers = new();
    private float _stepAngle;
    private float _targetAngle;
    private int _activeQuestion;
    private int _promptsCount;
    private bool _rotating;

    private void Update()
    {
        if (_rotating)
        {
            RotateSmoothly();
        }
    }

    private void Start()
    {
        boundaryData.data.Clear();
        slider.maxValue = layersColors.Count - 1;
        
        SetUpPromptVariables();
        CreateLayers();
        SpawnLineSegments();

        //Rotate the graph to match the horizontal slider
        transform.rotation = Quaternion.Euler(0, 0, _stepAngle / 2f - 90);
    }

    /// <summary>
    /// Set up all the variables connected to prompts
    /// </summary>
    private void SetUpPromptVariables()
    {
        if (prompts.data.Count == 0) return;
        _promptsCount = prompts.data.Count;
        _stepAngle = 360f / _promptsCount;
    }

    /// <summary>
    /// Creates the layers
    /// </summary>
    private void CreateLayers()
    {
        for (var i = 0; i < _promptsCount; i++)
        {
            var layer = new GameObject($"Layer{i + 1}")
            {
                transform =
                {
                    parent = gameObject.transform
                }
            };
            _segmentsWithLayers.Add(new SegmentWithLayer());
            _segmentsWithLayers[i].segments = new List<LineGenerator>();
            _segmentsWithLayers[i].layerParent = layer;
        }
    }

    /// <summary>
    /// Creates the segments made out of lines
    /// </summary>
    private void SpawnLineSegments()
    {
        for (var j = 0; j < layersColors.Count; j++)
        {
            for (var i = 0; i < _promptsCount; i++)
            {
                var child = Instantiate(segmentPrefab, _segmentsWithLayers[i].layerParent.transform);
                child.SpawnLine(lineWidth, layersColors[j], lineWidth + lineWidth * j, _stepAngle * i,
                    _stepAngle * (i + 1), transform.position);
                _segmentsWithLayers[i].segments.Add(child);
            }
        }
    }

    /// <summary>
    /// Perform the rotation of the graph
    /// </summary>
    public void RotateSegment()
    {
        _activeQuestion++;
        boundaryData.data.Add((int)slider.value);
        
        GreyOutUnusedParts(_activeQuestion, (int)slider.value + 1);
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
        for (var j = startValue; j < _segmentsWithLayers[segment].segments.Count; j++)
        {
            var line = _segmentsWithLayers[segment].segments[j].GetComponent<LineRenderer>();
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

    /// <summary>
    /// Get the colors from layers. Used to save colors in the preset
    /// </summary>
    /// <returns>List of colors in the layers</returns>
    public List<Color> GetColorsOfLayers()
    {
        return layersColors;
    }
}

[Serializable]
public class SegmentWithLayer
{
    public GameObject layerParent;
    public List<LineGenerator> segments;
}