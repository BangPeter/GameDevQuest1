using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject : MonoBehaviour
{
    //Default = 10
    public float playerMoveSpeed = 10.0f;
    public float jumpingPower = 8.0f;
    private float horizontal = 0.0f;

    private bool isFacingRight = true;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {

    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.8f, groundLayer);
    }

    private bool IsJumping()
    {
        return Mathf.Abs(rb.velocity.y) > 0.5;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal") * playerMoveSpeed;
        //vertical = Input.GetAxis("Vertical") * jumpingPower;

        animator.SetFloat("PlayerSpeed", Mathf.Abs(horizontal));
        animator.SetFloat("JumpSpeed", rb.velocity.y);

        if ((isFacingRight && horizontal < 0.0f) || (!isFacingRight && horizontal > 0.0f))
        {
            isFacingRight = !isFacingRight;
        }

        /*
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0.0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
        */
        if (Input.GetButtonDown("Vertical") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if ((Input.GetButtonUp("Vertical") && rb.velocity.y > 0.0f))
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            animator.SetTrigger("Attacking");
        }
    }

    private void FixedUpdate()
    {
        animator.SetBool("Jump", IsJumping());
        if (IsJumping())
        {
            rb.gravityScale = 2.0f;
        }
        else
        {
            rb.gravityScale = 1;
        }
        rb.velocity = new Vector2(horizontal, rb.velocity.y);
        sr.flipX = !isFacingRight;
    }
}