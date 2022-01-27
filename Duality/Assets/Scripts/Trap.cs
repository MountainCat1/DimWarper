using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Trap : MonoBehaviour
{
    public float randomWeight = 1f;
    public abstract void OnFloorGenerated(Floor floor);
}
