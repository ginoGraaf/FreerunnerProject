using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public Text test;

    public void PlayGame()
    {
        SceneManager.LoadScene("Dashboard"); // Dashboard or Test
        PlayerPrefs.SetString("Difficulty", test.text);

        PlayerPrefs.Save();
    }
}
