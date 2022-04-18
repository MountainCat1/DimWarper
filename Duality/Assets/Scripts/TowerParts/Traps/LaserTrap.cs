using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTrap : RandomTrap
{
    public Laser laserPrefab;

    public float laserRange = 6f;
    public float yOffset = 3f;

    public override void OnFloorGenerated(Floor floor)
    {
        Instantiate(laserPrefab.gameObject, GetRandomPos(), Quaternion.identity);
    }

    private Vector2 GetRandomPos()
    {
        float playerHeight = PlayerController.Instance.transform.position.y;

        return new Vector2(0f, Mathf.Lerp(playerHeight - laserRange + yOffset, playerHeight - laserRange + yOffset, Random.Range(0f, 1f)));

    }
}
