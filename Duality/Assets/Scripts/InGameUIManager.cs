using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class InGameUIManager : MonoBehaviour
{
    public static InGameUIManager Instance { get; private set; }
    
    [SerializeField] private string menuSceneName = "MainMenu";
    [SerializeField] private string endCutsceneName = "End Cutscene";

    
    [SerializeField] private Text heightTextDisplay;
    [SerializeField] private Slider heightSliderDisplay;
    [SerializeField] private Slider energyBar;
    [SerializeField] private Text timer;

    [SerializeField] private MenuWindow pauseMenu;
    [SerializeField] private MenuWindow gameOverMenu;
    [SerializeField] private MenuWindow winScreen;
    private float maxHeight = -1f;

    private void OnEnable()
    {
        if(Instance != null)
            Debug.LogError($"Instance of {GetType().Name} duplicated!");
            
        Instance = this;
    }

    private void OnDisable()
    {
        if(Instance == this)
            Instance = null;
    }

    void Update()
    {
        UpdateTimer();
        UpdateHeightDisplay();
        UpdateEnergyBar();

        if (Input.GetKeyDown(KeyCode.Escape) && !GameManager.Instance.GameOver)
        {
            if (GameManager.Instance.Paused)
            {
                GameManager.Instance.UnpauseGame();
                pauseMenu.Hide();
            }
            else
            {
                GameManager.Instance.PauseGame();
                pauseMenu.Show();
            }
        }
    }

    

    void UpdateHeightDisplay()
    {
        if (PlayerController.Instance.transform.position.y > maxHeight)
        {
            maxHeight = PlayerController.Instance.transform.position.y;

            // maxHeight should not exceed tower height
            if (maxHeight > GameManager.Instance.gameEndHeight)
                maxHeight = GameManager.Instance.gameEndHeight;
        }
            

        heightTextDisplay.text = (Mathf.Round(maxHeight * 10) / 10f).ToString();
        heightSliderDisplay.value = maxHeight / GameManager.Instance.gameEndHeight;
    }

    void UpdateEnergyBar()
    {
        energyBar.maxValue = Mathf.RoundToInt( GameManager.Instance.maxEnergy / GameManager.Instance.actionEnergyCost);
        energyBar.value = Mathf.FloorToInt( GameManager.Instance.Energy / GameManager.Instance.actionEnergyCost);
    }

    void UpdateTimer()
    {
        if (!Pause.isPaused && !GameManager.Instance.Lost && !GameManager.Instance.Won)
        {
            timer.text = (Mathf.Round(GameManager.Instance.Timer * 10) / 10f).ToString();
        }
    }
    
    public void Restart()
    {
        var restartSceneName = SceneManager.GetActiveScene().name;
        StartCoroutine(AsyncLoadScene(restartSceneName));
    }

    public void LoadMenu()
    {
        StartCoroutine(AsyncLoadScene(menuSceneName));
    }

    public void LoadEndCutscene()
    {
        StartCoroutine(AsyncLoadScene(endCutsceneName));
    }

    public void OnWin()
    {
        winScreen.Show();
        energyBar.gameObject.SetActive(false);
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
