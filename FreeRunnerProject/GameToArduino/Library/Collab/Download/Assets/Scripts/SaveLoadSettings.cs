using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SaveLoadSettings : MonoBehaviour
{
    string path;
    [SerializeField] private Settings settings;
    public Dropdown language;
    public Dropdown COMPort;
    public Dropdown numberOfNodes;
    public Toggle autoSelect;

    void Start()
    {
        path = Application.dataPath + "/Data/settings.json";
        settings = new Settings();
        GetSettings(path);
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
        settings.autoSelect = autoSelect.isOn;

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
            autoSelect.isOn = settings.autoSelect;

            language.RefreshShownValue();
            COMPort.RefreshShownValue();
            numberOfNodes.RefreshShownValue();
            if (autoSelect.isOn)
            {
                COMPort.interactable = false;
            }
        }
    }


    [System.Serializable]
    public class Settings
    {
        public string language;
        public string COMPort;
        public string numberOfNodes;
        public bool autoSelect;
    }
}

