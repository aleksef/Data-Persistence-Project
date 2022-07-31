using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public List<Score> highscores;
    public string username = "unknown user";

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadHighscores();
    }

    [System.Serializable]
    public class Score
    {
        public string username;
        public int value;
    }

    [System.Serializable]
    public class Highscores
    {
        public List<Score> data;
    }

    public void SaveHighscores()
    {
        Highscores highscoresForSave = new Highscores();
        highscoresForSave.data = highscores;

        string json = JsonUtility.ToJson(highscoresForSave);

        File.WriteAllText(Application.persistentDataPath + "/savefile_highscores.json", json);
    }

    public void LoadHighscores()
    {
        string path = Application.persistentDataPath + "/savefile_highscores.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            Highscores highscoresFromFile = JsonUtility.FromJson<Highscores>(json);

            highscores = highscoresFromFile.data;
        }
        else 
        {
            Debug.Log("Save file doesn't exist.");
            highscores = new List<Score>();
        }
    }

    public void AddNewScore(int newValue) 
    {
        Score lowestScore = GetLowestScore();
        if (newValue > lowestScore.value)
        {
            Score newScore = new Score();
            newScore.value = newValue;
            newScore.username = username;
            highscores.Add(newScore);

            if (highscores.Count > 10 && highscores.Contains(lowestScore)) 
            { 
                highscores.Remove(lowestScore);
            }
        }
    }

    public Score GetLowestScore()
    {
        Score lowestScore = new Score();
        lowestScore.username = "Uknown";
        lowestScore.value = 42069;

        if (highscores.Count > 0)
        {
            foreach (Score score in highscores)
            {
                if (score.value < lowestScore.value) { lowestScore = score; }
            }
        }
        else 
        {
            lowestScore.value = 0;
        }
        return lowestScore;
    }

    public Score GetScoreToBeat(int currentVal)
    {
        ////// WRONG
        Score lowestTobeat = new Score{value = 0};

        if (highscores.Count > 0)
        {
            foreach (Score score in highscores)
            {
                if (currentVal < score.value && score.value > lowestTobeat.value)
                {
                    lowestTobeat = score;
                }
            }
        }
        if (currentVal > lowestTobeat.value)
        {
            lowestTobeat.username = username;
            lowestTobeat.value = currentVal;
        }

        return lowestTobeat;
    }
 }
