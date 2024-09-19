using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TypeIN_ui_test : MonoBehaviour
{
    public Slider slider1;
    public Slider slider2;
    public List<string> prompts;
    public TMP_Text prompt;
    public int delay;
    public GameObject imagePrefab;
    public TMP_InputField inputField;
    
    private float _slider1Value;
    private float _slider2Value;
    private int _prompt;
    private readonly List<Vector3> _positions = new();
    private int _pressed;
    
    private void Awake()
    {
        ResetSliders();
        _prompt = 0;
        prompt.text = prompts[_prompt];
    }

    public void SaveInput()
    {
        float value;
        if (_pressed == 0)
        {
            if (float.TryParse(inputField.text, out value))
            {
                _slider1Value = value / 100.0f;
                _pressed++;
            }
            else
            {
                Debug.Log("This is not float");
            }
        }
        else
        {
            if (float.TryParse(inputField.text, out value))
            {
                _slider2Value = value / 100.0f;
                _pressed++;
            }
            else Debug.Log("This is not float");
        }
    }

    public void Display()
    {
        slider1.value = _slider1Value;
        slider2.value = _slider2Value;
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
            StartCoroutine(SkipAfterDelay.SwitchTextAfterDelay(prompts[_prompt], delay, prompt));
        }
        else
        {
            prompt.text =
                $"You chose: {(int)(_slider1Value * 100)} for yourself and {(int)(_slider2Value * 100)} for others";
            _prompt++;
            StartCoroutine(SkipAfterDelay.SwitchTextAfterDelay(prompts[_prompt], delay, prompt));
            SaveThePosition(slider1.handleRect.position, true);
            SaveThePosition(slider2.handleRect.position, true);
            ResetSliders();
        }
    }

    private void ResetSliders()
    {
        slider1.value = 0.5f;
        slider2.value = 0.5f;
        _pressed = 0;
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
}
