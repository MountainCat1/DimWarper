using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// This script is supposed to be used in cutscenes before loading a level.
/// This script displays a message to a player, and then loads a level assigned to a SceneToLoad variable;
/// </summary>
public class CutsceneTransition : MonoBehaviour
{
    static string SceneToLoad { set; get; }
    
    // Editor
    [SerializeField] private Text text;
    [SerializeField] private float timeToWaitBeforeLoadLevel;
    [SerializeField] private Animator fadeBlackScreenAnimator;
    [SerializeField] private Animator fadeMusicAnimator;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Escape))
        {
            GoToLevel();
        }
    }

    public void GoToLevel()
    {
        StartCoroutine(WaitToLoadLevelCoroutine());
    }
    
    private IEnumerator WaitToLoadLevelCoroutine()
    {
        fadeBlackScreenAnimator.SetBool("fade", true);
        fadeMusicAnimator.SetBool("fade", true);
        
        yield return new WaitForSeconds(timeToWaitBeforeLoadLevel);

        if(SceneToLoad == "")
            Debug.LogError("SceneToLoad property is empty! Did you try to start Cutscene scene directly?" +
                           "It should be started from StartTransition static method instead!");
        
        SceneManager.LoadSceneAsync(SceneToLoad);
        SceneToLoad = "";
    }

    public static void StartTransition(string cutsceneSceneName, string levelSceneName)
    {
        SceneToLoad = levelSceneName;
        SceneManager.LoadSceneAsync(cutsceneSceneName);
    }
}
