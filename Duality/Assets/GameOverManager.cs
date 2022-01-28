using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public Button restartButton;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            restartButton.onClick.Invoke();
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene("Game");
    }
}
