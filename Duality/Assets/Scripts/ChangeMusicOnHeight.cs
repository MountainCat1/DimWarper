using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMusicOnHeight : OnHeightBehaviour
{
    public AudioClip newMusicClip;
    public AudioSource audioSource;

    protected override void Action()
    {
        audioSource.clip = newMusicClip;
        audioSource.Play();
    }

}
