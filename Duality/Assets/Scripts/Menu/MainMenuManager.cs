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
    [SerializeField] private bool disableDeselecting;
    private GameObject lastSelected;
    
    [SerializeField] private GameObject windowContainer;
    [SerializeField] private MenuWindow mainWindow;

    private MenuWindow activeMenuWindow;
    
    private void Awake()
    {
        ShowMenuWindow(mainWindow);
    }

    private void Update()
    {
        if (disableDeselecting)
            CancelBackgroundDeselectionClick();
        
        lastSelected = EventSystem.current.currentSelectedGameObject;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowMenuWindow(mainWindow);
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
    
    public void CancelBackgroundDeselectionClick()
    {
        if (lastSelected != null && EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(lastSelected);
        }
    }

    public void ShowMenuWindow(MenuWindow menuWindow)
    {
        if(activeMenuWindow != null)
            activeMenuWindow.Hide();
        
        menuWindow.Show();
        activeMenuWindow = menuWindow;
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
    
    IEnumerator LoadYourAsyncScene(string level)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(level);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        Debug.Log("=== Game loaded... ===");
    }
}
