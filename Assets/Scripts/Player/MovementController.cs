using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField] private float speed = 5f;
    [Range(1,10)]
    [SerializeField] private float acceleration;
    float speedMultiplier;

    [Range(1, 5)]
    [SerializeField] private float jumpForce = 3f;
    bool buttonPressed;

    bool isTouchingWall;
    public LayerMask wallLayer;
    public Transform wallCheck;

    bool isTouchingGround;
    public LayerMask groundLayer;
    public Transform groundCheck;

    public float direction;

    
    [SerializeField]
    private int maxJumpCount = 2;
    private int jumpCount = 0;
    private bool jumpedOnThisFrame = false;

    Animator animator;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {

        Debug.Log(animator.GetBool("isJumping"));

        UpdateSpeedMultiplier();

        float targetSpeed = speed * speedMultiplier * direction;

        rb.linearVelocity = new Vector2(targetSpeed, rb.linearVelocity.y);

        isTouchingWall = Physics2D.OverlapBox(wallCheck.position, new Vector2(0.035f, 0.4f), 0, wallLayer);
        isTouchingGround = Physics2D.OverlapBox(groundCheck.position, new Vector2(0.16f, 0.02f), 0, groundLayer);

        if (isTouchingWall)
        {
            ResetJumps();
        }

        if (isTouchingGround)
        {
            if(!jumpedOnThisFrame)
            {
                ResetJumps();
                animator.SetBool("isJumping", false);
            }
        }

        animator.SetFloat("xVel", Math.Abs(rb.linearVelocity.x));
        animator.SetFloat("yVel", rb.linearVelocity.y);

        Flip();
        jumpedOnThisFrame = false;
    }

    public void Flip()
    {
        if (direction > 0) // Moving right
        {
            transform.localScale = new Vector3(1, 1, 1); // Default scale
        }
        else if (direction < 0) // Moving left
        {
            transform.localScale = new Vector3(-1, 1, 1); // Flipped scale
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            buttonPressed = true;
            speedMultiplier = 1;
        }
        else if (context.canceled)
        {
            buttonPressed = false;
            speedMultiplier = 0;
        }
        direction = context.ReadValue<Vector2>().x;
        if (direction > 0) direction = 1;
        if (direction < 0) direction = -1;

    }

    public void UpdateSpeedMultiplier()
    {
        if (buttonPressed && speedMultiplier < 1)
        {
            speedMultiplier += Time.deltaTime*acceleration;
        }
        else if (!buttonPressed && speedMultiplier > 1)
        {
            speedMultiplier -= Time.deltaTime*acceleration;
            if (speedMultiplier < 0)
            {
                speedMultiplier = 0;
            }
        }
    }

    public void Jump(InputAction.CallbackContext context) {
        if (jumpedOnThisFrame)
        {
            return;
        }
        if (jumpCount < maxJumpCount && context.started)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
            rb.AddForce(Vector2.one * jumpForce, ForceMode2D.Impulse);
            animator.SetBool("isJumping", true);
            jumpCount++;
            jumpedOnThisFrame = true;
        }
    }

    public void ResetJumps()
    {
        jumpCount = 0;
    }

}
