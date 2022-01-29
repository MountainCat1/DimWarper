using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Lore : MonoBehaviour
{
    string lore = "What … what happened??? – you ask. You wake up alone in mysterious tower, in front of you there is a note and a strange amulet. The note:You have been chosen for my challenge. Complete it and you will become more powerful than you could ever imagine, fail and loose you sanity. Your goal is to get on top of my tower, but many challenges lay in front of you.The amulet I gave you allows you to change physical properties of the tower, which will help you complete the quest. Also, you can’t die. XOXO Evil Magician.";

    public float writeSpeed = 0.05f;
    public float waitingTime = 5f;

    public AudioSource beep;
    public Animator fade;

    void Start()
    {
        StartCoroutine(write());
    }

    IEnumerator write()
    {
        for (int i = 0; i < lore.Length; i++)
        {
            beep.PlayDelayed(.5f);
            GetComponent<Text>().text += lore[i];
            yield return new WaitForSeconds(writeSpeed);
        }
        yield return new WaitForSeconds(waitingTime);
        fade.SetBool("fade", true);
        SceneManager.LoadScene("Game");
    }


}
