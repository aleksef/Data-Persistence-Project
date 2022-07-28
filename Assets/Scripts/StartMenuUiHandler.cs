using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

[DefaultExecutionOrder(1000)]
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

    public void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }
}
