using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeDifficulty : MonoBehaviour
{
    public Text ReadDifficulty;
    public Text WriteDifficulty;

    public void ChangeDifficultyInGame()
    {
        PlayerPrefs.SetString("Difficulty", ReadDifficulty.text);

        WriteDifficulty.text = ReadDifficulty.text;

        PlayerPrefs.Save();
    }
}
