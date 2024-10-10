using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DataDisplayer : MonoBehaviour
{
    [SerializeField] private StringData questions;
    [SerializeField] private List<BoundaryData> answers;
    [SerializeField] private GameObject dataDisplayer;
    [SerializeField] private TMP_Text textField;

    public void DisplayData()
    {
        foreach (var questionText in questions.data)
        {
            var question = Instantiate(textField, dataDisplayer.transform);
            question.text = questionText;
        }
    }
}
