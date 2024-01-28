using System.Collections;
using UnityEngine;
using Cinemachine;

public class CameraZoom : MonoBehaviour
{
    public Transform target; // Target GameObject to zoom in on
    public float zoomInSize = 5f; // Orthographic size when zoomed in
    public float zoomOutSize = 8f; // Default orthographic size
    public float zoomDuration = 1f; // Duration of the zoom in seconds

    private CinemachineVirtualCamera virtualCamera;
    private bool isZooming = false;
    private float originalSize;

    private void Start()
    {
        // Assuming your Cinemachine Virtual Camera is on the same GameObject as this script
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        originalSize = virtualCamera.m_Lens.OrthographicSize;
    }

    private void Update()
    {
        // Check for input or condition to trigger the zoom effect
        if (Input.GetKeyDown(KeyCode.Z) && !isZooming)
        {
            StartCoroutine(ZoomCoroutine());
        }
    }

    public void DoZoom()
    {
        if (!isZooming)
        {
            StartCoroutine(ZoomCoroutine());
        }
    }

    private IEnumerator ZoomCoroutine()
    {
        isZooming = true;

        // Zoom in
        float elapsed = 0f;
        float startSize = virtualCamera.m_Lens.OrthographicSize;
        Vector3 originalPosition = virtualCamera.transform.position;

        while (elapsed < zoomDuration)
        {
            // Adjust orthographic size
            virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(startSize, zoomInSize, elapsed / zoomDuration);

            // Move the camera towards the target during the zoom in (Lerp only x and y)
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, originalPosition.z);
            virtualCamera.transform.position = Vector3.Lerp(originalPosition, targetPosition, elapsed / zoomDuration);

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Ensure final values are set
        virtualCamera.m_Lens.OrthographicSize = zoomInSize;
        virtualCamera.transform.position = new Vector3(target.position.x, target.position.y, originalPosition.z);

        // Wait for a moment (you can customize this duration if needed)
        yield return new WaitForSeconds(1f);

        // Zoom out
        elapsed = 0f;
        while (elapsed < zoomDuration)
        {
            // Adjust orthographic size
            virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(zoomInSize, originalSize, elapsed / zoomDuration);

            // Move the camera back to its original position during the zoom out (Lerp only x and y)
            virtualCamera.transform.position = Vector3.Lerp(new Vector3(target.position.x, target.position.y, originalPosition.z), originalPosition, elapsed / zoomDuration);

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Ensure final values are set
        virtualCamera.m_Lens.OrthographicSize = originalSize;
        virtualCamera.transform.position = originalPosition;

        isZooming = false;
    }
}
