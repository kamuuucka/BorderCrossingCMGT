using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Boundary Data", menuName = "Data/Boundary Data")]
public class BoundaryData : ScriptableObject
{
    public List<int> data;
}
