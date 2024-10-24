using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataPersistenceManager : MonoBehaviour
{
    [SerializeField] private string fileName;
    [SerializeField] private StringData promptsToUse;

    public static DataPersistenceManager Instance { get; private set; }
    private PromptsData _promptsData;
    private FileDataHandler _dataHandler;
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
        _dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        _dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }


    private void NewGame()
    {
        _promptsData = new PromptsData();
    }

    private void LoadGame()
    {
        _promptsData = _dataHandler.Load();

        if (_promptsData == null)
        {
            Debug.Log("There's no game data");
            NewGame();
        }

        foreach (var dataPersistenceObject in _dataPersistenceObjects)
        {
            dataPersistenceObject.LoadData(_promptsData);
        }

        var foundActive = false;
        foreach (var prompt in _promptsData.promptList)
        {
            Debug.Log($"Am I active? {prompt.active}");
            if (prompt.active && !foundActive)
            {
                foundActive = true;
                UseTheSave(_promptsData.promptList.IndexOf(prompt));
            }
            else if (prompt.active && foundActive)
            {
                prompt.active = false;
            }
        }

        if (foundActive) return;
        
        Debug.Log("Setting the first prompt as active");
        _promptsData.promptList[0].active = true;
    }

    public void SaveGame()
    {
        foreach (var dataPersistenceObject in _dataPersistenceObjects)
        {
            dataPersistenceObject.SaveData(ref _promptsData);
        }

        _dataHandler.Save(_promptsData);
    }

    public void DeleteSave(int id)
    {
        Debug.Log($"Value {_promptsData.promptList[id].name} just got removed");
        if (_promptsData.promptList[id].active)
        {
            UseTheSave(id == 0 ? 1 : 0);
        }
        _promptsData.promptList.Remove(_promptsData.promptList[id]);

        _dataHandler.Save(_promptsData);
    }

    public void UseTheSave(int id)
    {
        foreach (var prompt in _promptsData.promptList)
        {
            prompt.active = _promptsData.promptList[id] == prompt;
        }

        Debug.Log($"Now using: {_promptsData.promptList[id].name}");
        promptsToUse.data = _promptsData.promptList[id].prompts;

        _dataHandler.Save(_promptsData);
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects =
            FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistenceObjects);
    }
}