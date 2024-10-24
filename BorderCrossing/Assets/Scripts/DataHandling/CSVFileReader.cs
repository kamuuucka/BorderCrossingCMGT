using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
#if UNITY_EDITOR
using UnityEditor;  // For using Unity's built-in file picker in the Editor.
#else
#if UNITY_STANDALONE_OSX
using System.Runtime.InteropServices;  // For macOS standalone native dialogs.
#endif
#endif

public class CSVFileReader : MonoBehaviour
{
    [SerializeField] private List<string> csvData;
    [SerializeField] private UnityEvent<List<string>> onFileRead;

    // Call this function from a UI Button press
    public void LoadCSVFile()
    {
        var filePath = OpenFileBrowser();
        var fileName = "";
        if (!string.IsNullOrEmpty(filePath))
        {
            csvData = ReadCSV(filePath);
            fileName = Path.GetFileName(filePath);
            csvData.Add(fileName);
        }
        onFileRead?.Invoke(csvData);
    }

    private string OpenFileBrowser()
    {
        string path = string.Empty;

#if UNITY_EDITOR
        // Unity Editor file picker
        path = EditorUtility.OpenFilePanel("Select CSV File", "", "csv");
#else
#if UNITY_STANDALONE_WIN
        // Windows standalone file dialog
        path = OpenFileBrowserWindows();
#elif UNITY_STANDALONE_OSX
        // macOS native file dialog
        path = OpenFileBrowserMac();
#endif
#endif
        return path;
    }

#if UNITY_STANDALONE_WIN
    private string OpenFileBrowserWindows()
    {
        System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog
        {
            InitialDirectory = @"C:\", // Set this to a desired default path
            Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*",
            FilterIndex = 1,
            RestoreDirectory = true
        };

        if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        {
            return openFileDialog.FileName;
        }

        return string.Empty;
    }
#endif

#if UNITY_STANDALONE_OSX
    [DllImport("CocoaDialog")]
    private static extern string OpenFilePanelMac();

    private string OpenFileBrowserMac()
    {
        return OpenFilePanelMac();
    }
#endif

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
            Debug.LogError("Failed to read CSV file: " + e.Message);
        }

        return data;
    }
}
