using System.Collections.Generic;
using UnityEngine;

public static class ProceduralGenAlgorithms
{
    // Generates a simple random walk path starting from a given position.
    public static HashSet<Vector2Int> SimpleRandomWalk(Vector2Int startPosition, int walkLength)
    {
        HashSet<Vector2Int> path = new HashSet<Vector2Int>(); // Stores visited positions
        path.Add(startPosition);

        var previousPosition = startPosition;

        for (int i = 0; i < walkLength; i++)
        {
            // Move in a random cardinal direction
            var newPosition = previousPosition + Direction2D.GetRandomCardinalDirection();
            path.Add(newPosition);
            previousPosition = newPosition;
        }
        return path;
    }

    public static class Direction2D
    {
        // List of cardinal directions (Up, Right, Down, Left)
        public static List<Vector2Int> cardinalDirectionsList = new List<Vector2Int>
        {
            new Vector2Int(0,1),  // UP
            new Vector2Int(1,0),  // RIGHT
            new Vector2Int(0, -1), // DOWN
            new Vector2Int(-1, 0)  // LEFT
        };

        // List of diagonal directions (Intermediate directions)
        public static List<Vector2Int> intermediateDirectionsList = new List<Vector2Int>
        {
            new Vector2Int(-1, 1),  // Top-left
            new Vector2Int(1, 1),   // Top-right
            new Vector2Int(-1, -1), // Bottom-left
            new Vector2Int(1, -1)   // Bottom-right
        };

        // Returns a random cardinal direction
        public static Vector2Int GetRandomCardinalDirection()
        {
            return cardinalDirectionsList[Random.Range(0, cardinalDirectionsList.Count)];
        }
    }
}
