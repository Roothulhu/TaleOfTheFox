using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(AudioSource))] // Añadimos un AudioSource al objeto
public class PlayerMovement2D : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float jumpForce = 5f;
    public float climbSpeed = 3f;
    public AudioClip jumpSound; // Clip de sonido para el salto

    private bool canJump = true;
    private bool isFalling = false;

    private Rigidbody2D rb;
    private Animator animator;
    private BoxCollider2D boxCollider;
    private AudioSource audioSource;

    private bool isGrounded;
    private string currentAnimationState;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        audioSource = GetComponent<AudioSource>();

        Debug.Log("PlayerMovement2D initialized.");
    }

    void Update()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, boxCollider.bounds.extents.y + 0.1f, LayerMask.GetMask("Ground"));

        if (isGrounded)
        {
            canJump = true; // Habilitar salto al tocar el suelo
        }

        if (Input.GetAxis("Horizontal") != 0)
        {
            HandleMovement();
        }
        else if (isGrounded && rb.velocity.x == 0)
        {
            ChangeAnimationState("isIdle");
        }

        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            HandleJump();
        }

        if (Input.GetKey(KeyCode.C))
        {
            HandleClimbing();
        }

        if (!isGrounded && rb.velocity.y < 0)
        {
            HandleFalling();
        }
    }

    void HandleMovement()
    {
        float moveDirection = Input.GetAxis("Horizontal");
        float speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;
        rb.velocity = new Vector2(moveDirection * speed, rb.velocity.y);

        if (isGrounded)
        {
            ChangeAnimationState(Input.GetKey(KeyCode.LeftShift) ? "isRunning" : "isWalking");
        }

        if (moveDirection != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(moveDirection), 1, 1);
            Debug.Log($"FlipCharacter called. Direction: {Mathf.Sign(moveDirection)}");
        }
    }

    void HandleJump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        ChangeAnimationState("isJumping");
        canJump = false;  // Deshabilitar salto inmediatamente después de saltar
        audioSource.PlayOneShot(jumpSound); // Reproduce el sonido de salto
        Debug.Log("HandleJump called: Jump triggered.");
    }

    void HandleClimbing()
    {
        if (IsTouchingClimbable())
        {
            rb.velocity = new Vector2(rb.velocity.x, climbSpeed);
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
        if (currentAnimationState == newState) return;

        animator.ResetTrigger("isIdle");
        animator.ResetTrigger("isWalking");
        animator.ResetTrigger("isRunning");
        animator.ResetTrigger("isJumping");
        animator.ResetTrigger("isFalling");
        animator.ResetTrigger("isClimbing");

        animator.SetTrigger(newState);
        currentAnimationState = newState;

        Debug.Log($"ChangeAnimationState: {newState} triggered.");
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
