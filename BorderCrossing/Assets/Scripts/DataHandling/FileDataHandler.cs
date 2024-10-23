using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileDataHandler
{
    private string _dataDirPath;
    private string _dataFileName;

    public FileDataHandler(string dataDirPath, string dataFileName)
    {
        _dataDirPath = dataDirPath;
        _dataFileName = dataFileName;
    }

    public PromptsData Load()
    {
        var fullPath = Path.Combine(_dataDirPath, _dataFileName);
        PromptsData data;
        if (!File.Exists(fullPath)) return null;
        try
        {
            var dataToLoad = "";
            using (var stream = new FileStream(fullPath, FileMode.Open))
            {
                using (var reader = new StreamReader(stream))
                {
                    dataToLoad = reader.ReadToEnd();
                }
            }

            data = JsonUtility.FromJson<PromptsData>(dataToLoad);
        }
        catch (Exception e)
        {
            Debug.Log($"Error occured while trying to save file on {fullPath} \n {e}");
            throw;
        }

        return data;
    }

    public void Save(PromptsData data)
    {
        var fullPath = Path.Combine(_dataDirPath, _dataFileName);
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            var dataToSave = JsonUtility.ToJson(data, true);

            using var stream = new FileStream(fullPath, FileMode.Create);
            using var writer = new StreamWriter(stream);
            writer.Write(dataToSave);
        }
        catch (Exception e)
        {
            Debug.Log($"Error occured while trying to save file on {fullPath} \n {e}");
            throw;
        }
    }
}
