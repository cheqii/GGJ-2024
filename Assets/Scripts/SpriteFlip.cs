using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFlip : MonoBehaviour
{
    private Transform spriteTransform;

    void Start()
    {
        spriteTransform = GetComponent<Transform>();
    }

    void Update()
    {
        // Check for input to flip the sprite
        float horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput > 0)
        {
            // Moving right
            spriteTransform.localScale = new Vector3(1, 1, 1);
        }
        else if (horizontalInput < 0)
        {
            // Moving left
            spriteTransform.localScale = new Vector3(-1, 1, 1);
        }
    }
}