using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsManager : MonoBehaviour
{
    public void GoToStartMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ResetHighscores()
    {
        GameManager.Instance.highscores = new List<GameManager.Score>();
        GameManager.Instance.SaveHighscores();
        SceneManager.LoadScene(0);
    }
}
