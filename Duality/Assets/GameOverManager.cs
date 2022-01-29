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
        StartCoroutine(AsyncLoadScene("Game"));
    }

    public void LoadMenu()
    {
        StartCoroutine(AsyncLoadScene("MainMenu"));
    }

    IEnumerator AsyncLoadScene(string sceneName)
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        Debug.Log("=== Game loaded... ===");
    }
}
