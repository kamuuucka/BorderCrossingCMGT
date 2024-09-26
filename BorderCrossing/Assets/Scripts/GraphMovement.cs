using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class GraphMovement : MonoBehaviour
{
    public float radius = 5f; // Radius of the circle
    public float lineWidth = 0.2f; // Width of the line
    public Color[] layersColors;
    public int questions;
    public bool[] visibility;
    public List<LayerVisibility> layersVisibilities;
    public int layers;

    public LineSpecs segmentPrefab;
    private List<LineSpecs> _segments = new();

    private List<SegmentWithLayer> _segmentsWithLayers = new();


    void Start()
    {
        if (questions == 0) return;
        var angle = 360 / questions;
        
        for (int j = 0; j < layers; j++)
        {
            var layer = new GameObject($"Layer{j+1}");
            layer.transform.parent = gameObject.transform;
            _segmentsWithLayers.Add(new SegmentWithLayer());
            _segmentsWithLayers[j].segments = new List<LineSpecs>();
            
            for (int i = 0; i < questions; i++)
            {
                var child = Instantiate(segmentPrefab, layer.transform);
                child.SpawnLine(lineWidth, layersColors[j], radius + lineWidth * j, angle *(i+1), angle * i);
                //_segments.Add(child);
                _segmentsWithLayers[j].segments.Add(child);
            }
        }
        
        
    }

    public void Disappear()
    {
        for (int j = 0; j < layers; j++)
        {
            for (int i = 0; i < questions; i++)
            {
                Debug.Log(layersVisibilities[j].visibility[i]);
                if (layersVisibilities[j].visibility[i]) continue;
                Destroy(_segmentsWithLayers[j].segments[i].gameObject);
            }
        }
        
    }
}

[Serializable]
public class LayerVisibility
{
    public bool[] visibility;
}

[Serializable]
public class SegmentWithLayer
{
    public List<LineSpecs> segments;
}