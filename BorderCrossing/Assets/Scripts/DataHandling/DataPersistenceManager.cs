using System.Collections.Generic;
using UnityEngine;

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
        CheckForActivePrompt(_promptsData);
        CheckForActiveDiscussion(_discussionsData);
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

    /// <summary>
    /// Check if there's an active element. If not, set the first one as the active one.
    /// </summary>
    /// <param name="dataToCheck">Game data containing the elements to check.</param>
    private void CheckForActivePrompt(GameData dataToCheck)
    {
        bool foundActivePrompt = false;
        
        foreach (var prompt in dataToCheck.Elements)
        {
            if(isDebug) Debug.Log($"Am I active? {prompt.active}.");
            if (prompt.active && !foundActivePrompt)
            {
                foundActivePrompt = true;
                OnSelectThePromptSave(dataToCheck.Elements.IndexOf(prompt));
            }
            else if (prompt.active && foundActivePrompt)
            {
                prompt.active = false;
            }
        }

        if (foundActivePrompt) return;
        
        if(isDebug) Debug.Log("Setting the first prompt as active");
        OnSelectThePromptSave(0);
    }

    /// <summary>
    /// Check if there's an active element. If not, set the first one as the active one.
    /// </summary>
    /// <param name="dataToCheck">Game data containing the elements to check.</param>
    private void CheckForActiveDiscussion(GameData dataToCheck)
    {
        bool foundActiveDiscussion = false;
        
        foreach (var discussion in dataToCheck.Elements)
        {
            if(isDebug) Debug.Log($"Am I active? {discussion.active}.");
            if (discussion.active && !foundActiveDiscussion)
            {
                foundActiveDiscussion = true;
                OnSelectTheDiscussionSave(dataToCheck.Elements.IndexOf(discussion));
            }
            else if (discussion.active && foundActiveDiscussion)
            {
                discussion.active = false;
            }
        }

        if (foundActiveDiscussion) return;
        
        if(isDebug) Debug.Log("Setting the first discussion as active");
        OnSelectTheDiscussionSave(0);
    }


    /// <summary>
    /// Saves the new added elements and the state of the whole game.
    /// </summary>
    public void SaveGame()
    {
        promptImporter.SaveData(ref _promptsData);
        discussionImporter.SaveData(ref _discussionsData);

        _questionsDataHandler.Save(_promptsData);
        _discussionsDataHandler.Save(_discussionsData);
    }
    
    /// <summary>
    /// Happens when the prompt deletion is performed. 
    /// </summary>
    /// <param name="id">The id of the element that needs to be removed from GameData.</param>
    public void OnDeletePromptSave(int id)
    {
        DeleteSave(id, _promptsData);
        _questionsDataHandler.Save(_promptsData);
    }

    /// <summary>
    /// Happens when the discussion deletion is performed.
    /// </summary>
    /// <param name="id">The id of the element that needs to be removed from GameData.</param>
    public void OnDeleteDiscussionSave(int id)
    {
        DeleteSave(id, _discussionsData);
        _discussionsDataHandler.Save(_discussionsData);
    }

    /// <summary>
    /// Removes the specific element from the GameData.
    /// If it's the currently active one, switches to the first one.
    /// </summary>
    /// <param name="id">The id of the element that needs to be removed from GameData.</param>
    /// <param name="gameData">GameData list that the operation will be performed on.</param>
    private void DeleteSave(int id, GameData gameData)
    {
        if(isDebug) Debug.Log($"Value {gameData.Elements[id].name} just got removed");
        if (gameData.Elements[id].active)
        {
            OnSelectThePromptSave(id == 0 ? 1 : 0);
        }
        gameData.Elements.Remove(gameData.Elements[id]);
    }

    /// <summary>
    /// Happens when the prompt selection is performed.
    /// </summary>
    /// <param name="id">The id of the element that needs to be selected.</param>
    public void OnSelectThePromptSave(int id)
    {
        SelectSave(id, _promptsData, promptsToUse, ref _activePrompt);
        
        settingsManager.UpdatePromptsSettings(_activePrompt.name, promptsToUse.data.Count);
        _questionsDataHandler.Save(_promptsData);
    }

    /// <summary>
    /// Happens when the discussion selection is performed.
    /// </summary>
    /// <param name="id">The id of the element that needs to be selected.</param>
    public void OnSelectTheDiscussionSave(int id)
    {
        SelectSave(id, _discussionsData, discussionsToUse, ref _activeDiscussion);
        
        settingsManager.UpdateDiscussionSettings(_activeDiscussion.name, discussionsToUse.data.Count);
        _discussionsDataHandler.Save(_discussionsData);
    }

    /// <summary>
    /// Marks the element as the active one. Updates also its colour and the data transferred through the levels.
    /// </summary>
    /// <param name="id">The id of the element that needs to be selected from GameData.</param>
    /// <param name="gameData">GameData list that the operation will be performed on.</param>
    /// <param name="dataToUse">Data that needs to be carried through the levels.</param>
    /// <param name="activeElement">Reference to the active element.</param>
    private void SelectSave(int id, GameData gameData, StringData dataToUse, ref GameData.DataElement activeElement)
    {
        foreach (var prompt in gameData.Elements)
        {
            prompt.active = gameData.Elements[id] == prompt;
            activeElement = gameData.Elements[id];

            if(prompt != gameData.Elements[id]) prompt.image.color = Color.white;
        }
        if(isDebug) Debug.Log($"Active prompt: {activeElement.name}");
        activeElement.image.color = new Color(0.839f, 0.89f, 0.694f);
        dataToUse.data = gameData.Elements[id].content;
    }
}