using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SaveLoadSettings : MonoBehaviour
{
    public Settings settings;
    public Dropdown language;
    public Dropdown COMPort;
    public Dropdown numberOfNodes;
    public Toggle autoSelect;

    void Start()
    {
        settings.RestoreFromDisc();
        GetSettings();
    }

    private void OnDisable()
    {
        SaveSettings();
        settings.StoreToDisc();
    }

    public void SaveSettings()
    {
        settings.language = language.captionText.text;
        settings.COMPort = COMPort.captionText.text;
        settings.numberOfNodes = numberOfNodes.captionText.text;
        settings.autoSelect = autoSelect.isOn;
    }

    public void GetSettings()
    {
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

