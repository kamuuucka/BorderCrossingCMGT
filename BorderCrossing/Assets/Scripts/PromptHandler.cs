using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateNewPrompts : MonoBehaviour, IDataPersistence
{
    [SerializeField] private StringData defaultPrompts;

    public void LoadData(PromptsData data)
    {
        Debug.Log($"This is the currently saved data: {data.promptList.Count}");
    }

    public void SaveData(ref PromptsData data)
    {
        data.AddNewPrompts(defaultPrompts.data);
        Debug.Log($"Data being saved: {data.promptList.Count}");
    }
}
