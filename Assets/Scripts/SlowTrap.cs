using System.Collections;
using UnityEngine;

public class SlowTrap : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public GameObject effectPrefab;
    public bool isStun;
    public float slowDuration = 1f;
    public float percentSpeed = 0.2f;

    private bool isPlayerSlowed = false;
    private float originalSpeed;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<PlayerMovement>() == null)
        {
            return;
        }

        playerMovement = col.GetComponent<PlayerMovement>();
        spriteRenderer.enabled = false;

        
        if (col.CompareTag("Player") && !isPlayerSlowed)
        {
            // Player trap animation
            if (animator != null)
            {
                animator.SetTrigger("Trap");
            }

            // Slow player
            StartCoroutine(SlowPlayerSpeed());

            // Bomb effect
            if (effectPrefab != null)
            {
                Instantiate(effectPrefab, transform.position, Quaternion.identity);
            }
        }
    }

    public void HideSprite()
    {
        spriteRenderer.enabled = false;
    }

    private IEnumerator SlowPlayerSpeed()
    {
        if (isStun)
        {
            spriteRenderer.enabled = false;
        }

        isPlayerSlowed = true;

        // Store original speed of player
        originalSpeed = playerMovement.speed;

        // Decrease the speed to 20% of the original speed
        playerMovement.speed = originalSpeed * percentSpeed;

        // Wait for the specified duration
        yield return new WaitForSeconds(slowDuration);

        // Restore the original speed
        playerMovement.speed = originalSpeed;
        isPlayerSlowed = false;

        Destroy(gameObject);
    }
}