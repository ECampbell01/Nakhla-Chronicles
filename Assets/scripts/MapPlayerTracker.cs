using System;
using UnityEngine;





public class MapPlayerTracker : MonoBehaviour
{

    public RectTransform hubWorldMap;
    public Transform player;
    public RectTransform playerIcon;
    private float mapWidth;
    private float mapHeight;
    public float hubWorldWidth = 33f;
    public float hubWorldHeight = 34f;

    void Start()
    {
        mapWidth = hubWorldMap.rect.width;
        mapHeight = hubWorldMap.rect.height;
    }


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
            float mapY = playerPosition.y * scaleY;

            // Set the player icon position
            playerIcon.anchoredPosition = new Vector2(mapX, mapY);

            // Debug logs to track positions
            // Debug.Log($"Player Position: {playerPosition}, Clamped Y: {clampedY}, ScaleX: {scaleX}, ScaleY: {scaleY}, Map Position: ({mapX}, {mapY})");
            Debug.Log($"Player Icon Position: {playerIcon.anchoredPosition}");
            Debug.Log($"Player Icon Position (Before Check): {playerIcon.anchoredPosition}");

        }
    }








}


