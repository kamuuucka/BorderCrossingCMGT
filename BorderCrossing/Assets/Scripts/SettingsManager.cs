using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private StringData promptsToUse;
    [SerializeField] private StringData debatesToUse;
    [SerializeField] private TMP_Text questTypePie;
    [SerializeField] private TMP_Text questNumPie;

    public void SetQuestionsName()
    {
        questTypePie.text = DataPersistenceManager.Instance.GetActivePrompt().name;
    }

    public void DisplayQuestionsNumber(TMP_Text text)
    {
        text.text = promptsToUse.data.Count.ToString();
    }

    public void DisplayDebatesNumber(TMP_Text text)
    {
        text.text = debatesToUse.data.Count.ToString();
    }
}
