using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class ChangePrompt : MonoBehaviour
{
    [SerializeField] private PromptData questions;
    
    private TMP_Text _questionText;
    private int _activePrompt;

    private void Start()
    {
        _questionText = GetComponent<TMP_Text>();
        _questionText.text = questions.data[_activePrompt];
    }
    
    public void GoToTheNextPrompt()
    {
        _activePrompt++;
        _questionText.text = questions.data[_activePrompt];
    }
}
