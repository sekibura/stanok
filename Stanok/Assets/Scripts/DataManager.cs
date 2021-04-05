using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;
using SimpleFileBrowser;

public class DataManager : MonoBehaviour
{

    [SerializeField]
    private InputManager _inputManager;
    [SerializeField]
    private OutputManager _outputManager;
    
    private void Start()
    {
        FileBrowser.SetFilters(true, new FileBrowser.Filter("Text Files", ".txt"));
        FileBrowser.SetDefaultFilter(".txt");
    }
    public void Save(InputValues data)
    {
        _outputManager.SetBrowserMode(true);
        string _json = JsonUtility.ToJson(data);
        FileBrowser.ShowSaveDialog((paths) =>
        {
            string targetPath = paths[0];
            WriteToFile(targetPath, _json);
            _outputManager.SetBrowserMode(false);
        }, ()=>{ _outputManager.SetBrowserMode(false); }, FileBrowser.PickMode.Files, false, "", "", "Save settings", "Save");
        

    }

    private void WriteToFile(string path, string json)
    {
        if (!string.IsNullOrEmpty(path))
        {
            FileStream fileStream = new FileStream(path, FileMode.Create);

            using (StreamWriter writer = new StreamWriter(fileStream))
            {
                writer.Write(json);
            }
        }
        else
        {
            Debug.LogWarning("Empty path");
        }

    }

    private string ReadFromFile(string path)
    {
        if (File.Exists(path))
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string json = reader.ReadToEnd();
                return json;
            }
        }
        else
            Debug.LogWarning("FileNotExist!");
        return "";
    }

    public void Load()
    {
        _outputManager.SetBrowserMode(true);
         FileBrowser.ShowLoadDialog((paths) =>
        {
            InputValues _data = new InputValues();
            string targetPath = paths[0];
            string _json = ReadFromFile(targetPath);
            if (!string.IsNullOrEmpty(_json))
                JsonUtility.FromJsonOverwrite(_json, _data);
            _inputManager.SetInputFieldsValues(_data);
            _outputManager.SetBrowserMode(false);

        }, () => { _outputManager.SetBrowserMode(false); }, FileBrowser.PickMode.Files, false, "", "", "Load", "Select");
    }


  
}
