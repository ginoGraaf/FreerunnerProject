using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SaveLoadSettings : MonoBehaviour
{
    string path = "";
    [SerializeField] private Settings settings;
    public Dropdown language;
    public Dropdown COMPort;
    public Dropdown numberOfNodes;

    void Start()
    {
        settings = new Settings();
        path = Application.dataPath + "/Data/settings.json";
        GetSettings(path);
        Debug.Log(path);
    }

    private void OnDisable()
    {
        SaveSettings(path);
    }

    public void SaveSettings(string path)
    {
        settings.language = language.captionText.text;
        settings.COMPort = COMPort.captionText.text;
        settings.numberOfNodes = numberOfNodes.captionText.text;

        string settingsToSave = JsonUtility.ToJson(settings);
        File.WriteAllText(path, settingsToSave);
    }

    public void GetSettings(string path)
    {
        if (File.Exists(path))
        {
            string textFromFile = File.ReadAllText(path);
            JsonUtility.FromJsonOverwrite(textFromFile, settings);

            language.value = language.options.FindIndex(option => option.text == settings.language);
            COMPort.value = COMPort.options.FindIndex(option => option.text == settings.COMPort);
            numberOfNodes.value = numberOfNodes.options.FindIndex(option => option.text == settings.numberOfNodes);

            language.RefreshShownValue();
            COMPort.RefreshShownValue();
            numberOfNodes.RefreshShownValue();
        }
    }

    [System.Serializable]
    public class Settings
    {
        public string language;
        public string COMPort;
        public string numberOfNodes;
    }
}

