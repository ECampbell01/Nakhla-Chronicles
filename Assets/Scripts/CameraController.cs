using UnityEngine;

public class CameraController: MonoBehaviour
{
    public int mininumZoom = 3;
    public int maximumZoom = 12;
    public float zoomSpeed = 4;
    public float lerpSpeed = 5;

    public Transform target;
    Vector3 playerPosition;

    Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void FixedUpdate()
    {
        HandlePlayerFollow();
    }

    void Update()
    {
        HandleZoom();
    }

    private void HandlePlayerFollow()
    {
        playerPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, playerPosition, Time.fixedDeltaTime * lerpSpeed);
    }

    private void HandleZoom()
    {
        float zoom = cam.orthographicSize;
        zoom -= Input.GetAxis("Mouse ScrollWheel") * 5;
        zoom = Mathf.Clamp(zoom, 2, 10);
        cam.orthographicSize = zoom;
    }
}
