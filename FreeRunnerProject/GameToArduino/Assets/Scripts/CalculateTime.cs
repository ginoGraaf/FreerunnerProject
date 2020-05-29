using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalculateTime : MonoBehaviour
{
    public Text timerLabel;
    public Text scoreLabel;

    private float time = 0;
    private int score = 1;
    private int bonus = 0;
    private bool start = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(start)
        time += Time.deltaTime;

        var minutes = time / 60; //Divide the guiTime by sixty to get the minutes.
        var seconds = time % 60;//Use the euclidean division for the seconds.
        var fraction = (time * 100) % 100;

        score += (int)((minutes + seconds + fraction + bonus) / 10);

        //update the label value
        timerLabel.text = string.Format("{0:00} : {1:00} : {2:000}", minutes, seconds, fraction);
        scoreLabel.text = score.ToString();
    }

    public void StartButtonPressed()
    {
        start = true;
    }

    public void GoodButtonPressed()
    {
        bonus += 50;
    }

    public void AmazingButtonPressed()
    {
        bonus += 10000;
    }
}
