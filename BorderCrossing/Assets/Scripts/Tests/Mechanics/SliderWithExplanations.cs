using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SliderWithExplanations : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TMP_Text numberOnHandle;
    [TextArea]
    [SerializeField] private List<string> explanations;
    [SerializeField] private TMP_Text explanationsText;
    [SerializeField] private BoundaryData boundaryData;

    private void Update()
    {
        explanationsText.text = $"{(int)slider.value + 1}: {explanations[(int)slider.value]}";
        numberOnHandle.text = $"{(int)slider.value + 1}";
    }

    public void SaveNumber()
    {
        boundaryData.data.Add((int)slider.value);
    }

    
}
