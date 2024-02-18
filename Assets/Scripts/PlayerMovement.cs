using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject : MonoBehaviour
{

    public float playerMoveSpeed = 5.0f;
    public float jumpingPower = 8.0f;

    private float horizontal = 0.0f;
    //private bool jump = false;
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
        return Physics2D.OverlapCircle(groundCheck.position, 1.2f, groundLayer);
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal") * playerMoveSpeed;

        animator.SetFloat("PlayerSpeed", Mathf.Abs(horizontal));
        animator.SetFloat("JumpSpeed", rb.velocity.y);

        if ((isFacingRight && horizontal < 0.0f) || (!isFacingRight && horizontal > 0.0f))
        {
            isFacingRight = !isFacingRight;
        }

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0.0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal, rb.velocity.y);
        sr.flipX = !isFacingRight;
    }
}