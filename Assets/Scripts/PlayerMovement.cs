using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject : MonoBehaviour
{
    //Default = 10
    public float playerMoveSpeed = 10.0f;
    public float jumpingPower = 8.0f;
    private float horizontal = 0.0f;
    private GameObject hitbox;

    private bool isFacingRight = true;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer sr;

    [SerializeField] private float attackDelay = 0.15f;

    // Start is called before the first frame update
    void Start()
    {
        hitbox = GameObject.FindWithTag("Hitbox");
        hitbox.GetComponent<BoxCollider2D>().enabled = false;
    }

    private bool IsGrounded()
    {
        //Debug.Log((bool) Physics2D.OverlapBox(groundCheck.position, new Vector2(0.1f, 0.8f), 0.0f, groundLayer));
        return Physics2D.OverlapBox(groundCheck.position, new Vector2(0.8f, 1.1f), 0.0f, groundLayer);
        //return Physics2D.OverlapCircle(groundCheck.position, 0.5f, groundLayer);
    }

    private bool IsJumping()
    {
        return Mathf.Abs(rb.velocity.y) != 0f;
    }

    private void ActivateHitbox()
    {
        //hitbox = GameObject.FindWithTag("Hitbox");
        hitbox.GetComponent<BoxCollider2D>().enabled = true;
        Invoke("DisableHitbox", attackDelay);
    }

    private void DisableHitbox()
    {
        //hitbox = GameObject.FindWithTag("Hitbox");
        hitbox.GetComponent<BoxCollider2D>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate player speed
        horizontal = Input.GetAxis("Horizontal") * playerMoveSpeed;
        //vertical = Input.GetAxis("Vertical") * jumpingPower;

        animator.SetFloat("PlayerSpeed", Mathf.Abs(horizontal));
        animator.SetFloat("JumpSpeed", rb.velocity.y);

        // Flip player
        if ((isFacingRight && horizontal < 0.0f) || (!isFacingRight && horizontal > 0.0f))
        {
            isFacingRight = !isFacingRight;
        }


        // Jump
        if (Input.GetButtonDown("Vertical") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        // Half Jump
        if ((Input.GetButtonUp("Vertical") && rb.velocity.y > 0.0f))
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }


        // Attack
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
            rb.gravityScale = 1.8f;
        }
        else
        {
            rb.gravityScale = 1;
        }
        // Set player velocity
        rb.velocity = new Vector2(horizontal, rb.velocity.y);
        sr.flipX = !isFacingRight;
    }
}