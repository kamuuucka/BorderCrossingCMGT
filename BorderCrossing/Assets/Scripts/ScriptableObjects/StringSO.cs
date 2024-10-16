using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "New String SO", menuName = "SO/String")]
public class StringSO : ScriptableObject
{
    [SerializeField] private string value;

    public void SaveString(string newValue)
    {
       value = newValue;
    }

    public string ReadString()
    {
        return value;
    }
}
