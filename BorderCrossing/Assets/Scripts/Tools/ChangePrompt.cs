using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(TMP_Text))]
public class ChangePrompt : MonoBehaviour
{
    [SerializeField] private StringData prompts;
    [SerializeField] private UnityEvent onPromptChanged;
    [SerializeField] private UnityEvent onPromptsFinished;
    
    private TMP_Text _promptText;
    private int _activePrompt;

    private void OnEnable()
    {
        _promptText = GetComponent<TMP_Text>();
        //_activePrompt = 0;
        
        if (prompts.data == null)
        {
            _promptText.text = $"No data available";
        }
        else
        {
            _promptText.text = prompts.data[_activePrompt];
        }
        
    }

    public void GoToTheNextPrompt()
    {
        Debug.Log("Go to the next prompt!");
        _activePrompt++;
        onPromptChanged?.Invoke();
        if (_activePrompt > prompts.data.Count-1 || prompts.data[_activePrompt] == null )
        {
            onPromptsFinished?.Invoke();
            _promptText.text = prompts.data[^1];
            _activePrompt = prompts.data.Count-1;
        }
        else
        {
            _promptText.text = prompts.data[_activePrompt];
        }
    }

    public void GoToThePreviousPrompt()
    {
        _activePrompt--;
        onPromptChanged?.Invoke();
        if (_activePrompt <= 0 || prompts.data[_activePrompt] == null )
        {
            onPromptsFinished?.Invoke();
            _promptText.text = prompts.data[0];
            _activePrompt = 0;
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

    public void AssignStringData(StringData data)
    {
        prompts = data;
        if (data != null) enabled = true;
    }
}
