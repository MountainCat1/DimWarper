using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; set; }

    public bool Dead { get; set; } = false;
    public AudioSource deathSoundAudioSource;
    public GameObject bloodParticleSystem;

    public float speed = 1f;
    public float jumpForce = 200f;
    public float diveSpeed = 0.25f;

    public Rigidbody2D rb;
    public CharacterAnimator spriteAnimator;

    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform groundCheck;                             // A position marking where to check if the player is grounded.
    [SerializeField] private Transform ceilingCheck;                            // A position marking where to check for ceilings
    [SerializeField] private Collider2D collider;                               // A position marking where to check for ceilings
    [SerializeField] private float ungroundedDelay = 0.1f;                       // How much time after falling player still is considered grounded
    private bool grounded;                                                      // Whether or not the player is grounded.
    private bool consideredGrounded;
    private Coroutine ungroudDelayCoroutine;
    const float groundedRadius = .2f;                                           // Radius of the overlap circle to determine if grounded

    [SerializeField] private string jumpParticleAnimation = "jump";
    [SerializeField] private float jumpForceAnimationSpeed = 4f;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip diveSound;
    [SerializeField] private string changeDimensionParticle = "warp";
    [SerializeField] private float changeDimensionAnimationSpeed = 4f;
    [SerializeField] private AudioClip noEnergySound;
    [SerializeField] private AudioClip energyUseSound;

    public Transform Floor { private set; get; }
    private bool UsedEnergyAction { get; set; } = false;

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("Singeleton duplicated!");
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Update()
    {
        if (Dead || GameManager.Instance.Paused)
            return;

        // If the player should jump...
        if (Input.GetButtonDown("Jump") && (consideredGrounded || TryUseEnergyAction()))
        {
            // Add a vertical force to the player.
            grounded = false;
            consideredGrounded = false;

            rb.velocity = new Vector2(rb.velocity.x, 0f);

            rb.AddForce(new Vector2(0f, jumpForce));

            AnimationManager.PlayAnimationAtPoint(transform.position, jumpParticleAnimation, jumpForceAnimationSpeed);
            AudioSource.PlayClipAtPoint(jumpSound, transform.position);
        }

        if(!grounded && Input.GetButtonDown("Dive"))
        {
            AudioSource.PlayClipAtPoint(diveSound, transform.position);
        }

        if (Input.GetButtonDown("Dimension Swap") 
            && !Pause.isPaused 
            && !GameManager.Instance.Lost 
            && !GameManager.Instance.Won)
        {
            AnimationManager.PlayAnimationAtPoint(transform.position, changeDimensionParticle, changeDimensionAnimationSpeed);
            DimensionManager.Instance.SwitchDimension();
        }
    }

    public void Kill()
    {
        Dead = true;
        collider.enabled = false;
        bloodParticleSystem.SetActive(true);
        deathSoundAudioSource.Play();
        GameManager.Instance.Lose();
    }
    
    private bool TryUseEnergyAction()
    {
        if (UsedEnergyAction)
        {
            AudioSource.PlayClipAtPoint(noEnergySound, Camera.main.transform.position);
            return false;
        }
            
        if (GameManager.Instance.actionEnergyCost > GameManager.Instance.Energy)
        {
            AudioSource.PlayClipAtPoint(noEnergySound, Camera.main.transform.position);
            return false;
        }

        GameManager.Instance.Energy -= GameManager.Instance.actionEnergyCost;
        UsedEnergyAction = true;
        AudioSource.PlayClipAtPoint(energyUseSound, Camera.main.transform.position);
        return true;
    }

    private void FixedUpdate()
    {
        CheckGround();
        Move();
    }

    private void CheckGround()
    {
        grounded = false;
        Floor = null;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        var colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius, whatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                UsedEnergyAction = false;
                grounded = true;
                Floor = colliders[i].transform;
                break;
            }
        }

        if (grounded)
        {
            consideredGrounded = true;
            if(ungroudDelayCoroutine != null) StopCoroutine(ungroudDelayCoroutine);
            return;
        }
        if (ungroudDelayCoroutine == null)
        {
            ungroudDelayCoroutine = StartCoroutine(UngroundDelay());
        }
    }
    
    private IEnumerator UngroundDelay()
    {
        yield return new WaitForSeconds(ungroundedDelay);
        consideredGrounded = false;
    }

    private void Move()
    {
        Vector2 move = new Vector2();

        // Horizontal Movement
        if (Input.GetAxisRaw("Horizontal") < 0)
            move += new Vector2(-1, 0);
        if (Input.GetAxisRaw("Horizontal") > 0)
            move += new Vector2(1, 0);
        move.x *= speed * Time.fixedDeltaTime;

        // Diving
        if (!grounded)
        {
            if (Input.GetButton("Dive"))
                move += new Vector2(0, -1);

            move.y *= diveSpeed * Time.fixedDeltaTime;
        }

        Vector2 newVelocity;
        if(move.y == 0)
            newVelocity = new Vector2(move.x, rb.velocity.y);
        else
            newVelocity = new Vector2(move.x, move.y);


        rb.velocity = newVelocity;

        // Animation stuff...
        if (move.x != 0)
            spriteAnimator.flipped = move.x > 0;

        if (grounded)
        {
            if (move.x != 0)
            {
                spriteAnimator.SetAnimation(CharacterAnimator.Animation.Walk);
            }
            else
            {
                spriteAnimator.SetAnimation(CharacterAnimator.Animation.Idle);
            }
        }
        else
        {
            spriteAnimator.SetAnimation(CharacterAnimator.Animation.Jump);
        }
        
    }
}
