using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New Boundary Data", menuName = "Data/Boundary Data")]
public class BoundaryData : ScriptableObject
{
    public List<int> data;

    public static BoundaryData DeSerialize(string serializedBoundaryData) 
    {
        BoundaryData boundaryData = ScriptableObject.CreateInstance<BoundaryData>();
        boundaryData.data = serializedBoundaryData.Split(',').Select(int.Parse).ToList();
        return boundaryData;
    }

    public static string Serialize(BoundaryData toSerialize)
    {
        return string.Join(",", toSerialize.data);
    }

}
