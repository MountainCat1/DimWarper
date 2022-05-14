using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class BossTrap : OneTimeTrap
{
    [SerializeField] private Boss bossPrefab;
    [SerializeField] private Vector3 spawnOffset = new Vector3(0, 10f, 0);
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
