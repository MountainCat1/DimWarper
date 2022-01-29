using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{

    [SerializeField] private string sceneName = "Game";

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && !Pause.isPaused)
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
        StartCoroutine(AsyncLoadScene(sceneName));
    }

    public void LoadMenu()
    {
        StartCoroutine(AsyncLoadScene("MainMenu"));
    }

    IEnumerator AsyncLoadScene(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        Debug.Log("=== Game loaded... ===");
    }
}
