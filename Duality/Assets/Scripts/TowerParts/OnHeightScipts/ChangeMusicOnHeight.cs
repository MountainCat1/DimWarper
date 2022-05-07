using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMusicOnHeight : OnHeightBehaviour
{
    public AudioClip newMusicClip;
    public AudioSource audioSource;
    
    private float maxVolume;
    private Animator animator;

    private void Awake()
    {
        animator = audioSource.GetComponent<Animator>();
    }

    protected override void Action()
    {
        StartCoroutine(ChangeMusicCoroutine());
    }


    IEnumerator ChangeMusicCoroutine()
    {
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
