using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RandomWalkGeneration : AbstractDungeonGenerator
{
    [SerializeField]
    private int iterations = 10; // Number of times to perform the random walk

    [SerializeField]
    private int walkLength = 10; // Number of steps in each random walk

    [SerializeField]
    public bool startRandomlyEachIteration = true; // Determines if each iteration starts at a random position

    // Main function to run procedural generation
    protected override void RunProceduralGeneration()
    {
        HashSet<Vector2Int> floorPositions = RunRandomWalk(startPosition);
        tilemapVisualizer.Clear(); // Clear previous tiles
        tilemapVisualizer.PaintFloorTiles(floorPositions); // Paint new tiles
    }

    // Executes multiple random walks and merges the results
    protected HashSet<Vector2Int> RunRandomWalk(Vector2Int position)
    {
        var currentPosition = position;
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();

        for (int i = 0; i < iterations; i++)
        {
            var path = ProceduralGenAlgorithms.SimpleRandomWalk(currentPosition, walkLength);
            floorPositions.UnionWith(path); // Add new path to the floor positions

            // If enabled, randomly pick a new starting position from existing floor positions
            if (startRandomlyEachIteration)
            {
                currentPosition = floorPositions.ElementAt(Random.Range(0, floorPositions.Count));
            }
        }

        // Clean up unnecessary tiles to refine the map
        floorPositions = ExpandBorderTiles(floorPositions);
        floorPositions = CleanUpTiles(floorPositions);

        return floorPositions;
    }

    // Removes isolated or unnecessary tiles to make the map more structured
    private HashSet<Vector2Int> CleanUpTiles(HashSet<Vector2Int> floorPositions)
    {
        HashSet<Vector2Int> filteredPositions = new HashSet<Vector2Int>(floorPositions);
        var cardinalDirections = ProceduralGenAlgorithms.Direction2D.cardinalDirectionsList;
        var diagonalDirections = ProceduralGenAlgorithms.Direction2D.intermediateDirectionsList;

        bool changesMade;

        do
        {
            HashSet<Vector2Int> toRemove = new HashSet<Vector2Int>();

            // Check each tile to determine if it should be removed
            foreach (var position in filteredPositions.ToList()) // Convert to list to avoid modification during iteration
            {
                int cardinalNeighborCount = 0;
                int intermediateNeighborCount = 0;

                // Count cardinal neighbors (up, down, left, right)
                foreach (var direction in cardinalDirections)
                {
                    if (filteredPositions.Contains(position + direction))
                    {
                        cardinalNeighborCount++;
                    }
                }

                // Count diagonal neighbors (corner tiles)
                foreach (var direction in diagonalDirections)
                {
                    if (filteredPositions.Contains(position + direction))
                    {
                        intermediateNeighborCount++;
                    }
                }

                // Remove tiles with too few neighbors to avoid isolated points
                if (cardinalNeighborCount <= 1 || intermediateNeighborCount <= 1)
                {
                    toRemove.Add(position);
                }
            }

            // Remove marked tiles in one batch
            filteredPositions.ExceptWith(toRemove);

            // Stop loop if no more tiles were removed
            changesMade = toRemove.Count > 0;

        } while (changesMade);

        // Expand the border tiles after cleanup to smooth out edges
        //filteredPositions = ExpandBorderTiles(filteredPositions);

        return filteredPositions;
    }

    // Expands the edges of the generated floor to make it appear more natural
    private HashSet<Vector2Int> ExpandBorderTiles(HashSet<Vector2Int> floorPositions)
    {
        HashSet<Vector2Int> expandedPositions = new HashSet<Vector2Int>(floorPositions);
        var cardinalDirections = ProceduralGenAlgorithms.Direction2D.cardinalDirectionsList;

        HashSet<Vector2Int> newPositions = new HashSet<Vector2Int>();

        // Check each tile and add adjacent empty tiles as border expansion
        foreach (var position in floorPositions)
        {
            foreach (var direction in cardinalDirections)
            {
                Vector2Int newTile = position + direction;

                // If the new tile is not already part of the floor, add it as an expansion tile
                if (!floorPositions.Contains(newTile))
                {
                    newPositions.Add(newTile);
                }
            }
        }

        // Add all new expansion tiles to the floor
        expandedPositions.UnionWith(newPositions);

        return expandedPositions;
    }
}
