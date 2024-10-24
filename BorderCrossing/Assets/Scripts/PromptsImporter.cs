using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PromptsImporter : MonoBehaviour, IDataPersistence
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TMP_Text chooseFileButtonName;
    [SerializeField] private UnityEvent<PromptsData.Prompts> onPromptCreated;
    private List<string> _dataToSave;
    private bool _save;
    public void LoadData(PromptsData data)
    {
        
    }

    public void SaveData(ref PromptsData data)
    {
        if (_save)
        {
            data.AddNewPrompts(inputField.text, _dataToSave);
            var newPrompts = data.promptList[^1];
            onPromptCreated?.Invoke(newPrompts);
        }
        _save = false;
    }

    public void GetCSVData(List<string> data)
    {
        var fileName = data[^1];
        chooseFileButtonName.text = fileName;
        data.RemoveAt(data.Count-1);
        _dataToSave = data;
    }

    public void SaveNewData()
    {
        _save = true;
    }
}
