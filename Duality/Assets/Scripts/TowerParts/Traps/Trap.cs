using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using UnityEngine;

public abstract class RandomTrap : Trap
{
    public float randomWeight = 1f;
}
public abstract class OneTimeTrap : Trap
{
    public float height = -1;
    public bool used = false;
}
public abstract class Trap : MonoBehaviour
{
    public abstract void OnFloorGenerated(Floor floor);
}


