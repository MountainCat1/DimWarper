using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    [SerializeField] private float delayToSetDefaultSelected = 1.2f;
    [SerializeField] private GameObject defaultSelected;


    private void Start()
    {
        Debug.Log("Setting time scale to 1");
        Time.timeScale = 1f;
        
        StartCoroutine(DelayToSetDefaultSelectedCoroutine());
    }

    public void LoadLevel(string levelName)
    {
        StopAllCoroutines();
        
        if (Application.CanStreamedLevelBeLoaded($"{levelName} Intro"))
        {
            levelName = $"{levelName} Intro";
        }
        
        LoadingScreen.Show();
        StartCoroutine(SceneLoader.InternalSceneLoad(levelName));
    }

    public void LoadLevelWithCutscene(string cutsceneSceneName, string levelSceneName)
    {
        CutsceneTransition.StartTransition(cutsceneSceneName, levelSceneName);
    }
    
    IEnumerator DelayToSetDefaultSelectedCoroutine()
    {
        yield return new WaitForSeconds(delayToSetDefaultSelected);
        EventSystem.current.SetSelectedGameObject(defaultSelected);
    }
}
