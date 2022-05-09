using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private string menuSceneName = "MainMenu";

    [SerializeField] private Text heightTextDisplay;
    [SerializeField] private Slider heightSliderDisplay;
    [SerializeField] private Slider energyBar;
    [SerializeField] private Text timer;

    [SerializeField] private MenuWindow pauseMenu;
    [SerializeField] private MenuWindow gameOverMenu;

    [SerializeField] private float towerHeight;
    private float maxHeight = -1f;

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
            maxHeight = PlayerController.Instance.transform.position.y;

        heightTextDisplay.text = (Mathf.Round(maxHeight * 10) / 10f).ToString();
        heightSliderDisplay.value = maxHeight / towerHeight;
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
