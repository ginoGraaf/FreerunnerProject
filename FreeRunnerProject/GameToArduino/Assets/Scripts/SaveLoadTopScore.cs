using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SaveLoadTopScore : MonoBehaviour
{
    string path;

    public TimeAndScoreHandler timeAndScoreHandler;
    [SerializeField] private TopScore topScore;

    void Start()
    {
        path = Application.dataPath + "/Data/TopScore.json";
        topScore = new TopScore();
        topScore.scores = new Score[5];
        GetScores(path);
    }

    private void OnDisable()
    {
        SaveScores(path);
    }

    public void SaveScores(string path)
    {
        for (int i = 0; i < timeAndScoreHandler.Scores.Length; i++)
        {
            topScore.scores[i].score = timeAndScoreHandler.Scores[i].text;
            topScore.scores[i].name = timeAndScoreHandler.Names[i].text;
        }

        string scoresToSave = JsonUtility.ToJson(topScore);
        File.WriteAllText(path, scoresToSave);
    }

    public void GetScores(string path)
    {
        if (File.Exists(path))
        {
            string textFromFile = File.ReadAllText(path);
            JsonUtility.FromJsonOverwrite(textFromFile, topScore);

            for (int i = 0; i < topScore.scores.Length; i++)
            {
                timeAndScoreHandler.Scores[i].text = topScore.scores[i].score;
                timeAndScoreHandler.Names[i].text = topScore.scores[i].name;
            }
        }
    }

    [System.Serializable]
    public class TopScore
    {
        public Score[] scores;
    }

    [System.Serializable]
    public class Score
    {
        public string score;
        public string name;
    }
}
