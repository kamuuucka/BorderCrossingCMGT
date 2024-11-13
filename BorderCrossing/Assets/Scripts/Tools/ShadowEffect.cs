using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SpriteRenderer))]
public class ShadowEffect : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private Material material;
    private GameObject _shadow;

    private void Start()
    {
        if (material == null)
        {
            material = new Material(Shader.Find("Unlit/Color"))
            {
                color = Color.black
            };
        }
        
        _shadow = new GameObject();
        _shadow.transform.SetParent(transform);
        _shadow.transform.localPosition = offset;
        _shadow.transform.localRotation = Quaternion.identity;

        var objectRenderer = GetComponent<SpriteRenderer>();
        var shadowRenderer = _shadow.AddComponent<SpriteRenderer>();
        shadowRenderer.sprite = objectRenderer.sprite;
        shadowRenderer.material = material;

        shadowRenderer.sortingLayerName = objectRenderer.sortingLayerName;
        shadowRenderer.sortingOrder = objectRenderer.sortingOrder - 1;

    }

    private void LateUpdate()
    {
        _shadow.transform.localPosition = offset;
    }
}
