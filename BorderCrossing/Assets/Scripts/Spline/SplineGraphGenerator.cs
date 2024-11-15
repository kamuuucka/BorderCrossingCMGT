using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines;

public class SplineGraphGenerator : MonoBehaviour
{
    #region Exposed Variables
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
    #endregion

    #region Private Variables

    private readonly float _smoothing = 1.64f;
    private List<ChunkWithLayers> _chunksWithLayers = new();

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
        
        var layers = graphMaterials.Count;
        // for (int i = 0; i < layers; i++)
        // {
        //     var newObj = new GameObject($"Layer{i + 1}");
        //     newObj.transform.SetParent(transform);
        //     var splineGenerator = newObj.AddComponent<SplitSplineGenerator>();
        //     var circularSpline = splineGenerator.CreateCircularSpline(radius + (layerWidth + layerWidth) * i);
        //     var splitSplines = splineGenerator.SplitSpline(circularSpline, prompts.data.Count, _smoothing);
        //
        //     foreach (var spline in splitSplines)
        //     {
        //         splineGenerator.ExtrudeSpline(spline, graphMaterials[i], layerWidth);
        //     }
        // }

        for (int i = 0; i < prompts.data.Count; i++)
        {
            var chunkParent = new GameObject($"Chunk{i + 1}")
            {
                transform = { parent = transform}
            };

            var chunkWithLayers = new ChunkWithLayers
            {
                chunk = chunkParent,
                segments = new List<SplineContainer>()
            };
            _chunksWithLayers.Add(chunkWithLayers);
        }

        //TODO: Fix this because it is not dividing to chunks correctly:(
        for (int i = 0; i < layers; i++)
        {
            var newObj = new GameObject();
            var splineGenerator = newObj.AddComponent<SplitSplineGenerator>();
            var circularSpline = splineGenerator.CreateCircularSpline(radius + (layerWidth + layerWidth) * i);
            
            for (int j = 0; j < prompts.data.Count; j++)
            {
                newObj.transform.SetParent(_chunksWithLayers[j].chunk.transform);
                
                var splitSplines = splineGenerator.SplitSpline(circularSpline, prompts.data.Count, _smoothing);

                foreach (var spline in splitSplines)
                {
                    splineGenerator.ExtrudeSpline(spline, graphMaterials[i], layerWidth);
                }
                
                _chunksWithLayers[j].segments = splitSplines;
            }
        }
    }
    
}

[Serializable]
public class ChunkWithLayers
{
    public GameObject chunk;
    public List<SplineContainer> segments;
}
