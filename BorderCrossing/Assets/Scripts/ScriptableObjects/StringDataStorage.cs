using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New String Data Storage", menuName = "Storage/String Data Storage")]
public class StringDataStorage : ScriptableObject
{
    public int activeList;
    public List<StringList> stringDatas;

    public void ReadActiveList()
    {
        Debug.Log(stringDatas[activeList].data);
    }

    [Serializable]
    public class StringList
    {
        public List<string> data;
    }
}