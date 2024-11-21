using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(TMP_Text))]
public class ChangePrompt : MonoBehaviour
{
    [SerializeField] private StringData prompts;
    [SerializeField] private UnityEvent onPromptChanged;
    
    private TMP_Text _promptText;
    private int _activePrompt;

    private void OnEnable()
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
        onPromptChanged?.Invoke();
        if (_activePrompt > prompts.data.Count-1 || prompts.data[_activePrompt] == null )
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
