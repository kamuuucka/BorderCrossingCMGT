using System.Collections.Generic;
using UnityEngine;

public abstract class BaseHandler : MonoBehaviour, IDataPersistence
{
    [SerializeField] protected GameObject group;
    [SerializeField] protected DataRecord dataRecord;
    [SerializeField] protected bool isDebug;

    private readonly List<DataRecord> _records = new();

    public void LoadData(GameData data)
    {
        var listOfPrompts = data.Elements;

        foreach (var prompt in listOfPrompts)
        {
            CreateNewPromptRecord(prompt);
        }
    }

    public void SaveData(ref GameData data)
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

    public void CreateNewPromptRecord(GameData.DataElement dataElement)
    {
        var newRecord = Instantiate(dataRecord, group.transform);
        newRecord.ChangeText(dataElement.name);
        dataElement.image = newRecord.GetColor();
        _records.Add(newRecord);
        var id = _records.IndexOf(newRecord);
        if (isDebug) Debug.Log($"Is this prompt a basic one? {dataElement.basePrompt}");
        if (dataElement.basePrompt) newRecord.GetDeleteButton().gameObject.SetActive(false);
        newRecord.GetDeleteButton().onClick.AddListener(()=> DeleteData(id));
        newRecord.GetLoadButton().onClick.AddListener(()=>UseData(id));
        
    }
}
