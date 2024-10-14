using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ManageColorPreset))]
public class ManageColorPresetEditor : Editor
{
    private ManageColorPreset _colorPreset;

    private void OnEnable()
    {
        _colorPreset = (ManageColorPreset)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Save The Preset"))
        {
            _colorPreset.SaveColorPreset();
            EditorUtility.SetDirty(_colorPreset);
            AssetDatabase.SaveAssets();
        }
        
        if (GUILayout.Button("Load The Preset"))
        {
            _colorPreset.LoadPreset();
            EditorUtility.SetDirty(_colorPreset);
            AssetDatabase.SaveAssets();
        }
    }
}