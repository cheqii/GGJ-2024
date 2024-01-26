using System.Collections;
using UnityEngine;

public class SpringRotation : MonoBehaviour
{
    public float rotationSpeed = 60f;
    public float oscillationFrequency = 1f;
    public float smoothReturnSpeed = 10f;

    private bool isRotating = false;
    private Quaternion initialRotation;

    private void Start()
    {
        initialRotation = transform.rotation;
    }

    private void Update()
    {
        if (isRotating)
        {
            RotateObject();
        }
    }

    void RotateObject()
    {
        float angle = Mathf.Sin(Time.time * oscillationFrequency) * rotationSpeed;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    public void StartRotation()
    {
        StopAllCoroutines();
        isRotating = true;
    }

    public void StopRotation()
    {
        StartCoroutine(SmoothReturnToInitialRotation());
    }

    IEnumerator SmoothReturnToInitialRotation()
    {
        while (Quaternion.Angle(transform.rotation, initialRotation) > 0.01f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, initialRotation, smoothReturnSpeed * Time.deltaTime);
            yield return null;
        }

        transform.rotation = initialRotation;
        isRotating = false;
    }
}