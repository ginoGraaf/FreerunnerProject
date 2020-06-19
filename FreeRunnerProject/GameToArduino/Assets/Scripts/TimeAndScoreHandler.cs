using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class TimeAndScoreHandler : MonoBehaviour
{
    public GameObject panel;
    public Text InputField;
    public Text timerLabel;
    public Text scoreLabel;
    public Text Difficulty;

    public Text[] Scores = new Text[5];
    public Text[] Names = new Text[5];

    private float time = 0;
    private float previousTime = 0;
    private int score = 1;
    private int bonus = 0;
    private int totalScore = 0;
    private bool start = false;
    private int difficultyDivider = 0;

    void Update()
    {
        if(start)
        {
            time += Time.deltaTime - previousTime;

            var minutes = Mathf.Floor(time / 60);
            var seconds = time % 60;
            var fraction = (time * 100) % 100;

            score += (int)((minutes + seconds + fraction ) / difficultyDivider);

            timerLabel.text = string.Format("{0:00} : {1:00} : {2:000}", minutes, seconds, fraction);
            totalScore = score + bonus;
            scoreLabel.text = totalScore.ToString();
        }
        
    }

    public void StartButtonPressed()
    {
        if (Difficulty.text == "Normal")
        {
            difficultyDivider = 10;
        }
        else if (Difficulty.text == "Hard")
        {
            difficultyDivider = 5;
        }
        else if (Difficulty.text == "Very Hard")
        {
            difficultyDivider = 1;
        }

        time = 0;
        score = 0;
        bonus = 0;
        totalScore = 0;

        timerLabel.text = string.Format("{0:00} : {0:00} : {0:000}", 0, 0, 0);
        scoreLabel.text = totalScore.ToString();
        start = true;

    }
    public void StopButtonPressed()
    {
        if(totalScore > int.Parse(Scores[4].text))
        {
            addScore();
        }
        start = false;
    }

    public void GoodButtonPressed()
    {
        bonus += 250;
    }

    public void AmazingButtonPressed()
    {
        bonus += 500;
    }

    private void addScore()
    {
        panel.SetActive(true);
    }

    public void enterName()
    {
        string inputName = InputField.text;
        for (int i = 0; i < Scores.Length; i++)
        {
            if (int.Parse(Scores[i].text) == 0 || totalScore > int.Parse(Scores[i].text))
            {
                int oldScore = int.Parse(Scores[i].text);
                string oldName = Names[i].text;

                Scores[i].text = totalScore.ToString();
                Names[i].text = inputName;

                totalScore = oldScore;
                inputName = oldName;
            }
        }

        time = 0;
        score = 0;
        bonus = 0;
        totalScore = 0;

        panel.SetActive(false);
    }
}
