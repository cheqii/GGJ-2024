using System.Collections;
using UnityEngine;
using Cinemachine;

public class CameraZooming : MonoBehaviour
{
    public Transform target; // Target GameObject to zoom in on
    public float zoomInSize = 5f; // Orthographic size when zoomed in
    public float zoomOutSize = 8f; // Default orthographic size
    public float zoomDuration = 1f; // Duration of the zoom in seconds

    private CinemachineVirtualCamera virtualCamera;
    private bool isZooming = false;

    private void Start()
    {
        // Assuming your Cinemachine Virtual Camera is on the same GameObject as this script
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    private void Update()
    {
        // Check for input or condition to trigger the zoom effect
        if (Input.GetKeyDown(KeyCode.Z) && !isZooming)
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
        while (elapsed < zoomDuration)
        {
            virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(startSize, zoomInSize, elapsed / zoomDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        virtualCamera.m_Lens.OrthographicSize = zoomInSize;

        // Wait for a moment (you can customize this duration if needed)
        yield return new WaitForSeconds(1f);

        // Zoom out
        elapsed = 0f;
        while (elapsed < zoomDuration)
        {
            virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(zoomInSize, zoomOutSize, elapsed / zoomDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        virtualCamera.m_Lens.OrthographicSize = zoomOutSize;

        isZooming = false;
    }
}