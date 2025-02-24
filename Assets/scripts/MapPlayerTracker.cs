using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class MapPlayerTracker : MonoBehaviour
{
    // UI map representation
    public RectTransform hubWorldMap;

    // Player's transform in the world
    public Transform player;

    // UI icon representing the player on the map
    public RectTransform playerIcon;

    // Dimensions of the UI map
    private float mapWidth;
    private float mapHeight;

    // Dimensions of the tile-based hub world
    private float hubWorldWidth;
    private float hubWorldHeight;

    void Start()
    {
        // Get UI map dimensions in pixels
        mapWidth = hubWorldMap.rect.width;
        mapHeight = hubWorldMap.rect.height;

        // Find the tilemap GameObject in the scene
        GameObject GroundBackground = GameObject.Find("GroundBackground");

        // Get the Tilemap component
        Tilemap tilemapSize = GroundBackground.GetComponent<Tilemap>();

        // Get the total size of the tilemap in tiles
        BoundsInt bounds = tilemapSize.cellBounds;
        hubWorldWidth = bounds.size.x;  // Number of tiles in width
        hubWorldHeight = bounds.size.y; // Number of tiles in height

        Debug.Log($"Tilemap Width: {hubWorldWidth}, Height: {hubWorldHeight}");
    }

    void Update()
    {
        // Ensure player and player icon exist before proceeding
        if (player != null && playerIcon != null)
        {
            // Get player's position in world space
            Vector3 playerPosition = player.position;

            // Calculate scaling factors from world to UI map
            float scaleX = mapWidth / hubWorldWidth;
            float scaleY = mapHeight / hubWorldHeight;

            // Convert player world position to UI map position
            float mapX = playerPosition.x * scaleX;
            float mapY = playerPosition.y * scaleY;

            // Set the player icon's position on the UI map
            playerIcon.anchoredPosition = new Vector2(mapX, mapY);

            // Debug logs to track positions
            Debug.Log($"Player Icon Position: {playerIcon.anchoredPosition}");
            Debug.Log($"Player Icon Position (Before Check): {playerIcon.anchoredPosition}");
        }
    }
}
