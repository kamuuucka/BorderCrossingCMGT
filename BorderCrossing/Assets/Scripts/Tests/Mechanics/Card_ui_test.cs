using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Card_ui_test : MonoBehaviour
{
    public List<string> prompts;
    public TMP_Text button;

    private int _iteration;

    private void Start()
    {
        button.text = prompts[0];
    }

    public void NextCard()
    {
        _iteration++;
        button.text = prompts[_iteration];
    }
}
