using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;  // Adjust the player's movement speed
    public float smoothTime = 0.1f;  // Adjust the smooth time

    public KeyCode moveLeftKey = KeyCode.A;
    public KeyCode moveRightKey = KeyCode.D;
    public KeyCode moveUpKey = KeyCode.W;
    public KeyCode moveDownKey = KeyCode.S;

    private Rigidbody2D rb;
    private Vector2 movementInput;
    private Vector2 currentVelocity;

    [SerializeField] private SpringRotation _springRotation;
    
    public Transform spriteTransform;


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
        
        
        
        // Check for input to flip the sprite

        if (Input.GetKey(moveRightKey))
        {
            // Moving right
            spriteTransform.localScale = new Vector3(1, 1, 1);
        }
        else if (Input.GetKey(moveLeftKey))
        {
            // Moving left
            spriteTransform.localScale = new Vector3(-1, 1, 1);
        }
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