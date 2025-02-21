using UnityEngine;



public class MapPlayerTracker : MonoBehaviour
{
    public Transform player;
    public RectTransform playerIcon;
    public float mapWidth = 643f;
    public float mapHeight = 722f;
    public float hubWorldWidth = 33f;
    public float hubWorldHeight = 34f;
    void Update()
    {
        if (player != null && playerIcon != null)
        {
            Vector3 playerPosition = player.position;

            // Calculate the scaling factors
            float scaleX = mapWidth / hubWorldWidth;  // e.g., 643.6908 / 34
            float scaleY = mapHeight / hubWorldHeight; // e.g., 722.1547 / 35

            // Convert world position to UI map position using scaling factors
            float mapX = playerPosition.x * scaleX;

            // Clamp the Y value and invert for UI
            float clampedY = Mathf.Clamp(playerPosition.y, 0, hubWorldHeight); // Ensure it's within the height range
            float mapY = mapHeight + (clampedY * scaleY); // Adjust Y position for UI

            // Set the player icon position
            playerIcon.anchoredPosition = new Vector2(mapX, mapY);

            // Debug logs to track positions
            Debug.Log($"Player Position: {playerPosition}, Clamped Y: {clampedY}, ScaleX: {scaleX}, ScaleY: {scaleY}, Map Position: ({mapX}, {mapY})");
            Debug.Log($"Player Icon Position: {playerIcon.anchoredPosition}");
            Debug.Log($"Player Icon Position (Before Check): {playerIcon.anchoredPosition}");

        }
    }

    void Start()
    {
        playerIcon.position = new Vector3(-50, -50, 0);
    }








}


