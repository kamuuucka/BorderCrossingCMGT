using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Slider_ui_test : MonoBehaviour
{
    public TMP_Text prompt;
    public Slider slider1;
    public Slider slider2;
    public List<string> prompts;
    public int delay;
    public GameObject imagePrefab;

    private float _slider1Value;
    private float _slider2Value;
    private int _prompt;
    private readonly List<Vector3> _positions = new();

    private void Awake()
    {
        ResetSliders();
        _prompt = 0;
        prompt.text = prompts[_prompt];
    }

    private void Update()
    {
        _slider1Value = slider1.value;
        _slider2Value = slider2.value;
    }

    public void Display()
    {
        var alreadyUsed = false;
        foreach (var position in _positions)
        {
            if (position == slider1.handleRect.position || position == slider2.handleRect.position)
            {
                alreadyUsed = true;
            }
        }

        if (alreadyUsed)
        {
            prompt.text = "You already chose this one!";
            StartCoroutine(SwitchTextAfterDelay(prompts[_prompt]));
        }
        else
        {
            prompt.text =
                $"You chose: {(int)(_slider1Value * 100)} for yourself and {(int)(_slider2Value * 100)} for others";
            _prompt++;
            StartCoroutine(SwitchTextAfterDelay(prompts[_prompt]));
            SaveThePosition(slider1.handleRect.position, true);
            SaveThePosition(slider2.handleRect.position, true);
            ResetSliders();
        }
        
    }

    private void ResetSliders()
    {
        slider1.value = 0.5f;
        slider2.value = 0.5f;
    }

    private void SaveThePosition(Vector3 position, bool sendToTheBack)
    {
        var newUI = Instantiate(imagePrefab, this.transform);
        var newTransform = newUI.GetComponent<RectTransform>();

        if (newTransform != null)
        {
            newTransform.position = position;
        }

        _positions.Add(position);
        if (sendToTheBack) newUI.transform.SetAsFirstSibling();
        else newUI.transform.SetAsLastSibling();
    }

    private IEnumerator SwitchTextAfterDelay(string text)
    {
        yield return new WaitForSeconds(delay);
        prompt.text = text;
    }
}