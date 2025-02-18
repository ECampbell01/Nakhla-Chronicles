using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RandomWalkGeneration : MonoBehaviour
{
    [SerializeField]
    protected Vector2Int startPosition = Vector2Int.zero;

    [SerializeField]
    private int iterations = 10;

    [SerializeField]
    private int walkLength = 10;

    [SerializeField]
    public bool startRandomlyEachIteration = true;

    [SerializeField]
    private TilemapVisualizer tilemapVisualizer;

    public void RunProceduralGeneration()
    {
        HashSet<Vector2Int> floorPositions = RunRandomWalk();
        tilemapVisualizer.Clear();
        tilemapVisualizer.PaintFloorTiles(floorPositions);
    }

    protected HashSet<Vector2Int> RunRandomWalk()
    {
        var currentPosition = startPosition;
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        for (int i = 0; i < iterations; i++)
        {
            var path = ProceduralGenAlgorithms.SimpleRandomWalk(currentPosition, walkLength);
            floorPositions.UnionWith(path);
            if (startRandomlyEachIteration)
            {
                currentPosition = floorPositions.ElementAt(Random.Range(0, floorPositions.Count));
            }
        }

        floorPositions = CleanUpTiles(floorPositions);

        return floorPositions;
    }

    private HashSet<Vector2Int> CleanUpTiles(HashSet<Vector2Int> floorPositions)
    {
        HashSet<Vector2Int> filteredPositions = new HashSet<Vector2Int>(floorPositions);
        var cardinalDirections = ProceduralGenAlgorithms.Direction2D.cardinalDirectionsList;
        var diagonalDirections = ProceduralGenAlgorithms.Direction2D.intermediateDirectionsList;

        bool changesMade;

        do
        {
            HashSet<Vector2Int> toRemove = new HashSet<Vector2Int>();

            foreach (var position in filteredPositions.ToList()) // Convert to list to avoid modifying during iteration
            {
                int cardinalNeighborCount = 0;
                int intermediateNeighborCount = 0;

                // Count cardinal neighbors
                foreach (var direction in cardinalDirections)
                {
                    if (filteredPositions.Contains(position + direction))
                    {
                        cardinalNeighborCount++;
                    }
                }

                // Count diagonal neighbors
                foreach (var direction in diagonalDirections)
                {
                    if (filteredPositions.Contains(position + direction))
                    {
                        intermediateNeighborCount++;
                    }
                }

                // Apply new removal conditions
                if (cardinalNeighborCount <= 1 || intermediateNeighborCount <= 1)
                {
                    toRemove.Add(position);
                }
            }

            // Remove tiles in one batch operation
            filteredPositions.ExceptWith(toRemove);

            // Stop when no more changes are made
            changesMade = toRemove.Count > 0;

        } while (changesMade);

        filteredPositions = ExpandBorderTiles(filteredPositions);

        return filteredPositions;
    }

    private HashSet<Vector2Int> ExpandBorderTiles(HashSet<Vector2Int> floorPositions)
    {
        HashSet<Vector2Int> expandedPositions = new HashSet<Vector2Int>(floorPositions);
        var cardinalDirections = ProceduralGenAlgorithms.Direction2D.cardinalDirectionsList;

        HashSet<Vector2Int> newPositions = new HashSet<Vector2Int>();

        foreach (var position in floorPositions)
        {
            foreach (var direction in cardinalDirections)
            {
                Vector2Int newTile = position + direction;

                // If the new tile is not already part of floorPositions, it's an expansion tile
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
