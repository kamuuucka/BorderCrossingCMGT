using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "New String Data", menuName = "Data/String Data")]
public class StringData : ScriptableObject
{
    [TextArea]
    public List<string> data;

    public static StringData DeSerialize(string serializedBoundaryData)
    {
        StringData stringData = ScriptableObject.CreateInstance<StringData>();
        stringData.data = serializedBoundaryData.Split(',').ToList();
        return stringData;
    }

    public static string Serialize(StringData toSerialize)
    {
        return string.Join(",", toSerialize.data);
    }
}
