using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "New String Data", menuName = "Data/String Data")]
public class StringData : ScriptableObject
{
    [TextArea]
    public List<string> data;
}
