using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class CopyNameBetweenElements : MonoBehaviour
{
    private TMP_Text _text;
    private void Start()
    {
        _text = GetComponent<TMP_Text>();
    }

    public void SetTextTo(TMP_Text text)
    {
        _text.text = text.text;
    }
}
