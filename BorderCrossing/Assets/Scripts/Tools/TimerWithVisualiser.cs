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
                Debug.Log("Timer finish");
                whenTimerFinished?.Invoke();
            }
        }
    }

    public void ResetTimer()
    {
        _startTimer = false;
        _currentTime = timer.value;
    }

    public void ResetAndStart()
    {
        _currentTime = timer.value;
        _startTimer = true;
    }

    public void StartTimer()
    {
        Debug.Log("Timer start");
        _startTimer = true;
    }

    public void PauseResumeTimer()
    {
        _startTimer = !_startTimer;
    }

    public void StopTimer()
    {
        _startTimer = false;
    }

    public void SetTimer(float value)
    {
        timer.SetInt(value);
        ResetTimer();
    }
    
}
