using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Serialization;

public class DataPersistenceManager : MonoBehaviour
{
    [SerializeField] private string fileName;
    [SerializeField] private string debatesFileName;
    [SerializeField] private StringData promptsToUse;
    [SerializeField] private StringData defaultPrompts;
    [SerializeField] private StringData debatesToUse;
    [SerializeField] private StringData defaultDebates;

    public static DataPersistenceManager Instance { get; private set; }
    private PromptsData _promptsData;
    private PromptsData.Prompts _activePrompt;
    private FileDataHandler _questionsDataHandler;
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
        _questionsDataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        _dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }


    private void NewGame()
    {
        _promptsData = new PromptsData();
        _promptsData.AddNewPrompts("General questions about transgressive behaviour", defaultPrompts.data);
        _promptsData.promptList[0].basePrompt = true;
        _promptsData.promptList[0].active = true;
    }

    private void LoadGame()
    {
        _promptsData = _questionsDataHandler.Load();

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
        Debug.Log(_dataPersistenceObjects.Count);
        foreach (var dataPersistenceObject in _dataPersistenceObjects)
        {
            Debug.Log("AHsyvdahejvdhj");
            dataPersistenceObject.SaveData(ref _promptsData);
        }

        _questionsDataHandler.Save(_promptsData);
    }

    public void DeleteSave(int id)
    {
        Debug.Log($"Value {_promptsData.promptList[id].name} just got removed");
        if (_promptsData.promptList[id].active)
        {
            UseTheSave(id == 0 ? 1 : 0);
        }
        _promptsData.promptList.Remove(_promptsData.promptList[id]);

        _questionsDataHandler.Save(_promptsData);
    }

    public void UseTheSave(int id)
    {
        foreach (var prompt in _promptsData.promptList)
        {
            prompt.active = _promptsData.promptList[id] == prompt;
            _activePrompt = _promptsData.promptList[id];
        }

        Debug.Log($"Now using: {_promptsData.promptList[id].name}");
        Debug.Log($"Active prompt: {_activePrompt.name}");
        promptsToUse.data = _promptsData.promptList[id].prompts;

        _questionsDataHandler.Save(_promptsData);
    }

    public PromptsData.Prompts GetActivePrompt()
    {
        return _activePrompt;
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects =
            FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistenceObjects);
    }
}