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
        maxVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= (1f / fadeTime) * 0.1f;
            yield return new WaitForSeconds(0.1f);
        }

        audioSource.clip = newMusicClip;
        audioSource.Play();

        while (audioSource.volume < maxVolume)
        {
            audioSource.volume += (1f / fadeTime) * 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
