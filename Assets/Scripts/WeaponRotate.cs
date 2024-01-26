using System.Collections;
using UnityEngine;

public class WeaponRotate : MonoBehaviour
{
    public float rotationSpeed = 45f; // Adjust the speed of rotation
    public float rotationBackSpeed = 30f; // Adjust the speed of rotation back to default

    private Quaternion defaultRotation;
    private bool isRotating = false;

    // Variable to store the desired rotation angle (set in the Inspector)
    public float desiredRotationAngle = 90f;
    public float delayBetweenRotations = 1f; // Adjust the delay between rotations

    void Start()
    {
        // Store the default rotation of the object
        defaultRotation = transform.rotation;
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && !isRotating)
        {
            // Rotate the object smoothly using Lerp with the desired rotation angle
            float rotationDirection = (horizontalInput > 0) ? 1f : -1f;
            StartCoroutine(RotateObjectCoroutine(rotationDirection * desiredRotationAngle, rotationSpeed, rotationBackSpeed, delayBetweenRotations));
        }
    }

    IEnumerator RotateObjectCoroutine(float desiredRotation, float rotateSpeed, float rotateBackSpeed, float delayBetweenRotations)
    {
        isRotating = true;

        // Rotate the object
        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = startRotation * Quaternion.Euler(Vector3.forward * desiredRotation);
        float duration = Mathf.Abs(desiredRotation) / rotateSpeed; // Adjust duration based on desired rotation

        float t = 0;

        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            transform.rotation = Quaternion.Lerp(startRotation, targetRotation, t);
            yield return null;
        }

        // Wait for a specified delay
        yield return new WaitForSeconds(delayBetweenRotations);

        // Rotate the object back to the default rotation
        t = 0;
        duration = Mathf.Abs(desiredRotation) / rotateBackSpeed; // Adjust duration based on desired rotation back
        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            transform.rotation = Quaternion.Lerp(targetRotation, defaultRotation, t);
            yield return null;
        }

        isRotating = false;
    }
}
