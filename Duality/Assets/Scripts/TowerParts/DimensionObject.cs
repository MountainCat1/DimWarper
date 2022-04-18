using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DimensionObject : MonoBehaviour
{
    public DimensionManager.Dimension dimension;

    private void OnEnable()
    {
        DimensionManager.Instance.dimensionObjects.Add(this);
        SetActive(DimensionManager.Instance.dimension == dimension);
    }
    private void OnDisable()
    {
        DimensionManager.Instance.dimensionObjects.Remove(this);
    }

    public abstract void SetActive(bool active);
}
