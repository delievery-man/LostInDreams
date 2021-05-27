#if UNITY_EDITOR
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using static System.Math;
using Random = UnityEngine.Random;

public class BSPGenerator: MonoBehaviour
{
    [SerializeField]
    private bool randomWalkRooms;
    [SerializeField] protected Vector2Int startPos = Vector2Int.zero;

    [FormerlySerializedAs("Player")] [SerializeField] protected Transform player;
    [FormerlySerializedAs("Exit")] [SerializeField] protected Transform exit;

    [FormerlySerializedAs("_visualizer")] [SerializeField]
    public TileMapVisualizer visualizer;

    private static Vector2Int KeyPos { get; set; }
    [NonSerialized] private Vector2Int playerPos;
    public static Vector2Int ExitPos { get; set; }

    public int minRoomWidth;
    public int minRoomHeight;
    public int dungeonWidth;
    public int dungeonHeight;
    public int offset;
    public Transform enemy;
    public Transform boss;
    public List<Vector2Int> roomCenters = new List<Vector2Int>();
    private List<BoundsInt> roomList;
    private BoundsInt firstRoom;
    [NonSerialized] public Vector3Int currRoom;
    [SerializeField] private int totalEnemyCount = 8;
    public Dictionary<Vector3Int, List<int>> enemyCounters;
    [SerializeField] private bool firstWaveKilled;
    [FormerlySerializedAs("Trap")] public GameObject trap;
    [NonSerialized] private Vector3 bossSpawn;
    private bool bossSpawned;

    public BSPGenerator(Transform enemy, Transform boss)
    {
        this.enemy = enemy;
        this.boss = boss;
    }

    private void Awake()
    {
        Clear(); 
        
    }

    void Start()
    {
        CreateRooms();
        enemy.GetComponent<EnemyFollowing>().target = player.transform;
        enemyCounters = new Dictionary<Vector3Int, List<int>>();
        foreach (var center in roomList
            .Select(x => Vector3Int.RoundToInt(x.center))
            .ToList())
        {
            enemyCounters[new Vector3Int(0, 0, 0)] = new List<int>(2){0, 0};
            if (!enemyCounters.ContainsKey(center))
            {
                enemyCounters[center] =new List<int>(2){0, 0};
            }
        }

        bossSpawn = new Vector3(KeyPos.x, KeyPos.y, 0f);
    }

    private void FixedUpdate()
    {
        var spawnRoom = roomList
            .Where(x=> x!= firstRoom)
            .FirstOrDefault(y =>
            {
                var min = Min(minRoomHeight, minRoomWidth) / 2;
                
                return Vector3.Distance(player.transform.position, y.center) <=
                       min;
            });
      
        var spawnCenter = Vector3Int.RoundToInt(spawnRoom.center);
        if (spawnCenter != Vector3Int.zero)
            currRoom = spawnCenter;
        
        
        if (spawnCenter != Vector3.zero && enemyCounters[spawnCenter][0] == 0  && new Vector3(playerPos.x, playerPos.y, 0f)!=spawnCenter)
        {
            
            
            if (enemyCounters[spawnCenter][1]<3)
            {
                if (enemyCounters[spawnCenter][1] == 0)
                
                    foreach (var dir in Direction.directions)
                    {

                        var trapPos = spawnCenter +
                                      new Vector3(dir.x, dir.y,
                                          0f) * (Mathf.Min(spawnRoom.size.x / 2.5f, spawnRoom.size.y / 2.5f) - offset);
                        if (Vector2.Distance(trapPos, player.position) <=
                            Min(minRoomHeight, minRoomWidth) / 2)
                            continue;
                        
                        Instantiate(trap,trapPos , Quaternion.identity );
                    }
                
                
                foreach (var random2d in Direction.directionsDiag)
                {

                    var enemyPos = spawnCenter + new Vector3(random2d.x, random2d.y, 0f).normalized*(Mathf.Min(spawnRoom.size.x/2.5f, spawnRoom.size.y/2.5f)-offset);
                    if (Vector2.Distance(enemyPos, player.position) <=
                        Min(minRoomHeight, minRoomWidth) / 3)
                        continue;
                    Instantiate(enemy,enemyPos , Quaternion.identity );

                    enemyCounters[spawnCenter][0]++;
                    currRoom = spawnCenter;
                }
                enemyCounters[spawnCenter][1]++;
            }

            if (bossSpawn == spawnCenter && !bossSpawned)
            {
                Instantiate(boss, bossSpawn, Quaternion.identity);
                bossSpawned = true;
            }

            
        }

    }

    public void CreateRooms()
    {
        roomList = RandomGenerationAlgs.BinarySpacePartitioning(new BoundsInt((Vector3Int)startPos, new Vector3Int(dungeonWidth, dungeonHeight, 0)), minRoomWidth, minRoomHeight);
        firstRoom = roomList.First();
        var floor = CreateSimpleRooms(roomList);
        foreach (var room in roomList)
        {
               roomCenters.Add((Vector2Int) Vector3Int.RoundToInt(room.center)); 
        }
        
        var corridors = ConnectRooms(floor, roomCenters);
        
        floor.UnionWith(corridors);

        visualizer.PaintFloor(floor);
        
        visualizer.SpawnPlayer(player, playerPos);
        
        visualizer.SpawnExit(exit, ExitPos);
        
        WallGenerator.CreateWalls(floor, visualizer);
        
    }



    private HashSet<Vector2Int> ConnectRooms(HashSet<Vector2Int> floor, List<Vector2Int> roomCenters)
    {
        var corridors = new HashSet<Vector2Int>();
        var currentCenter = roomCenters[Random.Range(0, roomCenters.Count)];
        roomCenters.Remove(currentCenter);

        while (roomCenters.Count > 0)
        {
            var closest = FindClosestRoom(currentCenter, roomCenters);
            roomCenters.Remove(closest);
            HashSet<Vector2Int> newCorridor;
            newCorridor = CreateConnection(currentCenter , closest, floor);
            currentCenter = closest;
            corridors.UnionWith(newCorridor);
            var corridorCenter = newCorridor.ToList()[newCorridor.Count/2-1];


        }

        return corridors;
    }

    private HashSet<Vector2Int> CreateConnection(Vector2Int currentCenter, Vector2Int closest, HashSet<Vector2Int> floor)
    {
        var corridor = new HashSet<Vector2Int>();
        var position = currentCenter;
        Vector2Int pos2Up;
        Vector2Int pos2Down;
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
            if(!floor.Contains(pos2Up))corridor.Add(pos2Up);
            pos2Up = position + Vector2Int.right;
            if(!floor.Contains(pos2Up))corridor.Add(pos2Up);

            if(!floor.Contains(position))corridor.Add(position);
            

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
                if(!floor.Contains(pos2Down))corridor.Add(pos2Down);
                pos2Down = position + Vector2Int.down;
                if(!floor.Contains(pos2Down))corridor.Add(pos2Down);
                if(!floor.Contains(position))corridor.Add(position);
                
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
            for (int col = offset; col < room.size.x - offset; col++)
            {
                for (int row = offset; row < room.size.y - offset; row++)
                {
                    Vector2Int position = (Vector2Int)room.min + new Vector2Int(col, row);
                    floor.Add(position);
                }
            }
        }

        var center = roomsList.First().center;
        playerPos = (Vector2Int) Vector3Int.RoundToInt(center);
        KeyPos = KeySpawner.GetKeyPlace(playerPos, roomsList.Select(x => (Vector2Int) Vector3Int.RoundToInt(x.center)).ToList());
        ExitPos =  (Vector2Int)Vector3Int.RoundToInt(new Vector3(center.x - 3, center.y));   
        return floor;
    }
    
    public void Clear()
    {
        visualizer.Clear();
        foreach (var obj in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            DestroyImmediate(obj);
        }
    }
    

}