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
    private static string SceneToLoad { set; get; }

    [SerializeField] private string sceneToLoad;
    // Editor
    [SerializeField] private GameObject slideContainer;
    [SerializeField] private float timeToWaitBeforeLoadLevel;
    [SerializeField] private Animator fadeBlackScreenAnimator;
    [SerializeField] private Animator fadeMusicAnimator;

    private List<GameObject> slides;
    private int presentSlide = 0;

    private void Awake()
    {
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneToLoad = sceneToLoad;
        }
    }

    private void Start()
    {
        slides = new List<GameObject>();
        foreach (Transform slide in slideContainer.transform)
        {
            slides.Add(slide.gameObject);
        }
        ShowSlide(slides[0]);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel") || Input.GetButtonDown("Jump") || Input.GetButtonDown("Dimension Swap"))
        {
            if (presentSlide + 1 < slides.Count)
            {
                NextSlide();
            }
            else
            {
                GoToLevel();
            }
        }
    }

    public void NextSlide()
    {
        fadeBlackScreenAnimator.SetTrigger("fadeOnce");
        HideSlide(slides[presentSlide]);
        presentSlide++;
        ShowSlide(slides[presentSlide]);
    }

    private void ShowSlide(GameObject slide)
    {
        slide.SetActive(true);
    }

    private void HideSlide(GameObject slide)
    {
        slide.SetActive(false);
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
