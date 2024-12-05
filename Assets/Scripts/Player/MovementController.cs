using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField] private float speed = 5f;
    [Range(1,10)]
    [SerializeField] private float acceleration;
    float speedMultiplier;

    [SerializeField] private float jumpForce = 10f;
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

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        UpdateSpeedMultiplier();

        float targetSpeed = speed * speedMultiplier * direction;

        rb.linearVelocity = new Vector2(targetSpeed, rb.linearVelocity.y);

        isTouchingWall = Physics2D.OverlapBox(wallCheck.position, new Vector2(0.035f, 0.4f), 0, wallLayer);

        if (isTouchingWall)
        {
            // touching wall
        }

        Flip();
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

}
