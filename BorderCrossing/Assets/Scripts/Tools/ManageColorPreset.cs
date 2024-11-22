using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ManageColorPreset : MonoBehaviour
{
    [SerializeField] private ColorPreset colorPreset;
    [SerializeField] private GraphGenerator graph;

    public void SaveGraphColorPreset()
    {
        colorPreset.SaveColorPreset(graph.GetColorsOfLayers());
    }
    
}
