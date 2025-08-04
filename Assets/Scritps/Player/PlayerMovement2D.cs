using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(AudioSource))]
public class PlayerMovement2D : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float jumpForce = 5f;
    public float climbSpeed = 3f;
    public AudioClip jumpSound;

    private bool canJump = true;

    private Rigidbody2D rb;
    private Animator animator;
    private CapsuleCollider2D capsuleCollider;
    private AudioSource audioSource;

    private bool isGrounded;
    private int groundLayerMask;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        audioSource = GetComponent<AudioSource>();
        groundLayerMask = LayerMask.GetMask("Ground");
    }

    void Update()
    {
        // Detecta si el personaje está tocando el suelo
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, capsuleCollider.bounds.extents.y + 0.1f, groundLayerMask);

        float moveDirection = Input.GetAxisRaw("Horizontal");

        // Movimiento horizontal
        float speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;
        rb.linearVelocity = new Vector2(moveDirection * speed, rb.linearVelocity.y);

        // Flip del personaje
        if (moveDirection != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(moveDirection), 1, 1);
        }

        // Estados de animación
        bool isMoving = Mathf.Abs(moveDirection) > 0;
        bool isRunning = isMoving && Input.GetKey(KeyCode.LeftShift);
        bool isWalking = isMoving && !isRunning;
        bool isIdle = !isMoving && isGrounded && Mathf.Abs(rb.linearVelocity.y) < 0.01f;
        bool isFalling = !isGrounded && rb.linearVelocity.y < 0;

        animator.SetBool("isWalking", isWalking);
        animator.SetBool("isRunning", isRunning);
        animator.SetBool("isIdle", isIdle);
        animator.SetBool("isFalling", isFalling);

        // Saltar
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            HandleJump();
        }

        // Trepar
        if (Input.GetKey(KeyCode.C))
        {
            animator.SetBool("isClimbing", true);
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, climbSpeed);
        }
        else
        {
            animator.SetBool("isClimbing", false);
        }
    }

    void HandleJump()
    {
        canJump = false;
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        animator.SetBool("isJumping", true);
        audioSource.PlayOneShot(jumpSound);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            canJump = true;
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", false);
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            canJump = true;
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", false);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            canJump = false;
        }
    }
}
