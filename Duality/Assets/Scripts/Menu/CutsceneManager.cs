using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneManager : MonoBehaviour
{
    private const string CutsceneSceneName = "Cutscene";
    
    private static Cutscene presentCutscene;
    private static string nextSceneName;
    
    [SerializeField] private Text textDisplay;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private float typingSpeed;

    

    private IEnumerator slowTypingCoroutine;


    private void Start()
    {
        NextMessage();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            // Check if there is any more messages to display
            if (presentCutscene.messages.Count == 0)
            {
                SceneManager.LoadScene(nextSceneName);
            }
            else
            {
                NextMessage();
            }
        }
    }

    private IEnumerator SlowTypingCoroutine(string message)
    {
        foreach (var c in message)
        {
            textDisplay.text += c;
            yield return new WaitForSeconds(1f / typingSpeed);
        }
    }
    
    private void NextMessage()
    {
        textDisplay.text = "";
        
        // Stop previous coroutine (if exists)
        if(slowTypingCoroutine != null)
            StopCoroutine(slowTypingCoroutine);

        // Take message with index 0
        var message = presentCutscene.messages[0];
        presentCutscene.messages.RemoveAt(0);
        
        // Start new coroutine
        StartCoroutine(SlowTypingCoroutine(message));
    }

    public static void LoadCutscene(Cutscene cutscene, string nextScene)
    {
        SceneManager.LoadScene(CutsceneSceneName);
        presentCutscene = cutscene;
        nextSceneName = nextScene;
    }
}


public class Cutscene
{
    public List<string> messages = new List<string>();
}