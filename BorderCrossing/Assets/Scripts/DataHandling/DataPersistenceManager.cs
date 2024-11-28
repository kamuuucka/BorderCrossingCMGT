using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class DataPersistenceManager : MonoBehaviour
{
    #region Exposed Variables
    [Header("Save Files")]
    [Tooltip("Name of the save file containing prompts.")]
    [SerializeField] private string promptsFileName = "promptsData";
    [Tooltip("Name of the save file containing discussions.")]
    [SerializeField] private string discussionFileName = "discussionsData";
    [Space(10)][Header("StringData Objects")]
    [Tooltip("StringData that will carry the prompts through the game.")]
    [SerializeField] private StringData promptsToUse;
    [Tooltip("StringData that will carry the discussions through the game.")]
    [SerializeField] private StringData discussionsToUse;
    [Tooltip("StringData with prompts provided by us.")]
    [SerializeField] private StringData defaultPrompts;
    [Tooltip("StringData with discussions provided by us.")]
    [SerializeField] private StringData defaultDiscussions;
    [Space(10)][Header("Handlers & Importers")]
    [Tooltip("Handler for the prompts.")]
    [SerializeField] private BaseHandler promptHandler;
    [Tooltip("Handler for the discussions.")]
    [SerializeField] private BaseHandler discussionHandler;
    [Tooltip("Importer for the prompts.")]
    [SerializeField] private BaseImporter promptImporter;
    [Tooltip("Importer for the discussions.")]
    [SerializeField] private BaseImporter discussionImporter;
    [Space(10)][Header("Settings Manager")]
    [Tooltip("Settings manager, updates the visuals.")]
    [SerializeField] private SettingsManager settingsManager;
    [Space(10)][Header("Debug")]
    [SerializeField] private bool isDebug;
    #endregion

    #region Public Variables
    public static DataPersistenceManager Instance { get; private set; }
    #endregion

    #region Private Variables
    private GameData _promptsData;
    private GameData _discussionsData;
    private GameData.DataElement _activePrompt;
    private GameData.DataElement _activeDiscussion;
    private FileDataHandler _questionsDataHandler;
    private FileDataHandler _discussionsDataHandler;
    #endregion

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
        _questionsDataHandler = new FileDataHandler(Application.persistentDataPath, promptsFileName);
        _discussionsDataHandler = new FileDataHandler(Application.persistentDataPath, discussionFileName);
        LoadGame();
    }
    private void LoadGame()
    {
        //Load the files
        _promptsData = _questionsDataHandler.Load();
        _discussionsData = _discussionsDataHandler.Load();

        //Create the new game if there is nothing in the files
        if (_promptsData == null || _discussionsData == null)
        {
            if(isDebug) Debug.Log("GameData missing!");
            NewGame();
        }
        
        //Load the data into the game.
        promptHandler.LoadData(_promptsData);
        discussionHandler.LoadData(_discussionsData);

        //Check for the active prompt / discussion.
        CheckForActive(_promptsData);
        CheckForActive(_discussionsData);
    }


    /// <summary>
    /// Creates the new game save.
    /// </summary>
    private void NewGame()
    {
        if (defaultPrompts != null && defaultDiscussions != null)
        {
            if(isDebug) Debug.Log("Generating default game data.");
            CreateNewGameData(ref _promptsData,"General questions about transgressive behaviour", defaultPrompts.data);
            CreateNewGameData(ref _discussionsData,"General discussion topics about transgressive behaviour", defaultDiscussions.data);
        }
        else if (isDebug)
        {
            Debug.LogError("No default values assigned. The game files won't generate.");
        }
    }

    /// <summary>
    /// Creates a new GameData variable on the object.
    /// </summary>
    /// <param name="newData">The GameData object that will store the data.</param>
    /// <param name="name">The name of the collection.</param>
    /// <param name="content">The content of the collection.</param>
    private void CreateNewGameData(ref GameData newData, string name, List<string> content)
    {
        newData = new GameData();
        newData.AddNewPrompts(name, content);
        newData.Elements[0].basePrompt = true;
        newData.Elements[0].active = true;
    }

    private void CheckForActive(GameData dataToCheck)
    {
        bool foundActivePrompt = false;
        
        foreach (var prompt in dataToCheck.Elements)
        {
            if(isDebug) Debug.Log($"Am I active? {prompt.active}");
            if (prompt.active && !foundActivePrompt)
            {
                foundActivePrompt = true;
                UseThePromptSave(dataToCheck.Elements.IndexOf(prompt));
            }
            else if (prompt.active && foundActivePrompt)
            {
                prompt.active = false;
            }
        }
        
        if (!foundActivePrompt)
        {
            if(isDebug) Debug.Log("Setting the first prompt as active");
            UseThePromptSave(0);
        }
    }


    public void SaveGame()
    {
        promptImporter.SaveData(ref _promptsData);
        discussionImporter.SaveData(ref _discussionsData);

        _questionsDataHandler.Save(_promptsData);
        _discussionsDataHandler.Save(_discussionsData);
    }

    public void DeletePromptSave(int id)
    {
        if(isDebug) Debug.Log($"Value {_promptsData.Elements[id].name} just got removed");
        if (_promptsData.Elements[id].active)
        {
            UseThePromptSave(id == 0 ? 1 : 0);
        }
        _promptsData.Elements.Remove(_promptsData.Elements[id]);

        _questionsDataHandler.Save(_promptsData);
    }

    public void DeleteDiscussionSave(int id)
    {
        if(isDebug) Debug.Log($"Value {_discussionsData.Elements[id].name} just got removed");
        if (_discussionsData.Elements[id].active)
        {
            UseTheDiscussionSave(id == 0 ? 1 : 0);
        }
        _discussionsData.Elements.Remove(_discussionsData.Elements[id]);

        _discussionsDataHandler.Save(_discussionsData);
    }

    public void UseThePromptSave(int id)
    {
        foreach (var prompt in _promptsData.Elements)
        {
            prompt.active = _promptsData.Elements[id] == prompt;
            _activePrompt = _promptsData.Elements[id];

            if(prompt != _promptsData.Elements[id]) prompt.image.color = Color.white;
        }
        if(isDebug) Debug.Log($"Active prompt: {_activePrompt.name}");
        _activePrompt.image.color = new Color(0.839f, 0.89f, 0.694f);
        promptsToUse.data = _promptsData.Elements[id].content;
        
        settingsManager.UpdatePromptsSettings(_activePrompt.name, promptsToUse.data.Count);
        
        _questionsDataHandler.Save(_promptsData);
    }

    public void UseTheDiscussionSave(int id)
    {
        foreach (var prompt in _discussionsData.Elements)
        {
            prompt.active = _discussionsData.Elements[id] == prompt;
            _activeDiscussion = _discussionsData.Elements[id];
            
            if(prompt != _discussionsData.Elements[id]) prompt.image.color = Color.white;
        }
        if(isDebug) Debug.Log($"Active prompt: {_activeDiscussion.name}");
        _activeDiscussion.image.color = new Color(0.839f, 0.89f, 0.694f);
        discussionsToUse.data = _discussionsData.Elements[id].content;
        
        settingsManager.UpdateDiscussionSettings(_activeDiscussion.name, discussionsToUse.data.Count);
        
        _discussionsDataHandler.Save(_discussionsData);
    }

    public GameData.DataElement GetActivePrompt()
    {
        return _activePrompt;
    }
    
}