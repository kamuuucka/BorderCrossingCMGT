using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ManageColorPreset : MonoBehaviour
{
    [SerializeField] private ColorPreset colorPreset;
    [SerializeField] private List<Color> colors;

    public void SaveColorPreset()
    {
        colorPreset.SaveColorPreset(colors);
    }

    public void LoadPreset()
    {
        colors = colorPreset.LoadColorPreset();
    }
}
