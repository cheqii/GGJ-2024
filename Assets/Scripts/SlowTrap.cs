using System.Collections;
using UnityEngine;

public class SlowTrap : MonoBehaviour
{
    public Player player;
    public PlayerMovement playerMovement;
    public GameObject effectPrefab;
    public float slowDuration = 1f;
    public float percentSpeed = 0.2f;

    private bool isPlayerSlowed = false;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Destroy(gameObject, 3f);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Player") && isPlayerSlowed)
        {
            Debug.Log("Reset speed if already slow.");
            isPlayerSlowed = false;
            playerMovement.speed = 5f;
            return;
        }
        else if (col.CompareTag("Player") && !isPlayerSlowed)
        {
            playerMovement = col.GetComponent<PlayerMovement>();
            player = col.GetComponent<Player>();
            spriteRenderer.enabled = false;

            if (player.IsBullying & !isPlayerSlowed)
            {
                // Bomb effect
                if (effectPrefab != null)
                {
                    Instantiate(effectPrefab, transform.position, Quaternion.identity);
                }

                // Slow player
                StartCoroutine(SlowPlayerSpeed());
                Debug.Log("Slow");
            }
            else
            {
                Debug.Log("Play is not bullying, then not slow.");
            }
        }
    }

    private IEnumerator SlowPlayerSpeed()
    {
        // Decrease the speed to 20% of the original speed
        isPlayerSlowed = true;
        playerMovement.speed = 5f * percentSpeed;

        // Wait for the specified duration
        yield return new WaitForSeconds(slowDuration);

        // Reset to not slow
        playerMovement.speed = 5f;
        isPlayerSlowed = false;
        Destroy(gameObject);
    }
}