using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider2D))]
public class PlayerMovement2D : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float jumpForce = 5f;
    public float crouchHeight = 0.5f;
    public float standingHeight = 1f;

    private Rigidbody2D rb;
    private Animator animator;
    private BoxCollider2D boxCollider;
    private bool isGrounded;
    private bool isCrouching = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        // Comprobar si el jugador está en el suelo
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, boxCollider.bounds.extents.y + 0.1f);
        float moveDirection = Input.GetAxis("Horizontal");
        float speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;
        rb.linearVelocity = new Vector2(moveDirection * speed, rb.linearVelocity.y);

        // Cambiar animaciones para correr y caminar
        if (moveDirection != 0)
        {
            animator.SetBool("run", Input.GetKey(KeyCode.LeftShift));
            animator.SetBool("idle", false);
        }
        else
        {
            animator.SetBool("run", false);
            animator.SetBool("idle", isGrounded);
        }

        // Saltar con flecha arriba
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            animator.SetTrigger("jump"); // Usar Trigger en vez de Bool
            Debug.Log("El jugador ha saltado.");
        }

        // Detectar caída
        if (!isGrounded && rb.linearVelocity.y < 0)
        {
            animator.SetBool("fall", true);
        }

        // Aterrizaje
        if (isGrounded && animator.GetBool("fall"))
        {
            animator.SetBool("fall", false);
            Debug.Log("El jugador ha aterrizado.");
        }

        // Agacharse
        if (Input.GetKey(KeyCode.DownArrow))
        {
            isCrouching = true;
            boxCollider.size = new Vector2(boxCollider.size.x, crouchHeight);
            animator.SetBool("crouch", true);
        }
        else
        {
            isCrouching = false;
            boxCollider.size = new Vector2(boxCollider.size.x, standingHeight);
            animator.SetBool("crouch", false);
        }

        // Trepar
        if (Input.GetKey(KeyCode.C))
        {
            animator.SetBool("climb", true);
        }
        else
        {
            animator.SetBool("climb", false);
        }

        // Voltear personaje
        if (moveDirection != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(moveDirection), 1, 1);
        }
    }

}
