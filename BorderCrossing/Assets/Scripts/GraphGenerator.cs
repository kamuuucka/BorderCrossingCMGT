using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GraphGenerator : MonoBehaviour
{
    [Header("Graph specifications")]
    [Tooltip("Radius of the middle point of the graph.")]
    [SerializeField] private float startRadius = 0.2f;
    [Tooltip("Width of the layer in graph.")]
    [SerializeField] private float lineWidth = 1.6f;
    [Tooltip("Colors of each layer in the graph. Starting from middle. This also defines how many layers are in the graph.")]
    [SerializeField] private Color[] layersColors;
    [Space(20)]
    [Header("Necessary objects")]
    [Tooltip("Slider to vote on the graph. Must have as many points as there are layers in the graph.")]
    [SerializeField] private Slider slider;
    [Tooltip("Data with the prompts")]
    [SerializeField] private StringData prompts;
    [Tooltip("LineSpecs prefab. Necessary to create separate fragments of graph.")]
    [SerializeField] private LineSpecs segmentPrefab;
    
    private readonly List<SegmentWithLayer> _segmentsWithLayers = new();
    private float _stepAngle;
    private int _activeQuestion;
    private int _promptsCount;


    private void Start()
    {
        if (prompts.data.Count == 0) return;
        _promptsCount = prompts.data.Count;
        _stepAngle = 360f / _promptsCount;
        slider.maxValue = layersColors.Length - 1;
        
        CreateLayers();
        SpawnLineSegments();
        
        //Rotate the graph to match the horizontal slider
        transform.rotation = Quaternion.Euler(0,0,_stepAngle/2f - 90);
    }

    private void CreateLayers()
    {
        for (var i = 0; i < _promptsCount; i++)
        {
            var layer = new GameObject($"Layer{i+1}")
            {
                transform =
                {
                    parent = gameObject.transform
                }
            };
            _segmentsWithLayers.Add(new SegmentWithLayer());
            _segmentsWithLayers[i].segments = new List<LineSpecs>();
            _segmentsWithLayers[i].layerParent = layer;
        }
    }

    private void SpawnLineSegments()
    {
        for (var j = 0; j < layersColors.Length; j++)
        {
            for (var i = 0; i < _promptsCount; i++)
            {
                var child = Instantiate(segmentPrefab, _segmentsWithLayers[i].layerParent.transform);
                child.SpawnLine(lineWidth, layersColors[j], startRadius + lineWidth * j, _stepAngle *(i+1), _stepAngle * i, transform);
                _segmentsWithLayers[i].segments.Add(child);
            }
        }
    }

    public void Rotate()
    {
        var currentAngle = transform.eulerAngles.z;
        currentAngle += _stepAngle;
        transform.rotation = Quaternion.Euler(0,0,currentAngle);
        RemoveUnusedSegments(_activeQuestion, (int)slider.value);
        _activeQuestion++;
    }

    private void RemoveUnusedSegments(int chunk, int number)
    {
        List<LineSpecs> segmentsToRemove = new();
        for (var j = 0; j < _segmentsWithLayers[chunk].segments.Count; j++)
        {
            if (j <= number) continue;
            segmentsToRemove.Add(_segmentsWithLayers[chunk].segments[j]);
            Destroy(_segmentsWithLayers[chunk].segments[j].gameObject);
        }

        foreach (var segmentToRemove in segmentsToRemove)
        {
            _segmentsWithLayers[chunk].segments.Remove(segmentToRemove);
        }
        
        segmentsToRemove.Clear();
    }
}

[Serializable]
public class SegmentWithLayer
{
    public GameObject layerParent;
    public List<LineSpecs> segments;
}