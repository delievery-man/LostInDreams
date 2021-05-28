using System.Collections.Generic;
using UnityEngine;

public static class WallGenerator
{
    public static void CreateWalls(HashSet<Vector2Int> floorPositions, TileMapVisualizer visualizer)
    {
        var walls = FindWalls(floorPositions, Direction.directions);
        foreach (var wall in walls)
        {
            visualizer.PaintOneWall(wall);
        }
    }

    private static HashSet<Vector2Int> FindWalls(HashSet<Vector2Int> floorPositions, List<Vector2Int> directions)
    {
        var wallPositions = new HashSet<Vector2Int>();
        foreach (var floorTile in floorPositions)
        {
            foreach (var direction in directions)
            {
                var guessedWall = floorTile + direction;
                if (!floorPositions.Contains(guessedWall))
                    wallPositions.Add(guessedWall);
            }
        }

        return wallPositions;
    }
}

