using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseEnemy : Enemy
{
    [SerializeField] private ParticleSystem hitParticles; 
    [SerializeField] private AudioClip hitAudioClip;
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float offsetToGiveUp = 1.5f;

    public bool chasing = true;

    private Vector3 lastDirection;

    private void FixedUpdate()
    {
        if (chasing)
        {
            Chase();
        }
        else
        {
            GoDown();
        }
    }

    /// <summary>
    /// Move enemy along lastDirection, this method is used then enemy _gave up_,
    /// and now is seeking to leave the view and de-spawn
    /// </summary>
    private void GoDown()
    {
        float step = Time.fixedDeltaTime * movementSpeed;

        Vector3 goal = transform.position + lastDirection.normalized;
        goal = new Vector3(goal.x, goal.y, transform.position.z);

        transform.position = Vector3.MoveTowards(transform.position, goal, step);
    }

    private void Chase()
    {
        float step = Time.fixedDeltaTime * movementSpeed;

        Vector3 goal = PlayerController.Instance.transform.position;
        goal = new Vector3(goal.x, goal.y, transform.position.z);


        // Set last direction only when its not Vector.Zero
        if ((goal - transform.position).magnitude > 0)
        {
            lastDirection = goal - transform.position;
        }

        // If enemy passed the player, it should not follow them anymore
        if (transform.position.y - offsetToGiveUp < PlayerController.Instance.transform.position.y)
        {
            chasing = false;
            FixedUpdate();
        }
        
        // If enemy is *inside* the player character, the enemy should give up
        if (Vector2.Distance(transform.position, goal) < 0.001f)
        {
            goal.y = -Mathf.Abs(goal.y);
            
            chasing = false;
            GoDown();
            return;
        }
        
        // Move enemy toward player character
        transform.position = Vector3.MoveTowards(transform.position, goal, step);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == PlayerController.Instance.gameObject)
        {
            if (hitAudioClip != null)
                AudioSource.PlayClipAtPoint(hitAudioClip, transform.position);
            if (hitParticles != null)
            {
                hitParticles.gameObject.SetActive(true);
                hitParticles.Play();
            }
            
            PlayerController.Instance.Kill();
        }
    }
}
