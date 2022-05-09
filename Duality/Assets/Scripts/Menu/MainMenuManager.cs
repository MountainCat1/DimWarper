using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject windowContainer;
    [SerializeField] private MenuWindow mainWindow;

    private MenuWindow activeMenuWindow;

    private void Awake()
    {
        ShowMenuWindow(mainWindow);
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowMainWindow();
        }

        if (Input.GetKey(KeyCode.H) && Input.GetKeyDown(KeyCode.J))
        {
            Cutscene cutscene = new Cutscene()
            {
                messages = new List<string>()
                {
                    "Ale ja",
                    "Bardzo ale to bardzo lubię koty",
                    "UwU UwU"
                }
            };

            CutsceneManager.LoadCutscene(cutscene, "MainMenu");
        }
    }

    public void ShowMainWindow()
    {
        ShowMenuWindow(mainWindow);
    }

    public void Campaign()
    {
        Debug.Log("=== Loading campain mode... ===");

        if (GameDataManager.Data.gameProgress == 0)
        {
            CutsceneTransition.StartTransition("Level 0 Intro", "Level 0");
            //StartCoroutine(LoadYourAsyncScene("Level 0"));
        }
        else
        {
            StartCoroutine(LoadYourAsyncScene("Campaign Map"));
        }
    }

    public void EndlessMode()
    {
        Debug.Log("=== Loading Endless Mode... ===");

        StartCoroutine(LoadYourAsyncScene("Level Endless"));
    }

    public void EasyMode()
    {
        Debug.Log("=== Loading easy mode... ===");

        CutsceneTransition.StartTransition("Level Intro", "Test Level");
    }

    public void HardMode()
    {
        Debug.Log("=== Loading hard mode... ===");

        StartCoroutine(LoadYourAsyncScene("Game"));
    }

    public void ShowMenuWindow(MenuWindow menuWindow)
    {
        if (activeMenuWindow != null)
            activeMenuWindow.Hide();

        menuWindow.Show();
        activeMenuWindow = menuWindow;
    }

    public void ResetProgress()
    {
        GameDataManager.ResetData();
    }

    public void GoToCredits()
    {
        Debug.Log("=== Loading credits... ===");

        StartCoroutine(LoadYourAsyncScene("Credits"));
    }

    public void Quit()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }

    /*IEnumerator LoadYourAsyncScene(string level)
    {
        ShowLoadingScreen();
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(level);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        Debug.Log("=== Game loaded... ===");
    }*/

    IEnumerator LoadYourAsyncScene(string level)
    {
        ShowLoadingScreen();
        return SceneLoader.InternalSceneLoad(level);
    }

    private void ShowLoadingScreen()
    {
        LoadingScreen.Show();
    }
}