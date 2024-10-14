using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class AnswersData : MonoBehaviour
{
    [SerializeField] private TMP_Text question;
    [SerializeField] private List<TMP_Text> scores;
    [SerializeField] private List<TMP_Text> scale;
    [SerializeField] private TMP_Text average;
    [SerializeField] private ColorPreset graphColorPreset;

    private void OnEnable()
    {
        if (scale == null || scale.Count != graphColorPreset.LoadColorPreset().Count) return;
        
        for (var i = 0; i < scale.Count; i++)
        {
            scale[i].color = graphColorPreset.LoadColorPreset()[i];
        }
    }

    public void SetQuestion(string value)
    {
        question.text = value;
    }

    public void SetScores(List<BoundaryData> data, int questionNumber)
    {
        foreach (var boundaryData in data)
        {
            if (scores[boundaryData.data[questionNumber]].text != "0")
            {
                var scoreToInt = int.Parse(scores[boundaryData.data[questionNumber]].text);
                scoreToInt++;
                scores[boundaryData.data[questionNumber]].text = scoreToInt.ToString();
            }
            else
            {
                scores[boundaryData.data[questionNumber]].text = "1";
            }
        }
    }

    public void Average(List<BoundaryData> data, int questionNumber)
    {
        var values = data.Select(boundaryData => boundaryData.data.ElementAt(questionNumber)).ToList();

        var size = values.Count;
        var midIndex = size / 2;
        var median = size % 2 == 0
            ? (((values[midIndex - 1] + 1) + (values[midIndex]) + 1) / 2.0f)
            : (values[midIndex] + 1);
        average.text = $"Class Average: {median}" ;
    }
}
