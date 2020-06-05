using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultySelect : MonoBehaviour
{
    public Text test1;

    public void Awake()
    {
        string value = PlayerPrefs.GetString("Difficulty");

        test1.text = value;
    }
}
