using UnityEngine;

public class BgBounds : MonoBehaviour
{
    private void LateUpdate()
    {
        ConstrainPlayersToBounds();
    }

    private void ConstrainPlayersToBounds()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        // Get the bounds of the background collider
        Bounds bounds = GetComponent<Collider2D>().bounds;

        foreach (GameObject player in players)
        {
            if (player != null && player.GetComponent<Player>().IsDead)
            {
                // Get the player's collider size and position
                Collider2D playerCollider = player.GetComponent<Collider2D>();
                Vector2 playerSize = playerCollider.bounds.size;

                // Calculate the minimum and maximum allowed positions
                float minX = bounds.min.x + playerSize.x / 2;
                float maxX = bounds.max.x - playerSize.x / 2;
                float minY = bounds.min.y + playerSize.y / 2;
                float maxY = bounds.max.y - playerSize.y / 2;

                // Get the player's position
                Vector3 playerPosition = player.transform.position;

                // Clamp the player's position to stay within the bounds
                float clampedX = Mathf.Clamp(playerPosition.x, minX, maxX);
                float clampedY = Mathf.Clamp(playerPosition.y, minY, maxY);

                // Update the player's position
                player.transform.position = new Vector3(clampedX, clampedY, playerPosition.z);
            }
        }
    }
}
