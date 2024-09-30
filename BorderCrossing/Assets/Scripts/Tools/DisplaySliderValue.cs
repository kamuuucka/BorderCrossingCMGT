using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class DisplaySliderValue : MonoBehaviour
{
    private Slider _slider;

    private void Start()
    {
        _slider = GetComponent<Slider>();
    }
    
    public void DisplayValue(TMP_Text text)
    {
        text.text = _slider.value.ToString();
    }
}
