using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineGraphGenerator : MonoBehaviour
{
    [SerializeField] private List<Material> graphMaterials;
    [SerializeField] private float layerWidth;
    [SerializeField] private int layers = 5;

    private void Start()
    {
        var radius = layerWidth + 2;
        for (int i = 0; i < layers; i++)
        {
            var newObj = new GameObject($"Layer{i + 1}");
            newObj.transform.SetParent(transform);
            var splitSpline = newObj.AddComponent<SplitSplineGenerator>();
            splitSpline.GenerateSplitSpline(radius + radius * i,10,1.64f, layerWidth);
        }
    }
}
