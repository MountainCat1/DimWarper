using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneMovingPlatformFloor : Floor
{
    public Transform platform;

    public float speed = 1f;

    private bool direction = false;

    private void Start()
    {
        float towerWidth = GameManager.Instance.towerWidth;

        float platformWidth = platform.GetComponent<BoxCollider2D>().size.x;

        float maxRadius = (towerWidth - platformWidth) / 2;
        float minRadius = (-towerWidth + platformWidth) / 2;

        float positionX = Random.Range(minRadius, maxRadius);

        platform.position = new Vector2(positionX, transform.position.y);
    }

    private void Update()
    {
        float step = Time.deltaTime * speed;

        Vector2 goal;

        float towerWidth = GameManager.Instance.towerWidth;
        float platformWidth = platform.GetComponent<BoxCollider2D>().size.x;

        if (direction)
            goal = new Vector2((towerWidth - platformWidth) / 2, transform.position.y);
        else
            goal = new Vector2((-towerWidth + platformWidth) / 2, transform.position.y);

        if (Vector2.Distance(platform.position, goal) == 0f)
            direction = !direction;

        platform.transform.position = Vector2.MoveTowards(platform.transform.position, goal, step);

        if(PlayerController.Instance.Floor == platform.transform)
        {
            Vector2 pos = PlayerController.Instance.transform.position;
            if (direction)
                pos.x += step;
            else
                pos.x -= step;

            PlayerController.Instance.transform.position = pos;
        }
    }
}
