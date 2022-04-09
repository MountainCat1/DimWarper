using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossTrap : Trap
{

    public override void OnFloorGenerated(Floor floor)
    {
        randomWeight = 0;
        SpawnBoss(floor);
    }

    protected abstract void SpawnBoss(Floor floor);
}
