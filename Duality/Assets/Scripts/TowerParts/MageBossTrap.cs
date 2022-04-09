using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageBossTrap : BossTrap
{
    public EvilMage bossPrefab;
    public Vector3 spawnOffset = new Vector3(0, 10, 0);

    protected override void SpawnBoss(Floor floor)
    {
        Instantiate(bossPrefab.gameObject, floor.transform.position + spawnOffset, Quaternion.identity);
    }
}
