using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GraphGenerator))]
public class GraphGeneratorEditor : Editor
{
    private GraphGenerator _generator;
    private void OnEnable()
    {
        _generator = (GraphGenerator)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

       OnlyOneBooleanActive();
       
       if (GUI.changed)
       {
           EditorUtility.SetDirty(_generator);
       }
    }

    private void OnlyOneBooleanActive()
    {
        EditorGUI.BeginChangeCheck();
        
        _generator.greyOut = EditorGUILayout.Toggle("Grey Out", _generator.greyOut);
        if (_generator.greyOut)
        {
            _generator.cutOut = false;
        }

        _generator.cutOut = EditorGUILayout.Toggle("Cut Out", _generator.cutOut);
        if (_generator.cutOut)
        {
            _generator.greyOut = false;
        }

        
        if (EditorGUI.EndChangeCheck())
        {
            if (_generator.cutOut && _generator.greyOut)
            {
                _generator.cutOut = false;
            }
            else if (!_generator.cutOut && !_generator.greyOut)
            {
                _generator.greyOut = true;
                _generator.cutOut = false;
            }
        }
    }
}
