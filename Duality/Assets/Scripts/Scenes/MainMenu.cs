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
