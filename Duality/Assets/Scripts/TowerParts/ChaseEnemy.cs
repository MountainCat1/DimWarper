using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseEnemy : Enemy
{
    public AudioClip hitAudioClip;
    public float movementSpeed = 5f;

    public bool chasing = true;

    private Vector3 lastDirection;

    private void FixedUpdate()
    {
        if (transform.position.y < PlayerController.Instance.transform.position.y)
        {
            chasing = false;
            if (lastDirection.magnitude < 0.001f)
            {
                lastDirection = Vector3.down;
            }
        }
            

        if (chasing)
        {
            Chase();
        }
        else
        {
            GoDown();
        }

    }

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

        lastDirection = goal - transform.position;

        transform.position = Vector3.MoveTowards(transform.position, goal, step);
    }

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
