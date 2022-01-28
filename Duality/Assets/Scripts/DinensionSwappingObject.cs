using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinensionSwappingObject : DimensionObject
{
    public event Action<bool> DimensionChangedEvent;

    public void SwapDimensions()
    {
        if (dimension == DimensionManager.Dimension.Ice)
            dimension = DimensionManager.Dimension.Fire;
        else
            dimension = DimensionManager.Dimension.Ice;

        SetActive(DimensionManager.Instance.dimension == dimension);
    }


    public override void SetActive(bool active)
    {
        DimensionChangedEvent?.Invoke(active);
    }
}
