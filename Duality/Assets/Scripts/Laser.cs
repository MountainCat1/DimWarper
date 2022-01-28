using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MultipleAnimationAnimator))]
public class Laser : MonoBehaviour
{

    private MultipleAnimationAnimator animator;

    public float timeToShot = 3f;
    public float staysFired = 2f;

    public string shadowAnimation;
    public string laserAnimation;

    public AudioSource audioSource;

    public AudioClip laserCharging;
    public AudioClip laserFire;


    public bool fired = false;

    private void Awake()
    {
        animator = GetComponent<MultipleAnimationAnimator>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (fired)
        {
            if (collision.gameObject == PlayerController.Instance.gameObject)
            {
                PlayerController.Instance.Kill();
            }
        }
    }

    private void Start()
    {
        animator.Play(shadowAnimation);

        StartCoroutine(LaserCharchingCoroutine());
    }

    private IEnumerator LaserCharchingCoroutine()
    {
        audioSource.clip = laserCharging;
        audioSource.Play();

        yield return new WaitForSeconds(timeToShot);

        animator.ForcePlay(laserAnimation);
        fired = true;
        audioSource.clip = laserFire;
        audioSource.Play();

        yield return new WaitForSeconds(staysFired);
        Destroy(gameObject);
    }
}
