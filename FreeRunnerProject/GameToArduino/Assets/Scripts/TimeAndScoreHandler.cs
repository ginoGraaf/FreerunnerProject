using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class TimeAndScoreHandler : MonoBehaviour
{
    public GameObject panel;
    public Text InputField;
    public Text timerLabel;
    public Text scoreLabel;
    public Text First;
    public Text Second;
    public Text Thirth;
    public Text Fourth;
    public Text Fifth;
    public Text FirstName;
    public Text SecondName;
    public Text ThirthName;
    public Text FourthName;
    public Text FifthName;

    private float time = 0;
    private float previousTime = 0;
    private int score = 1;
    private int bonus = 0;
    private int totalScore = 0;
    private bool start = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(start)
        {
            time += Time.deltaTime - previousTime;

            var minutes = Mathf.Floor(time / 60); //Divide the guiTime by sixty to get the minutes.
            var seconds = time % 60;//Use the euclidean division for the seconds.
            var fraction = (time * 100) % 100;

            score += (int)((minutes + seconds + fraction ) / 10);

            //update the label value
            timerLabel.text = string.Format("{0:00} : {1:00} : {2:000}", minutes, seconds, fraction);
            totalScore = score + bonus;
            scoreLabel.text = totalScore.ToString();
        }
        
    }

    public void StartButtonPressed()
    {
        start = true;
    }
    public void StopButtonPressed()
    {
        if(totalScore > int.Parse(Fifth.text))
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
        bool scoreAdded = false;
        string inputName = InputField.text;
        if (int.Parse(First.text) == 0 || totalScore > int.Parse(First.text))
        {
            //addScore();
            int oldScore = int.Parse(First.text);
            string oldName = FirstName.text;

            First.text = totalScore.ToString();
            FirstName.text = inputName;
            
            totalScore = oldScore;
            inputName = oldName;
        }
        if (int.Parse(Second.text) == 0 || totalScore > int.Parse(Second.text))
        {
            int oldScore = int.Parse(Second.text);
            string oldName = SecondName.text;

            Second.text = totalScore.ToString();
            SecondName.text = inputName;

            totalScore = oldScore;
            inputName = oldName;
        }
        if (int.Parse(Thirth.text) == 0 || totalScore > int.Parse(Thirth.text))
        {
            int oldScore = int.Parse(Thirth.text);
            string oldName = ThirthName.text;

            Thirth.text = totalScore.ToString();
            ThirthName.text = inputName;

            totalScore = oldScore;
            inputName = oldName;
        }
        if (int.Parse(Fourth.text) == 0 || totalScore > int.Parse(Fourth.text))
        {
            int oldScore = int.Parse(Fourth.text);
            string oldName = FourthName.text;

            Fourth.text = totalScore.ToString();
            FourthName.text = inputName;

            totalScore = oldScore;
            inputName = oldName;
        }
        if (int.Parse(Fifth.text) == 0 || totalScore > int.Parse(Fifth.text))
        {
            int oldScore = int.Parse(Fifth.text);
            string oldName = FifthName.text;

            Fifth.text = totalScore.ToString();
            FifthName.text = inputName;

            totalScore = oldScore;
            inputName = oldName;
        }
        //previousTime = Time.deltaTime;
        time = 0;
        score = 0;
        bonus = 0;
        totalScore = 0;

        panel.SetActive(false);
    }
}
