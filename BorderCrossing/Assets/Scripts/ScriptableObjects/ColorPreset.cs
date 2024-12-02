using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ColorPreset", menuName = "Preset/ColorPreset")]
public class ColorPreset : ScriptableObject
{
    [SerializeField] private List<Color> _colors = new();

    public void SaveColorPreset(List<Color> newColors)
    {
        _colors.Clear();
        if(newColors == null && newColors.Count == 0) return;
        _colors = newColors;
    }

    public List<Color> LoadColorPreset()
    {
        if (_colors != null && _colors.Count != 0)
        {
            return _colors;
        }

        return null;
    }
}
