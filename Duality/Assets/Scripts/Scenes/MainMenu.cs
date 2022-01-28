using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        Debug.Log("Launching game...");
        SceneManager.LoadScene("Game");
    }

    public void Quit()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
    

}
