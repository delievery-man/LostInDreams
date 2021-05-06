using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RandomGenerationAlgs
{
    // Start is called before the first frame update
    public static HashSet<Vector2Int> SimpleRandomWalk(Vector2Int startPos, int walkLen)
    {
        var path = new HashSet<Vector2Int>(){startPos};
        var prevPos = startPos;
        for (int i = 0; i < walkLen; i++)
        {
            var newPos = prevPos + Direction.GetRandomDirection();
            path.Add(newPos);
            prevPos = newPos;
        }

        return path;
    }

    public static List<Vector2Int> RandomWalkCorridor(Vector2Int startPos, int corridorLen)
    {
        var corridor = new List<Vector2Int>();
        var direction = Direction.GetRandomDirection();
        var currentPos = startPos;

        for (int i = 0; i < corridorLen; i++)
        {
            currentPos += direction;
            corridor.Add(currentPos);
            if (direction == Vector2Int.down || direction == Vector2Int.up)
            {
                corridor.Add(currentPos+Vector2Int.left);
                corridor.Add(currentPos+Vector2Int.right);
            }
            else
            {
                corridor.Add(currentPos+Vector2Int.down);
                corridor.Add(currentPos+Vector2Int.up);
            }

        }

        return corridor;
    }
}

public static class Direction
{
    public static List<Vector2Int> directions = new List<Vector2Int>()
    {
        Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right
    };

    public static Vector2Int GetRandomDirection()
    {
        return directions[Random.Range(0, directions.Count)];
    }
}