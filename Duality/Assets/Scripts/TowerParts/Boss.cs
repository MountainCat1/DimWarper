using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Boss : MonoBehaviour
{
    public float deathHeight = 800f;

    public float verticalMovementSpeed = 5f;
    public float horizontalMovementSpeed = 1f;

    public float yOffset = 10f;

    private float targetPosX;
    

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

        if (Vector3.Distance(transform.position, targetPos) < 0.1f)
            targetPosX = LevelGenerator.GetRandomPosX();
    }

    private void Update()
    {
        if(GameManager.Instance.ExpectedHeight >= deathHeight)
        {
            Kill();
            targetPosX = 0f;
        }
    }

    private void Kill()
    {
        StopAllCoroutines();
    }
}