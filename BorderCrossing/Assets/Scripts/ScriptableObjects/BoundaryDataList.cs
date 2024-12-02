using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Boundary Data List", menuName = "Data/Boundary Data List")]
public class BoundaryDataList : ScriptableObject
{
    [SerializeField] private List<BoundaryData> data = new();

    private void Awake()
    {
        data.Clear();
    }

    public void SaveData(BoundaryData newData)
    {
        Debug.Log("Saving data");
        data.Add(newData);
        Debug.Log(data.Count);
    }

    public List<BoundaryData> ReadData()
    {
        return data;
    }
}
