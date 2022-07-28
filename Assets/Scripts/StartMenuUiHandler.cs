using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenuUiHandler : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI usernameInputText;
    [SerializeField] Button startButton;

    public void StartNewGame()
    {
        if (usernameInputText.text.Length > 1)
        {
            GameManager.Instance.username = usernameInputText.text;
        }
        SceneManager.LoadScene(1);
    }
}
