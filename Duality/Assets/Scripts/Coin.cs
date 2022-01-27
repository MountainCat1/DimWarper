using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : PickUp
{
    protected override void OnPickUp()
    {
        GameManager.Instance.Coins++;
    }
}
