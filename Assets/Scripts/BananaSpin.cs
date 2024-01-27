using UnityEngine;

public class BananaSpin : MonoBehaviour
{
    public float rotationSpeed = 500f;

    void Update()
    {
        // Rotate the object around its forward axis (Z-axis)
        transform.Rotate(rotationSpeed * Time.deltaTime * Vector3.forward);
    }
}