using UnityEngine;
using UnityEngine.Serialization;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("Save Files")]
    [Tooltip("Name of the save file containing prompts.")]
    [SerializeField] private string promptsFileName = "promptsData";
    [Tooltip("Name of the save file containing discussions.")]
    [SerializeField] private string discussionFileName = "discussionsData";
    [Space(10)][Header("StringData Objects")]
    [SerializeField] private StringData promptsToUse;
    [SerializeField] private StringData defaultPrompts;
    [SerializeField] private StringData discussionsToUse;
    [SerializeField] private StringData defaultDiscussions;
    [SerializeField] private SettingsManager settingsManager;
    [SerializeField] private bool isDebug;
    [SerializeField] private BaseHandler promptHandler;
    [SerializeField] private BaseImporter promptImporter;
    [SerializeField] private BaseHandler discussionHandler;
    [SerializeField] private BaseImporter discussionImporter;

    public static DataPersistenceManager Instance { get; private set; }
    private GameData _promptsData;
    private GameData _discussionsData;
    private GameData.DataElement _activePrompt;
    private GameData.DataElement _activeDiscussion;
    private FileDataHandler _questionsDataHandler;
    private FileDataHandler _discussionsDataHandler;

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


    private void NewGame()
    {
        if(isDebug) Debug.Log("Generating default game data.");
        _promptsData = new GameData();
        _promptsData.AddNewPrompts("General questions about transgressive behaviour", defaultPrompts.data);
        _promptsData.Elements[0].basePrompt = true;
        _promptsData.Elements[0].active = true;
        _discussionsData = new GameData();
        _discussionsData.AddNewPrompts("General discussion topics about transgressive behaviour", defaultDiscussions.data);
        _discussionsData.Elements[0].basePrompt = true;
        _discussionsData.Elements[0].active = true;
    }

    private void LoadGame()
    {
        _promptsData = _questionsDataHandler.Load();
        _discussionsData = _discussionsDataHandler.Load();

        if (_promptsData == null || _discussionsData == null)
        {
            if(isDebug) Debug.Log("GameData missing!");
            NewGame();
        }
        
        promptHandler.LoadData(_promptsData);
        discussionHandler.LoadData(_discussionsData);

        var foundActivePrompt = false;
        foreach (var prompt in _promptsData.Elements)
        {
            if(isDebug) Debug.Log($"Am I active? {prompt.active}");
            if (prompt.active && !foundActivePrompt)
            {
                foundActivePrompt = true;
                UseThePromptSave(_promptsData.Elements.IndexOf(prompt));
            }
            else if (prompt.active && foundActivePrompt)
            {
                prompt.active = false;
            }
        }

        if (!foundActivePrompt)
        {
            if(isDebug) Debug.Log("Setting the first prompt as active");
            _promptsData.Elements[0].active = true;
        }
        
        var foundActiveDiscussion = false;
        foreach (var discussion in _discussionsData.Elements)
        {
            if(isDebug) Debug.Log($"Am I active? {discussion.active}");
            if (discussion.active && !foundActiveDiscussion)
            {
                foundActiveDiscussion = true;
                UseTheDiscussionSave(_discussionsData.Elements.IndexOf(discussion));
            }
            else if (discussion.active && foundActiveDiscussion)
            {
                discussion.active = false;
            }
        }

        if (!foundActiveDiscussion)
        {
            if(isDebug) Debug.Log("Setting the first discussion as active");
            _discussionsData.Elements[0].active = true;
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