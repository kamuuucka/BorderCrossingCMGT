using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CanvasShadowReference : MonoBehaviour
{
    [HideInInspector] public RectTransform shadowReference;
    private void Awake()
    {
        shadowReference = GetComponent<RectTransform>();
    }
}
