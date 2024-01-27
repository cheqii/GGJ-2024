using System;
using System.Collections;
using UnityEngine;

public class DamageArea : MonoBehaviour
{
    private bool damageApplied = false;
    private float damageDelay = 0.1f; // Set the delay time in seconds
    public int damageAmount = 10;

    private void Start()
    {
        Destroy(this.gameObject,0.3f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Entered trigger area");
        
        if (!damageApplied && other.CompareTag("Player"))
        {
            // Check if the collided object is the player
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                // Start a coroutine to apply damage with a delay
                StartCoroutine(ApplyDamageWithDelay(player));
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        
        Debug.Log("Exited trigger area");

        if (other.CompareTag("Player"))
        {
            // Reset the damageApplied flag when the player exits the trigger area
            damageApplied = false;
        }
    }

    // Coroutine to apply damage to the player with a delay
    IEnumerator ApplyDamageWithDelay(Player player)
    {
        // Set damageApplied to true to avoid applying damage again during the delay
        damageApplied = true;

        // Wait for the specified delay
        yield return new WaitForSeconds(damageDelay);

        // Apply damage to the player
        player.DeceaseHealth(damageAmount);

        // Reset damageApplied after the delay
        damageApplied = false;
        
        Destroy(this.gameObject);
    }
}