using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrap : OneTimeTrap
{
    public Boss bossPrefab;

    public Vector3 spawnOffset = new Vector3(0, 10, 0);

    public Boss SpawnedBoss { get; private set; }

    public override void OnFloorGenerated(Floor floor)
    {
        SpawnBoss(floor);
    }

    private void SpawnBoss(Floor floor)
    {
        var go = Instantiate(
            bossPrefab.gameObject, 
            floor.transform.position + spawnOffset, 
            Quaternion.identity);
        
        SpawnedBoss = go.GetComponent<Boss>();
    }
}
