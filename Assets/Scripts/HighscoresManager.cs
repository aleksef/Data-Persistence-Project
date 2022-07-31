using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Linq;

public class HighscoresManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI highscoresListText;

    private void Start()
    {
        if (GameManager.Instance == null)
        {
            highscoresListText.SetText("No game manager.<br> Play from start menu scene.");
        }
        else if (GameManager.Instance.highscores.Count == 0)
        {
            highscoresListText.SetText("No highscores yet.");
        }
        else if (GameManager.Instance.highscores.Count > 0)
        {
            DrawHighscoresList();
        }
    }

    public void GoToStartMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void DrawHighscoresList() 
    {
        string text = "";
        List<GameManager.Score> highscores = SortScores(GameManager.Instance.highscores);

        for (int i = 0; i < highscores.Count; i++)
        {
            string newLine = $"{i+1}. {highscores[i].username} = ";
            newLine += $"{highscores[i].value} <br>";
            if (i == 0) 
            {
                newLine = $"<b>{newLine}</b>";
            }
            text += newLine;
        }

        highscoresListText.SetText(text);
    }

    
    private List<GameManager.Score> SortScores(List<GameManager.Score> highscores)
    {
        List<GameManager.Score> sortedList = highscores.OrderByDescending(s => s.value).ToList();
        return sortedList;
    }
}
