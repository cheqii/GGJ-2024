using UnityEngine;

public class MoveToMiddle : MonoBehaviour
{
    public Transform transformA;
    public Transform transformB;

    void Start()
    {
        // Check if both transforms are assigned
        if (transformA == null || transformB == null)
        {
            Debug.LogError("Transform A or Transform B not assigned!");
        }
    }

    void Update()
    {
        // Check if both transforms are assigned
        if (transformA != null && transformB != null)
        {
            // Calculate the midpoint between transformA and transformB
            Vector3 midpoint = (transformA.position + transformB.position) / 2f;

            // Set the position of the game object to the midpoint
            transform.position = new Vector3(midpoint.x, midpoint.y, transform.position.z);
        }
    }
}