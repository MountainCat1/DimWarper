using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMusicOnHeight : OnHeightBehaviour
{
    public AudioClip newMusicClip;
    public AudioSource audioSource;

    public float fadeTime = 1f;

    private float maxVolume;

    protected override void Action()
    {
        StartCoroutine(ChangeMusicCoroutine());
    }


    IEnumerator ChangeMusicCoroutine()
    {
        var animator = audioSource.GetComponent<Animator>();
        animator.SetBool("fade", true);

        while (audioSource.volume > 0.05f)
        {
            yield return new WaitForSecondsRealtime(0f);
        }
        
        audioSource.clip = newMusicClip;
        audioSource.Play();
        animator.SetBool("fade", false);
    }
}
