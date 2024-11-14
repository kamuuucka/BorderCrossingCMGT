using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineGraphGenerator : MonoBehaviour
{
    [Header("Graph Specifications")]
    [Tooltip("Width of the layers in the graph.")]
    [Range(3,10)]
    [SerializeField] private float layerWidth;
    
    [Tooltip("Starting radius if the circle.")]
    [Range(2,5)]
    [SerializeField] private float radius;
    
    [Tooltip("Materials for each layer in the graph. First colour will be the one in the middle of the graph.")]
    [SerializeField] private List<Material> graphMaterials;

    [Space(10)] [Header("Necessary Objects")] 
    [Tooltip("Data with the prompts.")] 
    [SerializeField] private StringData prompts;

    private void Start()
    {
        if (prompts == null)
        {
            Debug.LogError("There's no prompts, graph won't generate.");
            return;
        }

        if (graphMaterials.Count == 0)
        {
            Debug.LogError("Graph has no layers to generate! ");
            return;
        }
        
        var layers = graphMaterials.Count;
        for (int i = 0; i < layers; i++)
        {
            var newObj = new GameObject($"Layer{i + 1}");
            newObj.transform.SetParent(transform);
            var splitSpline = newObj.AddComponent<SplitSplineGenerator>();
            splitSpline.GenerateSplitSpline(radius + (layerWidth + layerWidth) * i, prompts.data.Count,1.64f, layerWidth, graphMaterials[i]);
        }
    }
}
