using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject button;
    public GameObject button1;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            button.GetComponent<Button>().onClick.Invoke();
            button1.GetComponent<Button>().onClick.Invoke();
        }
    }


    public void EasyMode()
    {
        Debug.Log("=== Loading easy mode... ===");

        StartCoroutine(LoadYourAsyncScene("Game Easy"));
    }
    
    public void HardMode()
    {
        Debug.Log("=== Loading hard mode... ===");

        StartCoroutine(LoadYourAsyncScene("Game"));
    }

    public void Credits()
    {
        Debug.Log("=== Loading credits... ===");

        StartCoroutine(LoadYourAsyncScene("Credits"));
    }

    public void Quit()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }

    IEnumerator LoadYourAsyncScene(string level)
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(level);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        Debug.Log("=== Game loaded... ===");
    }

}
