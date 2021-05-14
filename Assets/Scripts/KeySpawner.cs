using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class KeySpawner
{
    public static Vector2Int GetKeyPlace(Vector2Int currentCenter, List<Vector2Int> roomCenters)
    {
        var farest = Vector2Int.zero;
        float maxDistance = float.MinValue;

        foreach (var pos in roomCenters)
        {
            var currentDistance = Vector2.Distance(pos, currentCenter);
            if (currentDistance > maxDistance)
            {
                maxDistance = currentDistance;
                farest = pos; 
            }
        }

        return farest;
    }

}
