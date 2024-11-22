using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Float", menuName = "Persistent Variables/Float")]
public class PersistentFloat : ScriptableObject
{
    public float value;

    public void SetInt(float value)
    {
        this.value = value;
    }
}
