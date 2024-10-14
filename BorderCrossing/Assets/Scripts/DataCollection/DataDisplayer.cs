using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DataDisplayer : MonoBehaviour
{
    [SerializeField] private StringData questions;
    [SerializeField] private List<BoundaryData> answers;
    [SerializeField] private GameObject dataDisplayer;
    [SerializeField] private AnswersData textField;

    public void DisplayData()
    {
        for (var i = 0; i < questions.data.Count; i++)
        {
            var questionText = questions.data[i];
            var question = Instantiate(textField, dataDisplayer.transform);
            question.SetQuestion(questionText);
            question.SetScores(answers,i);
            question.Average(answers,i);
        }
    }
}
