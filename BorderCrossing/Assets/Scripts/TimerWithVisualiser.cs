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
    [SerializeField] private float timer;

    private float _currentTime;
    private bool _startTimer = false;

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
                float normalizedTime = Math.Clamp(_currentTime / timer, 0f, 1f);
                timerImage.fillAmount = normalizedTime;
            }
            else
            {
                whenTimerFinished?.Invoke();
                _startTimer = false;
            }
        }
    }

    public void ResetTimer()
    {
        _startTimer = false;
        _currentTime = timer;
    }

    public void StartTimer()
    {
        _startTimer = true;
    }
    
}
