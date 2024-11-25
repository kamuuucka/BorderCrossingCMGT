using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private TMP_Text questTypePie;
    [SerializeField] private TMP_Text questNumPie;
    [SerializeField] private TMP_Text questTypeDis;
    [SerializeField] private TMP_Text questNumDis;
    
    public void UpdatePromptsSettings(string activeQuestionsName, int activeQuestionsNumber)
    {
        questTypePie.text = activeQuestionsName;
        questNumPie.text = activeQuestionsNumber.ToString();

    }

    public void UpdateDiscussionSettings(string activeDiscussionsName, int activeDiscussionsNumber)
    {
        questTypeDis.text = activeDiscussionsName;
        questNumDis.text = activeDiscussionsNumber.ToString();
    }
    
}
