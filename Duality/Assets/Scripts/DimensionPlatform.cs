using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent (typeof(SpriteRenderer))]
public class DimensionPlatform : DimensionObject
{
    private Collider2D collider;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        collider = gameObject.GetComponent<Collider2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    public override void SetActive(bool active)
    {
        collider.enabled = active;

        if (active)
        {
            Color newColor = spriteRenderer.color;
            newColor.a = 1f;
            spriteRenderer.color = newColor;
        }
        else
        {
            Color newColor = spriteRenderer.color;
            newColor.a = 0.07f;
            spriteRenderer.color = newColor;
        }
    }
}
