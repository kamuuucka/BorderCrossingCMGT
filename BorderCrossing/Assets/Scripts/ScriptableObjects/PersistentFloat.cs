using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Float", menuName = "Persistent Variables/Float")]
public class PersistentFloat : ScriptableObject
{
    public float value = 10f;

    private void Awake()
    {
        // value = 0;
    }

    public void SetFloat(float value)
    {
        Debug.Log("Setting value to the float");
        this.value = value;
    }

    public float GetFloat()
    {
        return value;
    }
}
