using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphGenerator : MonoBehaviour
{
    [Header("Graph specifications")] 
    [Tooltip("Width of the layer in graph.")] 
    [SerializeField] private float lineWidth = 1.6f;

    [Tooltip("Colors of each layer in the graph. Starting from middle. This also defines how many layers are in the graph.")]
    [SerializeField] private List<Color> layersColors;
    
    
    [Space(10)] [Header("Necessary objects")] 
    [Tooltip("Slider to vote on the graph.")] 
    [SerializeField] private Slider slider;

    public Slider Slider => slider;

    [Tooltip("Data with the prompts")] 
    [SerializeField] private StringData prompts;

    [Tooltip("LineSpecs prefab. Necessary to create separate fragments of graph.")] 
    [SerializeField] private LineGenerator segmentPrefab;

    public List<SegmentWithLayer> SegmentWithLayers { get; } = new();

    public float StepAngle { get; private set; }

    private float _targetAngle;
    private int _promptsCount;
    private bool _rotating;

    private void Start()
    {
        slider.maxValue = layersColors.Count - 1;
        
        SetUpPromptVariables();
        CreateLayers();
        SpawnLineSegments();

        //Rotate the graph to match the horizontal slider
        transform.rotation = Quaternion.Euler(0, 0, StepAngle / 2f - 90);

        var graphManager = GetComponent<GraphManager>();
        graphManager.enabled = true;
    }

    /// <summary>
    /// Set up all the variables connected to prompts
    /// </summary>
    private void SetUpPromptVariables()
    {
        if (prompts.data.Count == 0) return;
        _promptsCount = prompts.data.Count;
        StepAngle = 360f / _promptsCount;
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
            SegmentWithLayers.Add(new SegmentWithLayer());
            SegmentWithLayers[i].segments = new List<LineGenerator>();
            SegmentWithLayers[i].layerParent = layer;
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
                var child = Instantiate(segmentPrefab, SegmentWithLayers[i].layerParent.transform);
                child.SpawnLine(lineWidth, layersColors[j], lineWidth + lineWidth * j, StepAngle * i,
                    StepAngle * (i + 1), transform.position);
                SegmentWithLayers[i].segments.Add(child);
            }
        }
    }

    /// <summary>
    /// Get the colors from layers. Used to save colors in the preset
    /// </summary>
    /// <returns>List of colors in the layers</returns>
    public List<Color> GetColorsOfLayers()
    {
        return layersColors;
    }

    public void AssignStringData()
    {
       // prompts = data;
        Debug.Log("New data loaded in!");
    }
}

[Serializable]
public class SegmentWithLayer
{
    public GameObject layerParent;
    public List<LineGenerator> segments;
}