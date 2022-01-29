using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Lore : MonoBehaviour
{
    string lore = "What … what happened??? – you ask. You wake up alone in mysterious tower, in front of you there is a note and a strange amulet. The note:You have been chosen for my challenge. Complete it and you will become more powerful than you could ever imagine, fail and loose you sanity. Your goal is to get on top of my tower, but many challenges lay in front of you.The amulet I gave you allows you to change physical properties of the tower, which will help you complete the quest. Also, you can’t die. XOXO Evil Magician.";

    public float writeSpeed = 0.05f;
    public float waitingTime = 5f;

    public AudioSource beep;
    public Animator fade;

    private Text display;

    private string displayedText = "";

    private void Awake()
    {
        display = gameObject.GetComponent<Text>();
    }

    void Start()
    {
        StartCoroutine(WriteCoroutine());
    }



    IEnumerator WriteCoroutine()
    {
        for (int i = 0; i < lore.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                StartCoroutine(AsyncLoadScene("MainMenu"));
            }

            if(lore[i] != ' ')
                PlayBeep();

            displayedText += lore[i];

            display.text = AddSpacesForNextWord(displayedText, lore);

            yield return new WaitForSeconds(1f / writeSpeed);
        }
        yield return new WaitForSeconds(waitingTime);
        fade.SetBool("fade", true);

        StartCoroutine(AsyncLoadScene("MainMenu"));
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

    string AddSpacesForNextWord(string displayed, string toDisplay)
    {
        int spaces = 0;

        for (int i = displayed.Length; i < toDisplay.Length; i++)
        {
            if(toDisplay[i] != ' ')
            {
                spaces++;
            }
            else
            {
                break;
            }
            
        }
        displayed += "<color=black>";

        for (int i = 0; i < spaces; i++)
        {
            displayed += "_";
        }
        //<color=yellow>RICH</color> 
        displayed += "</color>";

        return displayed;
    }

    private void PlayBeep()
    {
        //beep.Stop();
        beep.Play();
    }

}
