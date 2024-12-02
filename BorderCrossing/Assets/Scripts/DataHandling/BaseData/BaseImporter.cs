using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class BaseImporter : MonoBehaviour, IDataPersistence
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TMP_Text chooseFileButtonName;
    [SerializeField] private UnityEvent<GameData.DataElement> onPromptCreated;

    private List<string> _dataToSave;
    private bool _save;
    
    public void LoadData(GameData data)
    {
        
    }

    public void SaveData(ref GameData data)
    {
        if (_save)
        {
            if (inputField.text == "")
            {
                inputField.text = "New Prompt";
            }
            data.AddNewPrompts(inputField.text, _dataToSave);
            var newPrompts = data.Elements[^1];
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
