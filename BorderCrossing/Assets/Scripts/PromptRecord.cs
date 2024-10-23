using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PromptRecord : MonoBehaviour
{
    [SerializeField] private TMP_Text textField;
    [SerializeField] private Button deleteButton;
    [SerializeField] private Button loadButton;

    public void ChangeText(string value)
    {
        textField.text = value;
    }

    public Button GetDeleteButton()
    {
        return deleteButton;
    }

    public Button GetLoadButton()
    {
        return loadButton;
    }
}
