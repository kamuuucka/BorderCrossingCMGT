using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour
{
    [SerializeField] private StringData promptsData;
    [SerializeField] private UnityEvent onPromptsFinished;

    private int _promptsUsed;
    private bool _started;

    private void OnEnable()
    {
        onPromptsFinished.AddListener(HandleOnPromptsFinished);
    }

    private void OnDisable()
    {
        onPromptsFinished.RemoveListener(HandleOnPromptsFinished);
    }

    private void Update()
    {
        if (_promptsUsed == promptsData.data.Count && !_started)
        {
            _started = true;
            onPromptsFinished.Invoke();
        }
    }

    public void PromptUsed()
    {
        _promptsUsed++;
    }

    private void HandleOnPromptsFinished()
    {
        Debug.Log("No more prompts to display!");
    }
}
