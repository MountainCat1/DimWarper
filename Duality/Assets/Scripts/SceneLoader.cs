using System;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    public static string ActiveScene { get; private set; }
    public static event Action LoadingScreenStartUnloading;
    
    public static IEnumerator InternalSceneLoad(string sceneName)
    {
        var operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
      
      while (!operation.isDone)
      {
          yield return null;
      }
      
      ActiveScene = sceneName;
      
      LoadingScreenStartUnloading?.Invoke();

      yield return null;
    }
}