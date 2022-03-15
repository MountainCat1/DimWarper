using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class EvilMage : DimensionObject
{
    public float deathHeight = 800f;

    public float verticalMovementSpeed = 5f;
    public float horizontalMovementSpeed = 1f;
    public float warpInterval = 8;
    public float warpIntervalRandomness = 4;
    public float missileShotInterval = 5f;
    public float missileShotIntervalRandomness = 3.5f;

    public float yOffset = 10f;

    private float targetPosX;

    private Collider2D collider;
    private SpriteRenderer spriteRenderer;

    public AudioSource dimensionSwapAudioSource;

    public Missile fireMissilePrefab;
    public Missile iceMissilePrefab;

    public string changeDimensionParticle = "warp";
    
    private void Awake()
    {
        collider = gameObject.GetComponent<Collider2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        StartCoroutine(DimensionSwapCoroutine());
        StartCoroutine(ShotMissileSwapCoroutine());
    }

    private void FixedUpdate()
    {
        float targetHeight = yOffset + GameManager.Instance.ExpectedHeight;

        Vector3 targetPos = new Vector3(targetPosX, targetHeight, transform.position.z);

        float stepX = Time.fixedDeltaTime * horizontalMovementSpeed;
        float stepY = Time.fixedDeltaTime * verticalMovementSpeed;

        Vector3 newPos = transform.position;

        newPos.x = Vector3.MoveTowards(transform.position, targetPos, stepX).x;
        newPos.y = Vector3.MoveTowards(transform.position, targetPos, stepY).y;

        transform.position = newPos;

        if (Vector3.Distance(transform.position, targetPos) < 0.1f)
            targetPosX = LevelGenerator.GetRandomPosX();
    }

    private void Update()
    {
        if(GameManager.Instance.ExpectedHeight >= deathHeight)
        {
            Kill();
            targetPosX = 0f;
        }
    }

    private void Kill()
    {
        StopAllCoroutines();
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
        Missile missile = dimension == DimensionManager.Dimension.Fire ? fireMissilePrefab : iceMissilePrefab;

        Vector3 direction = (PlayerController.Instance.transform.position - transform.position).normalized;

        var go = Instantiate(missile.gameObject, transform.position, Quaternion.identity);
        var newMissile = go.GetComponent<Missile>();

        newMissile.Direction = direction;
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