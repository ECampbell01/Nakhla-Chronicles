using UnityEngine;

public class CameraController: MonoBehaviour
{
    public int mininumZoom = 3;
    public int maximumZoom = 12;
    public float zoomSpeed = 4;
    public float lerpSpeed = 5;

    public Transform target;
    Vector3 playerPosition;

    void FixedUpdate()
    {
        HandlePlayerFollow();
    }

    private void Update()
    {
        HandleZoom();
    }

    private void HandlePlayerFollow()
    {
        playerPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, playerPosition, Time.deltaTime * lerpSpeed);
    }

    private void HandleZoom()
    {
        float zoom = Camera.main.orthographicSize;
        zoom -= Input.GetAxis("Mouse ScrollWheel") * 5;
        zoom = Mathf.Clamp(zoom, 2, 10);
        Camera.main.orthographicSize = zoom;
    }
}
