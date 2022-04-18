using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileTrap : RandomTrap
{
    public Missile missilePrefab;

    public float yOffset = 2f;

    public override void OnFloorGenerated(Floor floor)
    {
        Vector2 position = LevelGenerator.GetRandomPosX(floor, yOffset);
        var go = Instantiate(missilePrefab, position, Quaternion.identity);

        Missile missile = go.GetComponent<Missile>();
        missile.Direction = Vector2.down;
    }
}
