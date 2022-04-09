using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyPickup : PickUp
{
    public float energyAmount = 25f;
    protected override void OnPickUp()
    {
        GameManager.Instance.Energy += energyAmount;
    }
}
