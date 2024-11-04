using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BoundaryDataStorage : MonoBehaviour
{
    [SerializeField] List<BoundaryData> allData;
    [SerializeField] private UnityEvent<string> onCountUpdate;

    private void Awake()
    {
        string countText = 0 + "/" + PhotonNetwork.CurrentRoom.PlayerCount;
        onCountUpdate?.Invoke(countText);
    }


    public void AddBoundaryData(BoundaryData newData) { 
        allData.Add(newData);

        string countText = allData.Count + "/" + PhotonNetwork.CurrentRoom.PlayerCount;
        onCountUpdate?.Invoke(countText);
    }
}
