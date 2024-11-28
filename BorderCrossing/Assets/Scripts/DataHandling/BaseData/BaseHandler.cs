using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class BaseHandler : MonoBehaviour, IDataPersistence
{
    [SerializeField] protected GameObject group;
    [FormerlySerializedAs("promptRecord")] [SerializeField] protected DataRecord dataRecord;
    [SerializeField] protected bool isDebug;

    private readonly List<DataRecord> _records = new();

    public void LoadData(PromptsData data)
    {
        var listOfPrompts = data.promptList;

        foreach (var prompt in listOfPrompts)
        {
            CreateNewPromptRecord(prompt);
        }
    }

    public void SaveData(ref PromptsData data)
    {
        
    }

    protected void DeleteData(int id)
    {
        Destroy(_records[id].gameObject);
        _records.RemoveAt(id);
        OnDeleteData(id);
    }

    protected abstract void OnDeleteData(int id);
    protected abstract void UseData(int id);

    public void CreateNewPromptRecord(PromptsData.Prompts prompt)
    {
        var newRecord = Instantiate(dataRecord, group.transform);
        newRecord.ChangeText(prompt.name);
        prompt.image = newRecord.GetColor();
        _records.Add(newRecord);
        var id = _records.IndexOf(newRecord);
        if (isDebug) Debug.Log($"Is this prompt a basic one? {prompt.basePrompt}");
        if (prompt.basePrompt) newRecord.GetDeleteButton().gameObject.SetActive(false);
        newRecord.GetDeleteButton().onClick.AddListener(()=> DeleteData(id));
        newRecord.GetLoadButton().onClick.AddListener(()=>UseData(id));
        
    }
}
