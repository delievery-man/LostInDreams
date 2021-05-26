using System.Collections.Generic;
using UnityEngine;

public static class RandomGenerationAlgs
{
    // Start is called before the first frame update
    public static HashSet<Vector2Int> SimpleRandomWalk(Vector2Int startPos, int walkLen)
    {
        var path = new HashSet<Vector2Int> {startPos};
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
    
        public static List<BoundsInt> BinarySpacePartitioning(BoundsInt spaceToSplit, int minWidth, int minHeight)
    {
        Queue<BoundsInt> roomsQueue = new Queue<BoundsInt>();
        List<BoundsInt> roomsList = new List<BoundsInt>();
        roomsQueue.Enqueue(spaceToSplit);
        while(roomsQueue.Count > 0)
        {
            var room = roomsQueue.Dequeue();
            if(room.size.y >= minHeight && room.size.x >= minWidth)
            {
                if(Random.value < 0.5f)
                {
                    if(room.size.y >= minHeight * 2)
                    {
                        SplitHorizontally(minHeight, roomsQueue, room);
                    }else if(room.size.x >= minWidth * 2)
                    {
                        SplitVertically(minWidth, roomsQueue, room);
                    }else if(room.size.x >= minWidth && room.size.y >= minHeight)
                    {
                        roomsList.Add(room);
                    }
                }
                else
                {
                    if (room.size.x >= minWidth * 2)
                    {
                        SplitVertically(minWidth, roomsQueue, room);
                    }
                    else if (room.size.y >= minHeight * 2)
                    {
                        SplitHorizontally(minHeight, roomsQueue, room);
                    }
                    else if (room.size.x >= minWidth && room.size.y >= minHeight)
                    {
                        roomsList.Add(room);
                    }
                }
            }
        }
        return roomsList;
    }

    private static void SplitVertically(int minWidth, Queue<BoundsInt> roomsQueue, BoundsInt room)
    {
        var xSplit = Random.Range(1, room.size.x);
        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(xSplit, room.size.y, room.size.z));
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x + xSplit, room.min.y, room.min.z),
            new Vector3Int(room.size.x - xSplit, room.size.y, room.size.z));
        roomsQueue.Enqueue(room1);
        roomsQueue.Enqueue(room2);
    }

    private static void SplitHorizontally(int minHeight, Queue<BoundsInt> roomsQueue, BoundsInt room)
    {
        var ySplit = Random.Range(1, room.size.y);
        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(room.size.x, ySplit, room.size.z));
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x, room.min.y + ySplit, room.min.z),
            new Vector3Int(room.size.x, room.size.y - ySplit, room.size.z));
        roomsQueue.Enqueue(room1);
        roomsQueue.Enqueue(room2);
    }
}

public static class Direction
{
    public static List<Vector2Int> directions = new List<Vector2Int>
    {
        Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right
    };

    public static List<Vector2> directionsDiag = new List<Vector2>
    {
        Vector2.up, Vector2.down, Vector2.left, Vector2.right, (Vector2.down + Vector2.left).normalized,
        (Vector2.right + Vector2.down).normalized, (Vector2.up + Vector2.right).normalized, (Vector2.up + Vector2.left).normalized
    };

    public static Vector2Int GetRandomDirection()
    {
        return directions[Random.Range(0, directions.Count)];
    }
    
    public static Vector2 GetRandomDiagDirection()
    {
        return directionsDiag[Random.Range(0, directionsDiag.Count)];
    }
    
}