using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnePlatformFloor : Floor
{
    public Transform platform;


    private void Start()
    {
        float towerWidth = GameManager.Instance.towerWidth;

        float platformWidth = platform.GetComponent<BoxCollider2D>().size.x;

        float maxRadius = (towerWidth - platformWidth) / 2;
        float minRadius = (-towerWidth + platformWidth) / 2;

        float positionX = Random.Range(minRadius, maxRadius);

        platform.position = new Vector2(positionX, transform.position.y);
    }
}
