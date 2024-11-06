using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShadowEffect : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private float width = 100f;
    [SerializeField] private float height = 100f;
    private Image _shadow;

    private void Start()
    {
        var shadowObject = new GameObject();
        var shadowTransform = shadowObject.AddComponent<RectTransform>();
        var canvasForShadow = FindObjectOfType<CanvasShadowReference>().shadowReference.transform;
        shadowObject.transform.SetParent(canvasForShadow);
        shadowTransform.transform.position = transform.position + offset;
        var shadowTransformRect = shadowTransform.rect;
        shadowTransformRect.width = width;
        shadowTransformRect.height = height;
        _shadow = shadowObject.AddComponent<Image>();
        _shadow.sprite = GetComponent<Image>().sprite;
    }
}
