using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoomFirstDungeonGenerator : RandomWalkGeneration
{
    [SerializeField]
    private int minRoomWidth = 4, minRoomHeight = 4;
    [SerializeField]
    private int dungeonWidth = 20, dungeonHeight = 20;
    [SerializeField]
    [Range(0, 10)]
    private int offset = 1;
    [SerializeField]
    private bool randomWalkRooms = false;

    [SerializeField]
    private GameObject[] enemyPrefabs;
    [SerializeField]
    private int minEnemiesPerRoom = 1, maxEnemiesPerRoom = 3;
    [SerializeField]
    private float spawnRadius = 2.0f;
    [SerializeField]
    private GameObject playerPrefab;


    private void Start()
    {
        RunProceduralGeneration();
    }


    protected override void RunProceduralGeneration()
    {
        CreateRooms();
    }

    private void CreateRooms()
    {
        var roomsList = ProceduralGenAlgorithms.BinarySpacePartitioning(new BoundsInt((Vector3Int)startPosition,
            new Vector3Int(dungeonWidth, dungeonHeight, 0)), minRoomWidth, minRoomHeight);

        Debug.Log($"Rooms generated: {roomsList.Count}");
        if (roomsList.Count == 0)
        {
            Debug.LogError("No rooms were generated! Check BSP settings.");
            return;
        }

        HashSet<Vector2Int> floor = randomWalkRooms ? CreateRoomsRandomly(roomsList) : CreateSimpleRooms(roomsList);

        List<Vector2Int> roomCenters = new List<Vector2Int>();

        foreach (var room in roomsList)
        {
            Vector2Int center = new Vector2Int(
                Mathf.RoundToInt(room.center.x),
                Mathf.RoundToInt(room.center.y)
            );

            roomCenters.Add(center);
            Debug.Log($"Room center added: {center}");
        }

        if (roomCenters.Count > 0)
        {
            SpawnPlayer(roomCenters[0]);
        }
        else
        {
            Debug.LogError("No valid room centers found, skipping player spawn.");
        }

        if (roomCenters.Count > 2)
        {
            SpawnEnemies(roomCenters);
        }
        else
        {
            Debug.LogWarning("Not enough rooms to exclude first and last. Skipping enemy spawn.");
        }

        HashSet<Vector2Int> corridors = ConnectRooms(roomCenters);
        floor.UnionWith(corridors);

        tilemapVisualizer.PaintFloorTiles(floor);
    }

    private void SpawnPlayer(Vector2Int spawnPosition)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector3 playerSpawnPos = new Vector3(spawnPosition.x, spawnPosition.y, 0);
        player.transform.position = playerSpawnPos;
    }


    private void SpawnEnemies(List<Vector2Int> roomCenters)
    {
        if (roomCenters.Count <= 2)
        {
            Debug.LogWarning("Not enough rooms to exclude first and last. Skipping enemy spawn.");
            return;
        }

        List<Vector2Int> validRooms = roomCenters.GetRange(1, roomCenters.Count - 2);

        foreach (var center in validRooms)
        {
            int enemyCount = Random.Range(minEnemiesPerRoom, maxEnemiesPerRoom + 1);
            for (int i = 0; i < enemyCount; i++)
            {
                Vector2 spawnPosition = (Vector2)center + Random.insideUnitCircle * spawnRadius;
                GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

                Instantiate(enemyPrefab, new Vector3(spawnPosition.x, spawnPosition.y, 0), Quaternion.identity);
            }
        }
    }

    private HashSet<Vector2Int> CreateRoomsRandomly(List<BoundsInt> roomsList)
    {
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        for (int i = 0; i < roomsList.Count; i++)
        {
            var roomBounds = roomsList[i];
            var roomCenter = new Vector2Int(Mathf.RoundToInt(roomBounds.center.x), Mathf.RoundToInt(roomBounds.center.y));
            var roomFloor = RunRandomWalk(roomCenter);
            foreach (var position in roomFloor)
            {
                if (position.x >= (roomBounds.xMin + offset) && position.x <= (roomBounds.xMax - offset) && position.y >= (roomBounds.yMin -
                    offset) && position.y <= (roomBounds.yMax - offset))
                {
                    floor.Add(position);
                }
            }
        }
        return floor;
    }

    private HashSet<Vector2Int> ConnectRooms(List<Vector2Int> roomCenters)
    {
        HashSet<Vector2Int> corridors = new HashSet<Vector2Int>();
        var currentRoomCenter = roomCenters[Random.Range(0, roomCenters.Count)];
        roomCenters.Remove(currentRoomCenter);

        while (roomCenters.Count > 0)
        {
            Vector2Int closest = FindClosestPointTo(currentRoomCenter, roomCenters);
            roomCenters.Remove(closest);
            HashSet<Vector2Int> newCorridor = CreateCorridor(currentRoomCenter, closest);
            currentRoomCenter = closest;
            corridors.UnionWith(newCorridor);
        }
        return corridors;
    }

    private HashSet<Vector2Int> CreateCorridor(Vector2Int currentRoomCenter, Vector2Int destination)
    {
        HashSet<Vector2Int> corridor = new HashSet<Vector2Int>();
        var position = currentRoomCenter;

        void AddCorridorTiles(Vector2Int pos, bool horizontal)
        {
            corridor.Add(pos); // Center tile
            if (horizontal)
            {
                corridor.Add(pos + Vector2Int.up);
                corridor.Add(pos + Vector2Int.up * 2);
                corridor.Add(pos + Vector2Int.down);
                corridor.Add(pos + Vector2Int.down * 2);
            }
            else
            {
                corridor.Add(pos + Vector2Int.left);
                corridor.Add(pos + Vector2Int.left * 2);
                corridor.Add(pos + Vector2Int.right);
                corridor.Add(pos + Vector2Int.right * 2);
            }
        }

        while (position.y != destination.y)
        {
            position += (destination.y > position.y) ? Vector2Int.up : Vector2Int.down;
            AddCorridorTiles(position, false);
        }

        while (position.x != destination.x)
        {
            position += (destination.x > position.x) ? Vector2Int.right : Vector2Int.left;
            AddCorridorTiles(position, true);
        }

        return corridor;
    }

    private Vector2Int FindClosestPointTo(Vector2Int currentRoomCenter, List<Vector2Int> roomCenters)
    {
        Vector2Int closest = Vector2Int.zero;
        float distance = float.MaxValue;
        foreach (var position in roomCenters)
        {
            float currentDistance = Vector2.Distance(currentRoomCenter, position);
            if (currentDistance < distance)
            {
                distance = currentDistance;
                closest = position;
            }
        }
        return closest;
    }

    private HashSet<Vector2Int> CreateSimpleRooms(List<BoundsInt> roomsList)
    {
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        foreach (var room in roomsList)
        {
            for (int col = offset; col < room.size.x - offset; col++)
            {
                for (int row = offset; row < room.size.y - offset; row++)
                {
                    Vector2Int position = (Vector2Int)room.min + new Vector2Int(col, row);
                    floor.Add(position);
                }
            }
        }
        return floor;
    }
}
