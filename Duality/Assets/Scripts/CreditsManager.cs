using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreditsManager : MonoBehaviour
{
    public Transform end;
    public GameObject panel;

    public float speed = 1f;

    public float yOffset = 500f;

    private bool moved = false;

    private void Start()
    {
        Time.timeScale = 1f;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Return)) {
            StopAllCoroutines();
            StartCoroutine(AsyncLoadScene("MainMenu"));
        }
    }

    private void LateUpdate()
    {
        if (!moved && panel.GetComponent<RectTransform>().sizeDelta.y != 0)
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
            Vector3 newPos = panel.GetComponent<RectTransform>().anchoredPosition;
            newPos.y = -panel.GetComponent<RectTransform>().sizeDelta.y - yOffset;
            moved = true;
            panel.GetComponent<RectTransform>().anchoredPosition = newPos;
            StartCoroutine(CreditsRollCoroutine());
        }
    }

    IEnumerator CreditsRollCoroutine()
    {

        while (Vector2.Distance(panel.transform.position, end.position) > 0.1f)
        {
            float step = Time.deltaTime * speed;
            panel.transform.position = Vector2.MoveTowards(panel.transform.position, end.position, step);

            yield return new WaitForEndOfFrame();
        }
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
