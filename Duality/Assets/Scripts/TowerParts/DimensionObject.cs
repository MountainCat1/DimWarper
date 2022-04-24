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

    protected void SetDimensionForSpriteRenderer(bool active, SpriteRenderer spriteRenderer)
    {
        if (active)
        {
            Color newColor = spriteRenderer.color;
            newColor.a = 1f;
            spriteRenderer.color = newColor;
        }
        else
        {
            Color newColor = spriteRenderer.color;
            newColor.a = DimensionManager.Instance.deactivatedObjectAlpha;
            spriteRenderer.color = newColor;
        }
    }
}
