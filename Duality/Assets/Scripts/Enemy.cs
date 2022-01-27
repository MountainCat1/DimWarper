using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public AudioClip hitAudioClip;
    public float randomWeight = 1f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == PlayerController.Instance.gameObject)
        {
            if (hitAudioClip != null)
                AudioSource.PlayClipAtPoint(hitAudioClip, transform.position);

            PlayerController.Instance.Kill();
        }
    }
}
