using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[CreateAssetMenu(fileName = "gameSettings", menuName = "gamesettings SO")] 
public class Settings : ScriptableObject
{
    private string SavePath;
    public string language;
    public string COMPort;
    public string numberOfNodes;

    public bool autoSelect;

    public void OnEnable()
    {
        SavePath = Application.dataPath + "/Data/settings.json";
    }
    public void StoreToDisc()
    {
        Debug.Log("saving");
        string controlsAsText = JsonUtility.ToJson(this, true);
        File.WriteAllText(SavePath, controlsAsText);
    }

    public void RestoreFromDisc()
    {
        if(File.Exists(SavePath))
        {
            string textFromFile = File.ReadAllText(SavePath);
            JsonUtility.FromJsonOverwrite(textFromFile, this);
        }
    }

}
