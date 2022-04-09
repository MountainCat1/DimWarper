using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : PickUp
{

    protected override void OnPickUp()
    {
        Power();
    }


    protected abstract void Power();
}
