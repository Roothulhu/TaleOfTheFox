using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(AudioSource))]
public class PlayerMovement2D : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float jumpForce = 5f;
    public float climbSpeed = 3f;
    public AudioClip jumpSound;

    private bool canJump = true;
    private bool isFalling = false;
    
    private Rigidbody2D rb;
    private Animator animator;
    private BoxCollider2D boxCollider;
    private AudioSource audioSource;

    private bool isGrounded; //Detectar suelo
    private int groundLayerMask; //Identificador de capa ecenario
    private string currentAnimationState;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        audioSource = GetComponent<AudioSource>();
        
        groundLayerMask = LayerMask.GetMask("Ground");
        Debug.Log($"Ground Layer Mask Value: {groundLayerMask}");
    }

    void Update()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, boxCollider.bounds.extents.y + 0.1f, groundLayerMask);
        
        float moveDirection = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveDirection * (Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed), rb.linearVelocity.y);
        
        if (moveDirection != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(moveDirection), 1, 1);
            Debug.Log($"FlipCharacter called. Direction: {Mathf.Sign(moveDirection)}");
        }

        if (Input.GetAxis("Horizontal") != 0 && Input.GetKey(KeyCode.LeftShift))
        {
            ChangeAnimationState("isRunning");
        }

        if (Input.GetAxis("Horizontal") != 0)
        {
            ChangeAnimationState("isWalking");
        }

        if (rb.linearVelocity.x == 0)
        {
            ChangeAnimationState("isIdle");
        }
        else if (!isGrounded && rb.linearVelocity.y > 0)
        {
            ChangeAnimationState("isJumping");
        }
        
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            if (currentAnimationState == "isRunning" || currentAnimationState == "isWalking")
            {
                HandleJump();
            }
            else
            {
                HandleJump();
            }
        }
        
        if (Input.GetKey(KeyCode.C))
        {
            HandleClimbing();
        }

        if (!isGrounded && rb.linearVelocity.y < 0)
        {
            HandleFalling();
            canJump = true; // Habilitar salto al tocar el suelo
        }
    }

    void HandleJump()
    {
        ChangeAnimationState("isJumping");
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        canJump = false;
        audioSource.PlayOneShot(jumpSound);
        Debug.Log("HandleJump called: Jump triggered.");
    }

    void HandleClimbing()
    {
        if (Input.GetKey(KeyCode.C) && gameObject.CompareTag("Climbable"))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, climbSpeed); // Escalar hacia arriba
            ChangeAnimationState("isClimbing");
            Debug.Log("HandleClimbing called: Climbing triggered.");
        }
    }

    void HandleFalling()
    {
        ChangeAnimationState("isFalling");
        Debug.Log("HandleFalling called: Falling triggered.");
    }

    bool IsTouchingClimbable()
    {
        bool touchingClimbable = Physics2D.Raycast(transform.position, Vector2.up, boxCollider.bounds.extents.y + 0.1f, LayerMask.GetMask("Climbable"));
        Debug.Log($"IsTouchingClimbable: {touchingClimbable}");
        return touchingClimbable;
    }

    void ChangeAnimationState(string newState)
    {
        animator.ResetTrigger("isIdle");
        animator.ResetTrigger("isWalking");
        animator.ResetTrigger("isRunning");
        animator.ResetTrigger("isJumping");
        animator.ResetTrigger("isFalling");
        animator.ResetTrigger("isClimbing");
        animator.SetTrigger(newState);
        currentAnimationState = newState;
        Debug.Log($"ChangeAnimationState: {newState} triggered.");
        if (currentAnimationState == newState) return;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            canJump = true;  // Permitir salto inmediatamente al aterrizar
            ChangeAnimationState("isIdle");
            Debug.Log("OnCollisionEnter2D: Landed on ground, canJump set to true.");
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            canJump = true;  // Mantener canJump habilitado mientras está en el suelo
            ChangeAnimationState("isIdle");
            Debug.Log("OnCollisionStay2D: Staying on ground, canJump set to true.");
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            canJump = false;  // Deshabilitar salto al salir del suelo
            Debug.Log("OnCollisionExit2D: Left ground, canJump set to false.");
        }
    }
}
