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

        // Detectar movimiento a la izquierda y derecha
        float moveDirection = Input.GetAxis("Horizontal");
        float speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;
        rb.velocity = new Vector2(moveDirection * speed, rb.velocity.y);

        // Cambiar animaciones para correr y caminar
        if (moveDirection != 0)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                animator.SetBool("isRunning", true);
                animator.SetBool("isWalking", false);
                Debug.Log("El jugador está corriendo.");
            }
            else
            {
                animator.SetBool("isRunning", false);
                animator.SetBool("isWalking", true);
                Debug.Log("El jugador está caminando.");
            }
        }
        else
        {
            animator.SetBool("isRunning", false);
            animator.SetBool("isWalking", false);
        }

        // Saltar con flecha arriba
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            animator.SetTrigger("Jump");
            Debug.Log("El jugador ha saltado.");
        }

        // Agacharse con flecha abajo
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            isCrouching = true;
            boxCollider.size = new Vector2(boxCollider.size.x, crouchHeight);
            animator.SetBool("isCrouching", true);
            Debug.Log("El jugador se ha agachado.");
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            isCrouching = false;
            boxCollider.size = new Vector2(boxCollider.size.x, standingHeight);
            animator.SetBool("isCrouching", false);
            Debug.Log("El jugador ha dejado de agacharse.");
        }

        // Voltear al personaje hacia la dirección de movimiento
        if (moveDirection != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(moveDirection), 1, 1);
        }
    }
}
