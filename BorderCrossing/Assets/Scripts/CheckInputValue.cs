using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

[RequireComponent(typeof(TMP_InputField))]
public class CheckInputValue : MonoBehaviour
{
    [Tooltip("The SO that contains the correct value that should be put into the input field")]
    [SerializeField] private StringSO correctValue;
    [Tooltip("The character that will be used as a placeholder for each element of the input")]
    [SerializeField] private char placeholderChar = '0';
    [SerializeField] private UnityEvent onCodeCorrect;
    [SerializeField] private UnityEvent onCodeIncorrect;
    private TMP_InputField _inputField;

    private void Awake()
    {
        _inputField = GetComponent<TMP_InputField>();
        if (_inputField != null) SetUpInputField();

    }

    /// <summary>
    /// Sets up the character limit and placeholder text based on the length of the correct code
    /// </summary>
    private void SetUpInputField()
    {
        var codeLength = correctValue.ReadString().Length;
        _inputField.characterLimit = codeLength;
        var tempString = placeholderChar.ToString();
        if (codeLength > 1)
        {
            for (var i = 0; i < codeLength-1; i++)
            {
                tempString += placeholderChar;
            }
        }
        
        _inputField.placeholder.GetComponent<TMP_Text>().text = tempString;
    }

    /// <summary>
    /// Checks if the value in the input field is the same as the one in the SO
    /// </summary>
    public void CheckValue()
    {
        if (_inputField.text.Equals(correctValue.ReadString(), StringComparison.InvariantCultureIgnoreCase))
        {
            onCodeCorrect?.Invoke();
            return;
        }
        onCodeIncorrect?.Invoke();
    }
}
