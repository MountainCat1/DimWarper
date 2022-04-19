using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    [SerializeField] private float delayToSetDefaultSelected = 1.2f;
    [SerializeField] private GameObject defaultSelected;

    private void Start()
    {
        StartCoroutine(DelayToSetDefaultSelectedCoroutine());
    }

    public void LoadLevel(string levelName)
    {
        StopAllCoroutines();
        StartCoroutine(LoadYourAsyncScene(levelName));
    }

    public void LoadLevelWithCutscene(string cutsceneSceneName, string levelSceneName)
    {
        CutsceneTransition.StartTransition(cutsceneSceneName, levelSceneName);
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

    IEnumerator DelayToSetDefaultSelectedCoroutine()
    {
        yield return new WaitForSeconds(delayToSetDefaultSelected);
        EventSystem.current.SetSelectedGameObject(defaultSelected);
    }
}
