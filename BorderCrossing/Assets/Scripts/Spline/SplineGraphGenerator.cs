using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines;

public class SplineGraphGenerator : MonoBehaviour
{
    #region Exposed Variables

    [Header("Graph Specifications")] [Tooltip("Width of the layers in the graph.")] [Range(3, 10)] [SerializeField]
    private float layerWidth;

    [Tooltip("Starting radius if the circle.")] [Range(2, 5)] [SerializeField]
    private float radius;

    [Tooltip("Materials for each layer in the graph. First colour will be the one in the middle of the graph.")]
    [SerializeField]
    private List<Material> graphMaterials;

    [Space(10)] [Header("Necessary Objects")] [Tooltip("Data with the prompts.")] [SerializeField]
    private StringData prompts;

    #endregion

    #region Private Variables

    private readonly float _smoothing = 1.64f;
    private List<SegmentWithLayers> _chunksWithLayers = new();

    #endregion


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
        
        CreateSegments(prompts.data.Count);
        SpawnGraphOnChunks(graphMaterials.Count, prompts.data.Count);
    }

    /// <summary>
    /// Create a graph divided into layers and segments
    /// </summary>
    /// <param name="numberOfLayers">Number of layers the graph should have</param>
    /// <param name="numberOfChunks">Number of segments the graph should be divided into</param>
    private void SpawnGraphOnChunks(int numberOfLayers, int numberOfChunks)
    {
        for (var i = 0; i < numberOfLayers; i++)
        {
            //Create temp object just to create SplitSplineGenerator
            var tempObject = new GameObject();
            var splineGenerator = tempObject.AddComponent<SplitSplineGenerator>();
            var circularSpline = splineGenerator.CreateCircularSpline(radius + (layerWidth + layerWidth) * i);
            var splitSplines = splineGenerator.SplitSpline(circularSpline, prompts.data.Count, _smoothing);
            
            for (int j = 0; j < numberOfChunks; j++)
            {
                _chunksWithLayers[j].segments.Add(splitSplines[j]);
                _chunksWithLayers[j].segments[i].transform.SetParent(_chunksWithLayers[j].chunk.transform);
            }
            
            foreach (var spline in splitSplines)
            {
                splineGenerator.ExtrudeSpline(spline, graphMaterials[i], layerWidth);
            }
            
            Destroy(tempObject);
        }
    }

    /// <summary>
    /// Create segments with lists of layers that can be added to them
    /// </summary>
    /// <param name="numberOfChunks">Number of segments the graph should be divided into</param>
    private void CreateSegments(int numberOfChunks)
    {
        for (int i = 0; i < numberOfChunks; i++)
        {
            var chunkParent = new GameObject($"Chunk{i + 1}")
            {
                transform = { parent = transform }
            };

            var chunkWithLayers = new SegmentWithLayers
            {
                chunk = chunkParent,
                segments = new List<SplineContainer>()
            };
            _chunksWithLayers.Add(chunkWithLayers);
        }
    }
}

[Serializable]
public class SegmentWithLayers
{
    public GameObject chunk;
    public List<SplineContainer> segments;
}