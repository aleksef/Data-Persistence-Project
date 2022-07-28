using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public string highscoreUsername;
    public int highscore = 0;
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

        LoadHighscore();
    }

    [System.Serializable]
    class SaveData
    {
        public string username;
        public int score;
    }

    public void SaveHighscore()
    {
        SaveData data = new SaveData();
        data.username = highscoreUsername;
        data.score = highscore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadHighscore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            highscoreUsername = data.username;
            highscore = data.score;
        }
        else 
        {
            highscoreUsername = "No highscore yet...";
        }
    }
}
