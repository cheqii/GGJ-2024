using UnityEngine;

public class CrosshairFollow : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed = 5f;
    public float maxDistance = 2f; // Maximum distance from the player
    public float hideThreshold = 0.5f; // Threshold for hiding the crosshair

    void Update()
    {
        // Get player input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate target position based on player input
        Vector2 targetPosition = player.position + new Vector3(horizontalInput, verticalInput, 0) * maxDistance;

        // Smoothly move towards the target position
        Vector2 smoothedPosition = Vector2.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

        // Check distance between crosshair and player to determine if it's near
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer < hideThreshold)
        {
            // Hide the sprite or set its alpha to zero (adjust based on your sprite rendering method)
            GetComponent<SpriteRenderer>().enabled = false;
        }
        else
        {
            // Show the sprite or set its alpha to one
            GetComponent<SpriteRenderer>().enabled = true;
        }

        // Adjust depth based on player input
        float adjustedDepth = verticalInput * 0.5f; // You can adjust this value based on your needs
        transform.position = new Vector3(transform.position.x, transform.position.y, player.position.z + adjustedDepth);
    }
}