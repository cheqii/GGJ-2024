using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;  // Adjust the player's movement speed
    public float smoothTime = 0.1f;  // Adjust the smooth time

    private Rigidbody2D rb;
    private Vector2 movementInput;
    private Vector2 currentVelocity;

    [SerializeField] private SpringRotation _springRotation;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Get player input
        movementInput.x = Input.GetAxisRaw("Horizontal");
        movementInput.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        // Move the player
        MovePlayer();

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

        // Calculate the target velocity
        Vector2 targetVelocity = movementInput * speed;

        // Smoothly interpolate between the current velocity and the target velocity
        rb.velocity = Vector2.Lerp(rb.velocity, targetVelocity, smoothTime);
    }
}