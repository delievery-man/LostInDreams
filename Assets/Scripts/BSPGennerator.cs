using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using Random = UnityEngine.Random;

public class BSPGennerator : RandomWalkGenerator
{
    [SerializeField]
    private bool randomWalkRooms = false;


    private Vector2Int farest;
    public Transform Enemy;
    private List<Vector2Int> roomCenters = new List<Vector2Int>();
    private List<BoundsInt> roomList;
    public int counter;
    private BoundsInt firstRoom;
    public Vector3 currRoom;
    private int TotalEnemyCount = 8;
    public Dictionary<Vector3, List<int>>enemyCounters = new Dictionary<Vector3, List<int>>();
    private bool FirstWaveKilled = false;

    private void Awake()
    {
        Clear(); 
        CreateRooms();
    }

    void Start()
    {
        Enemy.GetComponent<EnemyFollowing>().target = Player.transform;
        foreach (var center in roomList
            .Select(x => x.center)
            .ToList())
        {
            enemyCounters[new Vector3(0f, 0f, 0f)] = new List<int>(2){0, 0};
            if (!enemyCounters.ContainsKey(center))
            {
                enemyCounters[center] =new List<int>(2){0, 0};
            }
        }
    }

    private void FixedUpdate()
    {
        var pos = roomList
            .Where(x=> x!= firstRoom)
            .Select(x => x.center)
            .FirstOrDefault(y =>
            {
                var min = Math.Min(_mapSettings.minRoomHeight, _mapSettings.minRoomWidth) / 2;
                
                return Vector3.Distance(Player.transform.position, y) <=
                       min;
            });
        if (pos != Vector3.zero)
        {
            currRoom = pos;
        }
        
        if (pos != Vector3.zero && enemyCounters[pos][0] ==0  && new Vector3(playerPos.x, playerPos.y, 0f)!=pos)
        {
            
            if (enemyCounters[pos][1]<=3)
            {
                foreach (var random2d in Direction.directionsDiag)
                {

                    var enemyPos = pos + new Vector3(random2d.x, random2d.y, 0f)*4;

                    Instantiate(Enemy,enemyPos , Quaternion.identity );
                    currRoom = pos;
                    enemyCounters[pos][0]++;
                }
                enemyCounters[pos][1]++;
            }

            
        }

    }

    public void CreateRooms()
    {
        roomList = RandomGenerationAlgs.BinarySpacePartitioning(new BoundsInt((Vector3Int)startPos, new Vector3Int(_mapSettings.dungeonWidth, _mapSettings.dungeonHeight, 0)), _mapSettings.minRoomWidth, _mapSettings.minRoomHeight);
        firstRoom = roomList.First();
        var floor = CreateSimpleRooms(roomList);
        foreach (var room in roomList)
        {
               roomCenters.Add((Vector2Int) Vector3Int.RoundToInt(room.center)); 
        }
        
        var corridors = ConnectRooms(roomCenters);
        floor.UnionWith(corridors);

        visualizer.PaintFloor(floor);
        // visualizer.PaintKey(keyPos);
        visualizer.SpawnPlayer(Player, playerPos);

        visualizer.SpawnKey(Key, keyPos);
        visualizer.SpawnExit(Exit, exitPos);
        WallGenerator.CreateWalls(floor, visualizer);
        
    }



    private HashSet<Vector2Int> ConnectRooms(List<Vector2Int> roomCenters)
    {
        var corridors = new HashSet<Vector2Int>();
        var currentCenter = roomCenters[Random.Range(0, roomCenters.Count)];
        roomCenters.Remove(currentCenter);

        while (roomCenters.Count > 0)
        {
            var closest = FindClosestRoom(currentCenter, roomCenters);
            roomCenters.Remove(closest);
            var newCorridor = new HashSet<Vector2Int>();
            for (int i = 0; i < 2; i++)
            {
                newCorridor = CreateConnection(currentCenter +Vector2Int.left*i, closest+Vector2Int.left*i);
                currentCenter = closest;
                corridors.UnionWith(newCorridor);
            }
           
        }

        return corridors;
    }

    private HashSet<Vector2Int> CreateConnection(Vector2Int currentCenter, Vector2Int closest)
    {
        var corridor = new HashSet<Vector2Int>();
        var position = currentCenter;
        var pos2Up = Vector2Int.zero;
        var pos2Down = Vector2Int.zero;
        corridor.Add(position);
        while (position.y != closest.y)
        {
            if(closest.y > position.y)
            {
                position += Vector2Int.up;
                
            }
            else if(closest.y < position.y)
            {
                position += Vector2Int.down;
            }
            pos2Up = position + Vector2Int.left;
            corridor.Add(pos2Up);
            pos2Up = position + Vector2Int.right;
            corridor.Add(pos2Up);

            corridor.Add(position);
            

        }
        
        while (position.x != closest.x)
        {

                if (closest.x > position.x)
                {
                    position += Vector2Int.right;
                }else if(closest.x < position.x)
                {
                    position += Vector2Int.left;
                }
                pos2Down = position + Vector2Int.up;
                corridor.Add(pos2Down);
                pos2Down = position + Vector2Int.down;
                corridor.Add(pos2Down);


                corridor.Add(position);
                
        }
        
        return corridor;
    }

    private Vector2Int FindClosestRoom(Vector2Int currentCenter, List<Vector2Int> roomCenters)
    {
        var closest = Vector2Int.zero;
        float minDistance = float.MaxValue;

        foreach (var pos in roomCenters)
        {
            var currentDistance = Vector2.Distance(pos, currentCenter);
            if (currentDistance<minDistance)
            {
                minDistance = currentDistance;
                closest = pos; 
            }
        }

        return closest;
        
    }

    private HashSet<Vector2Int> CreateSimpleRooms(List<BoundsInt> roomsList)
    {
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        foreach (var room in roomsList)
        {
            for (int col = _mapSettings.offset; col < room.size.x - _mapSettings.offset; col++)
            {
                for (int row = _mapSettings.offset; row < room.size.y - _mapSettings.offset; row++)
                {
                    Vector2Int position = (Vector2Int)room.min + new Vector2Int(col, row);
                    floor.Add(position);
                }
            }
        }

        var center = roomsList.First().center;
        playerPos = (Vector2Int) Vector3Int.RoundToInt(center);
        keyPos = KeySpawner.GetKeyPlace(playerPos, roomsList.Select(x => (Vector2Int) Vector3Int.RoundToInt(x.center)).ToList());
        exitPos =  (Vector2Int)Vector3Int.RoundToInt(new Vector3(center.x - 3, center.y));   
        return floor;
    }
    

}

[CustomEditor(typeof(BSPGennerator))]
public class LevelGeneratorEditor2 : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        //Reference to our script
        BSPGennerator levelGen = (BSPGennerator) target;

        //Only show the mapsettings UI if we have a reference set up in the editor
        if (levelGen._mapSettings != null)
        {
            Editor mapSettingEditor = CreateEditor(levelGen._mapSettings);
            mapSettingEditor.OnInspectorGUI();

            if (GUILayout.Button("Generate"))
            {
                levelGen.Clear();
                levelGen.CreateRooms();
            }

            if (GUILayout.Button("Clear"))
            {
                levelGen.Clear();
            }
        }
    }
}
