using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonActiveXTimes : MonoBehaviour
{
    public  UnityEvent onEnable;
    private void OnEnable()
    {
        onEnable.AddListener(Something);
    }

    private void OnDisable()
    {
        onEnable.RemoveListener(Something);
    }

    private void Something()
    {
        throw new NotImplementedException();
    }
}
