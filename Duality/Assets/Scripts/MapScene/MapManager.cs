using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    
    public void LoadLevel(string levelName)
    {
        StopAllCoroutines();
        StartCoroutine(LoadYourAsyncScene(levelName));
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
