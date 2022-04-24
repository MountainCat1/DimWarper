using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class DimensionCreature : DimensionObject
{
    public bool Active { get => DimensionManager.Instance.dimension == dimension; }

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

        SetDimensionForSpriteRenderer(active, spriteRenderer);
    }
}
