using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseEnemy : Enemy
{
    public float movementSpeed = 5f;

    public bool chasing = true;

    private Vector3 lastDirection;

    private void FixedUpdate()
    {
        if (transform.position.y < PlayerController.Instance.transform.position.y)
            chasing = false;

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

        Vector3 goal = transform.position + lastDirection;
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
}
