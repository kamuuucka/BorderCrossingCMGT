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
            _colorPreset.SaveGraphColorPreset();
            EditorUtility.SetDirty(_colorPreset);
            AssetDatabase.SaveAssets();
        }
    }
}