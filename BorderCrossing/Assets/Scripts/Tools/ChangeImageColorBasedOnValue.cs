using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeImageColorBasedOnValue : MonoBehaviour
{
    [SerializeField] private ColorPreset colors;

    private Image _image;

    private void Start()
    {
        _image = GetComponent<Image>();
    }

    public void ChangeColor(Slider slider)
    {
        _image.color = colors.LoadColorPreset()[(int)slider.value];
    }
}
