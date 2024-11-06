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
    [SerializeField] private BoundaryDataList dataList;

    public void DisplayData()
    {
        for (var i = 0; i < questions.data.Count; i++)
        {
            var questionText = questions.data[i];
            var question = Instantiate(textField, dataDisplayer.transform);
            question.SetQuestion(questionText);
            question.SetScores(dataList.ReadData(),i);
            question.Average(dataList.ReadData(),i);
        }
    }
}
