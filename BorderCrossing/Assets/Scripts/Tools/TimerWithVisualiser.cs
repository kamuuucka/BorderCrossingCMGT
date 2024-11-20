using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TimerWithVisualiser : MonoBehaviour
{
    [SerializeField] private UnityEvent whenTimerFinished;
    [SerializeField] private Image timerImage;
    [SerializeField] private PersistentFloat timer;

    private float _currentTime;
    private bool _startTimer;

    private void Start()
    {
        ResetTimer();
    }

    private void Update()
    {
        Debug.Log($"_startTimer is {_startTimer}");
        if (_startTimer)
        {
            if (_currentTime > 0)
            {
                _currentTime -= Time.deltaTime;
                float normalizedTime = Math.Clamp(_currentTime / timer.value, 0f, 1f);
                timerImage.fillAmount = normalizedTime;
            }
            else
            {
                _startTimer = false;
                whenTimerFinished?.Invoke();
            }
        }
    }

    public void ResetTimer()
    {
        _startTimer = false;
        _currentTime = timer.value;
        Debug.Log($"Time after reset {_currentTime}");
    }

    public void ResetAndStart()
    {
        _currentTime = timer.value;
        _startTimer = true;
        Debug.Log($"Timer set to {_startTimer}");
    }

    public void StartTimer()
    {
        _startTimer = true;
        Debug.Log($"Start timer, the value is {_startTimer}");
    }

    public void PauseResumeTimer()
    {
        Debug.Log("PAUSE");
        _startTimer = !_startTimer;
    }

    public void SetTimer(float value)
    {
        Debug.Log("Hagabagwa");
        timer.SetInt(value);
        ResetTimer();
    }
    
}
