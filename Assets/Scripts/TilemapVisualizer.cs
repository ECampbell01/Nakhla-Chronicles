using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapVisualizer : MonoBehaviour
{
    [SerializeField]
    private Tilemap floorTilemap; // Reference to the tilemap where floor tiles will be placed

    [SerializeField]
    private TileBase floorTile; // Tile asset used for floor tiles

    // Paints the given floor positions onto the tilemap
    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions)
    {
        PaintTiles(floorPositions, floorTilemap, floorTile);
    }

    // Loops through positions and paints each tile on the given tilemap
    private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileBase tile)
    {
        foreach (var position in positions)
        {
            PaintSingleTile(tilemap, tile, position);
        }
    }

    // Paints a single tile at the specified position
    private void PaintSingleTile(Tilemap tilemap, TileBase tile, Vector2Int position)
    {
        var tilePosition = tilemap.WorldToCell((Vector3Int)position); // Convert world position to tilemap cell position
        tilemap.SetTile(tilePosition, tile); // Set the tile at the computed position
    }

    // Clears all tiles from the tilemap
    public void Clear()
    {
        floorTilemap.ClearAllTiles();
    }
}
