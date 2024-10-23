using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataPersistence
{
    public void LoadData(PromptsData data);
    public void SaveData(ref PromptsData data);
}
