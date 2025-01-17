using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PersistentValuesManager : MonoBehaviour
{
    public BoundaryDataList dataList;
    public PersistentFloat timer;

    private void Awake()
    {
        DontDestroyOnLoad(dataList);
        DontDestroyOnLoad(timer);
    }
}
