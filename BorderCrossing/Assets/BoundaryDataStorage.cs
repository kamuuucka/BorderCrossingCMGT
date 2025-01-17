using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BoundaryDataStorage : MonoBehaviour
{
    [SerializeField] List<BoundaryData> allData;
    [SerializeField] private UnityEvent<string> onCountUpdate;
    [SerializeField] private BoundaryDataList dataList;

    public static List<BoundaryData> NewDataList = new();

    private void Awake()
    {
        string countText = 0 + "/" + (PhotonNetwork.CurrentRoom.PlayerCount - 1);
        onCountUpdate?.Invoke(countText);
    }


    public void AddBoundaryData(BoundaryData newData) { 
        allData.Add(newData);
        NewDataList.Add(newData);
        Debug.Log(BoundaryData.Serialize(newData));

        string countText = allData.Count + "/" + (PhotonNetwork.CurrentRoom.PlayerCount - 1);
        onCountUpdate?.Invoke(countText);
    }

    public void SaveToDataList()
    {
        foreach (var data in allData)
        {
            Debug.Log("trying to save data");
            dataList.SaveData(data);
            
        }
    }
}
