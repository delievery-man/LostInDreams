using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class KeySpawner
{
    public static Vector2Int GetRandomKeyPlace(HashSet<Vector2Int> floorPositions)
    {
        return floorPositions.ToArray()[Random.Range(0, floorPositions.Count-1)];
    }

}
