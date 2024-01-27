using System.Collections;
using UnityEngine;

public class BombTrap : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public GameObject bombEffectPrefab;
    public float slowDuration = 1f;

    private bool isPlayerSlowed = false;
    private float originalSpeed;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && !isPlayerSlowed)
        {
            StartCoroutine(SlowPlayerSpeed());
            spriteRenderer.enabled = false;

            // Bomb effect
            Instantiate(bombEffectPrefab, transform.position, Quaternion.identity);
        }
    }

    private IEnumerator SlowPlayerSpeed()
    {
        isPlayerSlowed = true;

        // Store original speed of player
        originalSpeed = playerMovement.speed;

        // Decrease the speed to 20% of the original speed
        playerMovement.speed = originalSpeed * 0.2f;

        // Wait for the specified duration
        yield return new WaitForSeconds(slowDuration);

        // Restore the original speed
        playerMovement.speed = originalSpeed;
        isPlayerSlowed = false;

        Destroy(gameObject);
    }
}