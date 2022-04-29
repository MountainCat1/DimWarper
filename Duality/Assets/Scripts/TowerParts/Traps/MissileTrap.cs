using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileTrap : RandomTrap
{
    private const float ZPosition = 0f;
    
    [SerializeField] private Missile missilePrefab;
    [SerializeField] private float yOffset = 2f;

    
    
    public override void OnFloorGenerated(Floor floor)
    {
        Vector2 position = LevelGenerator.GetRandomPosX(floor, yOffset);
        var go = Instantiate(missilePrefab, (Vector3)position + new Vector3(0, 0, ZPosition), Quaternion.identity);

        Missile missile = go.GetComponent<Missile>();
        missile.Direction = Vector2.down;
    }
}
