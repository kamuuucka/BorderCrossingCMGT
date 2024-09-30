using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TMP_Text))]
public class DisplaySliderValue : MonoBehaviour
{
    private TMP_Text _text;

    private void Start()
    {
        _text = GetComponent<TMP_Text>();
    }
    
    public void DisplayValue(Slider slider)
    {
        _text.text = slider.value.ToString();
    }
}
