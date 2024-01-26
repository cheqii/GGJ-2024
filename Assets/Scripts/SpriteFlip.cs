using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFlip : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Check for input to flip the sprite
        float horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput > 0)
        {
            // Moving right
            spriteRenderer.flipX = false;
        }
        else if (horizontalInput < 0)
        {
            // Moving left
            spriteRenderer.flipX = true;
        }
    }
}