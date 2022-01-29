using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Restart();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            LoadMenu();
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
