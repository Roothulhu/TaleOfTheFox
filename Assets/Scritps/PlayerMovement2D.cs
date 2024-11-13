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
    private bool isCrouching;


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

        //Debug.Log("El movimieento en x es: " + rb.linearVelocity.x);
        // Cambiar animaciones para correr y caminar
        if(rb.linearVelocity.x == 0)
        {
            animator.SetBool("isIdle", true);
        }
        else
        {
            transform.localScale = new Vector3(Mathf.Sign(moveDirection), 1, 1);
            if (Input.GetKey(KeyCode.LeftShift))
            {
                animator.SetBool("isRunning", true);
                //Debug.Log("El jugador está corriendo.");
            }
            else
            {
                animator.SetBool("isWalking", true);
                //Debug.Log("El jugador está caminando.");
            }
        }
        

        // Saltar con flecha arriba
        if (Input.GetKeyDown(KeyCode.UpArrow )) 
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            animator.SetBool("isJumping", true); //Mientras no est tocando el suelo y su velovidad en Y sea mayor que 0
            //Debug.Log("El jugador ha saltado.");
        }

        // Agacharse con flecha abajo
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            isCrouching = true;
            boxCollider.size = new Vector2(boxCollider.size.x, crouchHeight);
            animator.SetBool("isCrouching", true);
            //Debug.Log("El jugador se ha agachado.");
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            isCrouching = false;
            boxCollider.size = new Vector2(boxCollider.size.x, standingHeight);
            //Debug.Log("El jugador ha dejado de agacharse.");
        }
    }
}
