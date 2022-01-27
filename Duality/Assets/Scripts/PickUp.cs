using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class PickUp : MonoBehaviour
{

    public AudioClip audioClip;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == PlayerController.Instance.gameObject)
        {
            if(audioClip != null)
                AudioSource.PlayClipAtPoint(audioClip, transform.position);

            OnPickUp();
            Destroy(gameObject);
        }

    }

    protected abstract void OnPickUp();
    
}
