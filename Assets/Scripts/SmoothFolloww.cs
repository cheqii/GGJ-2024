using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    public Transform target; // The target to follow
    public float smoothSpeed = 0.125f; // Smoothing factor
    public Vector3 offset; // Offset from the target's position

    void LateUpdate()
    {
        if (target == null)
        {
            // Make sure to assign the target in the Unity Editor
            Debug.LogWarning("Target not assigned!");
            return;
        }

        // Calculate the desired position for the camera
        Vector3 desiredPosition = target.position + offset;

        // Smoothly interpolate between the current position and the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Set the camera's position to the smoothed position
        transform.position = smoothedPosition;

        // Make the camera always look at the target
        transform.LookAt(target);
    }
}