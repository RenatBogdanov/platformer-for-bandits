using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float speedBoostMultiplier = 2f;
    public float jumpForce = 7f;

    public Animator animator;
    public SpriteRenderer spriteRenderer;

    private Rigidbody2D rb;
    private bool isGrounded;
    private float originalSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalSpeed = moveSpeed;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Move();
        Jump();

        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        animator.SetBool("IsJumping", !isGrounded);
    }

    private void Move()
    {
        float moveInput = Input.GetAxis("Horizontal");

        if (Input.GetKey(KeyCode.LeftShift) && isGrounded)
        {
            moveSpeed = originalSpeed * speedBoostMultiplier;
            animator.SetBool("IsRunning", true);
        }
        else
        {
            moveSpeed = originalSpeed;
            animator.SetBool("IsRunning", false);
        }   
        
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        if (moveInput < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (moveInput > 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    private void Jump()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}