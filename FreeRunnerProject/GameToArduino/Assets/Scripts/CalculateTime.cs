using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalculateTime : MonoBehaviour
{
    public Text timerLabel;
    public Text scoreLabel;
    public Text First;
    public Text Second;
    public Text Thirth;
    public Text Fourth;
    public Text Fifth;

    private float time = 0;
    private int score = 1;
    private int bonus = 0;
    private bool start = false;

    int first = 0;
    int second = 0;
    int thirth = 0;
    int fourth = 0;
    int fifth = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(start)
        {
            time += Time.deltaTime;

            var minutes = time / 60; //Divide the guiTime by sixty to get the minutes.
            var seconds = time % 60;//Use the euclidean division for the seconds.
            var fraction = (time * 100) % 100;

            score += (int)((minutes + seconds + fraction + bonus) / 10);

            //update the label value
            timerLabel.text = string.Format("{0:00} : {1:00} : {2:000}", minutes, seconds, fraction);
            scoreLabel.text = score.ToString();
        }
        
    }

    public void StartButtonPressed()
    {
        start = true;
    }
    public void StopButtonPressed()
    {
        if(int.Parse(First.text) == 0 || score > int.Parse(First.text))
        {
            int oldScore = int.Parse(First.text);
            First.text = score.ToString();
            score = oldScore;
        }
        if(int.Parse(Second.text) == 0 || score > int.Parse(Second.text))
        {
            int oldScore = int.Parse(Second.text);
            Second.text = score.ToString();
            score = oldScore;
        }
        if (int.Parse(Thirth.text) == 0 || score > int.Parse(Thirth.text))
        {
            int oldScore = int.Parse(Thirth.text);
            Thirth.text = score.ToString();
            score = oldScore;
        }
        if (int.Parse(Fourth.text) == 0 || score > int.Parse(Fourth.text))
        {
            int oldScore = int.Parse(Fourth.text);
            Fourth.text = score.ToString();
            score = oldScore;
        }
        if (int.Parse(Fifth.text) == 0 || score > int.Parse(Fifth.text))
        {
            int oldScore = int.Parse(Fifth.text);
            Fifth.text = score.ToString();
            score = oldScore;
        }
        score = 0;
        start = false;
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
