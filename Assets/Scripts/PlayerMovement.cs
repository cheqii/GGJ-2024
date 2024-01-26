using System.Collections;
using MoreMountains.Feedbacks;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;  // Adjust the player's movement speed
    public float dashSpeed = 10f;  // Adjust the player's dash speed
    public float dashDuration = 0.2f;  // Adjust the duration of the dash
    public float dashCooldown = 2f;  // Adjust the cooldown time between dashes
    public float smoothTime = 0.1f;  // Adjust the smooth time

    public KeyCode moveLeftKey = KeyCode.A;
    public KeyCode moveRightKey = KeyCode.D;
    public KeyCode moveUpKey = KeyCode.W;
    public KeyCode moveDownKey = KeyCode.S;
    public KeyCode dashKey = KeyCode.LeftShift;

    private Rigidbody2D rb;
    private Vector2 movementInput;
    private Vector2 currentVelocity;
    private bool isDashing = false;
    private bool isDashReady = true;

    public Transform spriteTransform;

    public MMF_Player DashEffect;

    [SerializeField] private SpringRotation _springRotation;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Get player input using the Input System
        movementInput = new Vector2(
            Input.GetKey(moveRightKey) ? 1 : Input.GetKey(moveLeftKey) ? -1 : 0,
            Input.GetKey(moveUpKey) ? 1 : Input.GetKey(moveDownKey) ? -1 : 0
        );

        // Check for input to dash
        if (Input.GetKeyDown(dashKey) && isDashReady)
        {
            StartCoroutine(Dash());
        }

        // Check for input to flip the sprite
        if (movementInput.x > 0)
        {
            // Moving right
            spriteTransform.localScale = new Vector3(1, 1, 1);
        }
        else if (movementInput.x < 0)
        {
            // Moving left
            spriteTransform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void FixedUpdate()
    {
        // Move the player
        MovePlayer();

        // Handle spring rotation
        if (movementInput.x != 0 || movementInput.y != 0)
        {
            _springRotation.StartRotation();
        }
        else
        {
            _springRotation.StopRotation();
        }
    }

    void MovePlayer()
    {
        // Normalize the movement vector to ensure constant speed in all directions
        movementInput.Normalize();

        // Calculate the target velocity based on whether dashing or not
        Vector2 targetVelocity = isDashing ? movementInput * dashSpeed : movementInput * speed;

        // Smoothly interpolate between the current velocity and the target velocity
        rb.velocity = Vector2.Lerp(rb.velocity, targetVelocity, smoothTime);
    }

    IEnumerator Dash()
    {
        isDashReady = false;

        Debug.Log("Dash is not ready");

        DashEffect.PlayFeedbacks();

        isDashing = true;
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);
        isDashReady = true;

        Debug.Log("Dash is ready");
    }
}
