using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using SimpleFileBrowser;

public class CSVFileReader : MonoBehaviour
{
    [SerializeField] private UnityEvent<List<string>, string> onFileRead;

    // Call this function from a UI Button press
    public void LoadCSVFile()
    {
        FileBrowser.SetFilters(false, new FileBrowser.Filter("CSV Files", ".csv"));
        FileBrowser.ShowLoadDialog(
            (paths) =>  // OnSuccess callback
            {
                if (paths.Length > 0)
                {
                    string filePath = paths[0];
                    Debug.Log("Selected file: " + filePath);
                    var csvData = ReadCSV(filePath);
                    foreach (var line in csvData)
                    {
                        Debug.Log(line);
                    }
                    onFileRead?.Invoke(csvData, Path.GetFileName(filePath));
                }
            },
            () => Debug.Log("File selection cancelled"),  // OnCancel callback
            FileBrowser.PickMode.Files,  // PickMode (we want to select files)
            false,  // Allow multi-selection (set to false for one file)
            null,  // Default path (null to use system default)
            null,  // Default file name
            "Select CSV File",  // Title of the dialog
            "Select"  // Confirmation button text
        );
    }

    private List<string> ReadCSV(string filePath)
    {
    
        var data = new List<string>();
        try
        {
            using var reader = new StreamReader(filePath);
            while (reader.ReadLine() is { } line)
            {
                data.Add(line);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("Failed to read CSV: " + e.Message);
        }
        return data;
    
    }
}
