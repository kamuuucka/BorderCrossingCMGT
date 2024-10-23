using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataPersistenceManager : MonoBehaviour
{
    public static DataPersistenceManager Instance { get; private set; }
    private PromptsData _promptsData;
    private List<IDataPersistence> _dataPersistenceObjects;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is already an instance!");
        }

        Instance = this;
    }

    private void Start()
    {
        _dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects =
            FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistenceObjects);
    }

    public void NewGame()
    {
        _promptsData = new PromptsData();
    }

    public void LoadGame()
    {
        if (_promptsData == null)
        {
            Debug.Log("There's no game data");
            NewGame();
        }
    }

    public void SaveGame()
    {
        
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }
}
