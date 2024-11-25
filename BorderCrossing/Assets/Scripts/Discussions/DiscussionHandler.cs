using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class DiscussionHandler : MonoBehaviour, IDataPersistence
{
    [SerializeField] private GameObject group;
    [SerializeField] private PromptRecord promptRecord;

    private List<PromptRecord> _records = new();

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

    private void DeleteData(int id)
    {
        Destroy(_records[id].gameObject);
        _records.RemoveAt(id);
        DataPersistenceManager.Instance.DeleteDiscussionSave(id);
    }

    private void UseData(int id)
    {
        DataPersistenceManager.Instance.UseTheDiscussionSave(id);
    }

    public void CreateNewPromptRecord(PromptsData.Prompts prompt)
    {
        var newRecord = Instantiate(promptRecord, group.transform);
        newRecord.ChangeText(prompt.name);
        prompt.image = newRecord.GetColor();
        _records.Add(newRecord);
        var id = _records.IndexOf(newRecord);
        Debug.Log($"Is this prompt a basic one? {prompt.basePrompt}");
        if (prompt.basePrompt)
        {
            newRecord.GetDeleteButton().gameObject.SetActive(false);
        }
        newRecord.GetDeleteButton().onClick.AddListener(() => DeleteData(id));
        newRecord.GetLoadButton().onClick.AddListener(()=> UseData(id));
    }
}