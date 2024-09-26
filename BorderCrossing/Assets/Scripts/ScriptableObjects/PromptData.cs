using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "New Prompt Data", menuName = "Data/Prompt Data")]
public class PromptData : ScriptableObject
{
    [TextArea]
    public List<string> data;
}
