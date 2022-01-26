using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; set; }

    public float speed = 1f;
    public float jumpForce = 200f;
    public float diveSpeed = 0.25f;

    public Rigidbody2D rb;
    public CharacterAnimator spriteAnimator;

    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform groundCheck;                           // A position marking where to check if the player is grounded.
    [SerializeField] private Transform ceilingCheck;                          // A position marking where to check for ceilings
    private bool grounded;            // Whether or not the player is grounded.
    const float groundedRadius = .2f; // Radius of the overlap circle to determine if grounded

    [SerializeField] private string jumpParticleAnimation = "jump";
    [SerializeField] private AudioClip jumpSound;

    private void Awake()
    {
        if(Instance!= null)
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
        // If the player should jump...
        if (grounded && Input.GetKeyDown(KeyCode.W))
        {
            // Add a vertical force to the player.
            grounded = false;
            rb.AddForce(new Vector2(0f, jumpForce));

            AnimationManager.PlayAnimationAtPoint(transform.position, jumpParticleAnimation, 2.5f);
            AudioSource.PlayClipAtPoint(jumpSound, transform.position);
        }
    }
    private void FixedUpdate()
    {
        CheckGround();
        Move();
    }

    private void CheckGround()
    {
        grounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius, whatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                grounded = true;
            }
        }
    }

    private void Move()
    {
        Vector2 move = new Vector2();

        // Horizontal Movement
        if (Input.GetKey(KeyCode.A))
            move += new Vector2(-1, 0);
        if (Input.GetKey(KeyCode.D))
            move += new Vector2(1, 0);
        move.x *= speed * Time.fixedDeltaTime;

        // Diving
        if (!grounded)
        {
            if (Input.GetKey(KeyCode.S))
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
