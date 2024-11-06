using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Boundary Data List", menuName = "Data/Boundary Data List")]
public class BoundaryDataList : ScriptableObject
{
    [SerializeField] private List<BoundaryData> data;

    public void SaveData(List<BoundaryData> newData)
    {
        data = newData;
    }

    public List<BoundaryData> ReadData()
    {
        return data;
    }
}
