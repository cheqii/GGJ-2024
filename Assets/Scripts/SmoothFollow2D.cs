using UnityEngine;

public class SmoothFollow2D : MonoBehaviour
{
    public Transform target; // The player or object to follow
    public float smoothSpeed = 5f; // The speed of the smooth follow
    public Vector3 offset = new Vector3(0f, 0f, -10f); // Offset from the target

    void LateUpdate()
    {
        if (target == null)
        {
            Debug.LogWarning("Target not set for SmoothFollow2D script.");
            return;
        }

        // Calculate the desired position with the offset
        Vector3 desiredPosition = target.position + offset;

        // Smoothly interpolate towards the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // Set the position of the camera to the smoothed position
        transform.position = smoothedPosition;
    }
}
