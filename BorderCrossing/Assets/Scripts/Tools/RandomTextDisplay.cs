using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Random = UnityEngine.Random;

[RequireComponent(typeof(TMP_Text))]
public class RandomTextDisplay : MonoBehaviour
{
    [TextArea] [SerializeField] private List<string> textToChooseFrom;
    [SerializeField] private List<UnityEvent> onOptionEvents;
    private TMP_Text _textField;
    private int _lastChoice = -1;
    private int _repeatCount = 0;

    private void OnEnable()
    {
        _textField = GetComponent<TMP_Text>();
        var randomValue = GetRandomChoice();
        _textField.text = textToChooseFrom[randomValue];
        onOptionEvents[randomValue]?.Invoke();
    }
    
    private int GetRandomChoice()
    {
        int newChoice;
        
        if (_repeatCount >= 2)
        {
            newChoice = 1 - _lastChoice;
            _repeatCount = 1;
        }
        else
        {
            newChoice = Random.Range(0, 2);
            
            if (newChoice == _lastChoice)
            {
                _repeatCount++;
            }
            else
            {
                _repeatCount = 1;
            }
        }

        _lastChoice = newChoice;
        return newChoice;
    }
}
