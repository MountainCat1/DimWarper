using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// This class should NOT be used
// It is basically code i will use later for last boss
// Right now i just have no good idea how to do it so ye xD
public class DimensionalEvilMage : DimensionObject
{
    private Collider2D collider;
    private SpriteRenderer spriteRenderer;

    public AudioSource dimensionSwapAudioSource;

    public Missile fireMissilePrefab;
    public Missile iceMissilePrefab;
    
    public string changeDimensionParticle = "warp";
    
    public float warpInterval = 8;
    public float warpIntervalRandomness = 4;
    public float missileShotInterval = 5f;
    public float missileShotIntervalRandomness = 3.5f;
    
    private void Awake()
    {
        collider = gameObject.GetComponent<Collider2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
    
    protected void Start()
    {
        StartCoroutine(DimensionSwapCoroutine());
        StartCoroutine(ShotMissileSwapCoroutine());
    }

    private IEnumerator DimensionSwapCoroutine()
    {
        while (true)
        {
            SwapDimension();
            yield return new WaitForSeconds(warpInterval + UnityEngine.Random.Range(-warpIntervalRandomness, +warpIntervalRandomness));
        }
    }
    
    private void SwapDimension()
    {
        dimensionSwapAudioSource.Play();
        if (dimension == DimensionManager.Dimension.Ice)
            dimension = DimensionManager.Dimension.Fire;
        else
            dimension = DimensionManager.Dimension.Ice;

        SetActive(dimension == DimensionManager.Instance.dimension);
        AnimationManager.PlayAnimationAtPoint(transform.position, changeDimensionParticle, 5);
    }

    /// <summary>
    /// Calls ShotMissile method once every random amount of time
    /// depending on missileShotInterval and missileShotIntervalRandomness
    /// </summary>
    /// <returns></returns>
    private IEnumerator ShotMissileSwapCoroutine()
    {
        while (true)
        {
            ShotMissile();
            yield return 
                new WaitForSeconds(missileShotInterval + 
                    UnityEngine.Random.Range(
                        -missileShotIntervalRandomness,
                        +missileShotIntervalRandomness));
        }
    }

    private void ShotMissile()
    {
        var missile = dimension == DimensionManager.Dimension.Fire ? fireMissilePrefab : iceMissilePrefab;

        var direction = (PlayerController.Instance.transform.position - transform.position).normalized;

        var go = Instantiate(missile.gameObject, transform.position, Quaternion.identity);
        var newMissile = go.GetComponent<Missile>();

        newMissile.Direction = direction;
    }

    public override void SetActive(bool active)
    {
        collider.enabled = active;
    
        var newColor = spriteRenderer.color;
        if (active)
        {
            newColor.a = 1f;
            spriteRenderer.color = newColor;
        }
        else
        {
            newColor.a = 0.07f;
            spriteRenderer.color = newColor;
        }
    }
}
