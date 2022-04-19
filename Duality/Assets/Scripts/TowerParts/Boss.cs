using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Boss : MonoBehaviour
{
    [SerializeField] private float verticalMovementSpeed = 5f;
    [SerializeField] private float horizontalMovementSpeed = 1f;
    [SerializeField] private float yOffset = 10f;

    private float targetPosX;
    private bool moving = true;
    
    [SerializeField] private AudioSource deathSoundAudioSource;
    [SerializeField] private GameObject dyingParticleSystem;
    [SerializeField] private GameObject deathParticleSystem;
    [SerializeField] private float delayToDie = 1.5f;

    private void FixedUpdate()
    {
        float targetHeight = yOffset + GameManager.Instance.ExpectedHeight;

        Vector3 targetPos = new Vector3(targetPosX, targetHeight, transform.position.z);

        float stepX = Time.fixedDeltaTime * horizontalMovementSpeed;
        float stepY = Time.fixedDeltaTime * verticalMovementSpeed;

        Vector3 newPos = transform.position;

        newPos.x = Vector3.MoveTowards(transform.position, targetPos, stepX).x;
        newPos.y = Vector3.MoveTowards(transform.position, targetPos, stepY).y;

        transform.position = newPos;

        if (moving)
        {
            if (Vector3.Distance(transform.position, targetPos) < 0.1f)
                targetPosX = LevelGenerator.GetRandomPosX();
        }
        else
        {
            targetPosX = 0;
        }
    }
    
    public virtual void Kill()
    {
        deathSoundAudioSource.gameObject.SetActive(true);
        dyingParticleSystem.SetActive(true);
        
        StopAllCoroutines();

        StartCoroutine(DelyDieCoroutine());
    }

    IEnumerator DelyDieCoroutine()
    {
        yield return new WaitForSeconds(delayToDie);

        GetComponent<SpriteRenderer>().enabled = false;
        deathParticleSystem.SetActive(true);
        dyingParticleSystem.SetActive(false);
    }

    /// <summary>
    /// Forces boss to move to the center of the screen;
    /// Used for making player attack look more epic uwu
    /// </summary>
    public void Center()
    {
        moving = false;
    }
}