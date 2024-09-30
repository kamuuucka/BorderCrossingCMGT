using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TMP_Text))]
public class ChangePrompt : MonoBehaviour
{
    [SerializeField] private StringData prompts;
    
    private TMP_Text _promptText;
    private int _activePrompt;

    private void Start()
    {
        _promptText = GetComponent<TMP_Text>();
        _activePrompt = 0;
        
        if (prompts.data == null)
        {
            _promptText.text = $"No data available";
        }
        else
        {
            _promptText.text = prompts.data[0];
        }
    }
    
    public void GoToTheNextPrompt()
    {
        _activePrompt++;
        if (prompts.data[_activePrompt] == null || _activePrompt > prompts.data.Count-1)
        {
            _promptText.text = $"No data available";
        }
        else
        {
            _promptText.text = prompts.data[_activePrompt];
        }
    }

    public void PromptBasedOnSlider(Slider slider)
    {
        Debug.Log($"Slider value: {slider.value} and prompts {prompts.data.Count}");
        if (slider.value > prompts.data.Count-1 || prompts.data[(int)slider.value] == null)
        {
            _promptText.text = $"No data available";
        }
        else
        {
            _promptText.text = prompts.data[(int)slider.value];
        }
    }
}
