using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeTextColorBasedOnValue : MonoBehaviour
{
    [SerializeField] private ColorPreset colors;

    private TMP_Text _text;

    private void Start()
    {
        _text = GetComponent<TMP_Text>();
    }

    public void ChangeColor(Slider slider)
    {
        _text.color = colors.LoadColorPreset()[(int)slider.value];
    }
}
