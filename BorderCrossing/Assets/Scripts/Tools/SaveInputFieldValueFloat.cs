using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TMP_InputField))]
public class SaveInputFieldValueFloat : MonoBehaviour
{
    [SerializeField] private PersistentFloat persistentFloat;

    private TMP_InputField _inputField;

    private void Start()
    {
        _inputField = GetComponent<TMP_InputField>();
        persistentFloat.SetFloat(5f);
    }

    public void SaveValue()
    {
        if (IsNumeric(_inputField.text))
        {
            persistentFloat.SetFloat(float.Parse(_inputField.text));
        }
        else
        {
            _inputField.text = "Must be numbers!";
        }

        Debug.Log(persistentFloat.GetFloat());
    }
    
    private bool IsNumeric(string input)
    {
        foreach (char c in input)
        {
            if (!char.IsDigit(c) && c != '.' && c != '-') // Allow decimal points and negative sign
            {
                return false;
            }
        }
        return true;
    }

}
