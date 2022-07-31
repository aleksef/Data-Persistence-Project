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
        highscoresForSave.data = GameManager.Instance.highscores;
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
            Debug.Log("Saved scores loaded.");
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
            GameManager.Instance.highscores.Add(newScore);

            if (GameManager.Instance.highscores.Count > 10 && GameManager.Instance.highscores.Contains(lowestScore)) 
            { 
                GameManager.Instance.highscores.Remove(lowestScore);
            }
        }
    }

    public Score GetLowestScore()
    {
        Score lowestScore = new Score();
        lowestScore.username = "Uknown";
        lowestScore.value = 42069;

        if (GameManager.Instance.highscores.Count > 0)
        {
            foreach (Score score in GameManager.Instance.highscores)
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

    public Score GetHighestScore()
    {
        Score highestScore = new Score();
        highestScore.username = "Uknown";
        highestScore.value = 0;

        if (GameManager.Instance.highscores.Count > 0)
        {
            foreach (Score score in GameManager.Instance.highscores)
            {
                if (score.value > highestScore.value) { highestScore = score; }
            }
        }
        else
        {
            highestScore.value = 0;
        }
        return highestScore;
    }
}
