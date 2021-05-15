using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class RandomWalkGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    protected Vector2Int startPos = Vector2Int.zero;
    [SerializeField]
    protected internal MapSettings _mapSettings;
    [SerializeField]
    protected Transform Player;

    protected bool startRandomEachIteraion = true;
    
    [FormerlySerializedAs("_visualizer")] [SerializeField]
    public TileMapVisualizer visualizer;

    public Vector2Int keyPos;
    public Vector2Int playerPos;

    // private void Start()
    // {
    //     Clear();
    //     RunProceduralGeneration();
    // }

    public void RunProceduralGeneration()
    {
        var floorPositions = new HashSet<Vector2Int>();
        switch (_mapSettings.algorithm)
        {
            case Algorithm.Corridors:
                var potentialRooms = new HashSet<Vector2Int>();
                CreateCorridor(floorPositions, potentialRooms);
                var roomPositions = CreateRooms(potentialRooms, floorPositions);
                floorPositions.UnionWith(roomPositions);
                break;
            case Algorithm.RandomWalk:
                RunRandomWalk(floorPositions, startPos);
                break;

                
        }
        Clear();
        visualizer.PaintFloor(floorPositions);
        visualizer.PaintKey(keyPos);
        visualizer.SpawnPlayer(Player, playerPos);
       
        WallGenerator.CreateWalls(floorPositions, visualizer);
    }

    public HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potentialRooms, HashSet<Vector2Int> floorPositions)
    {
        var roomPositions = new HashSet<Vector2Int>();
        var roomToCreateCounter = Mathf.RoundToInt(potentialRooms.Count * _mapSettings.roomPercent);

        var roomToCreate = potentialRooms
            .OrderBy(x => Guid.NewGuid())
            .Take(roomToCreateCounter)
            .ToList();
        playerPos = roomToCreate.First();
        visualizer.SpawnPlayer(Player, playerPos);
        foreach (var roomPos in roomToCreate)
        {
            RunRandomWalk(floorPositions, roomPos);
            roomPositions.UnionWith(floorPositions);

            if (roomPos == roomToCreate.Last())
                keyPos = roomPos;

        }
        return roomPositions;
    }

    protected void CreateCorridor(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> potentialRooms)
    {
        var currentPos = startPos;
        potentialRooms.Add(currentPos);
        for (int i = 0; i < _mapSettings.corridorCount; i++)
        {
            var corridor = RandomGenerationAlgs.RandomWalkCorridor(currentPos, _mapSettings.corridorLen);
            currentPos = corridor[corridor.Count - 2];
            potentialRooms.Add(currentPos);
            floorPositions.UnionWith(corridor);
        }
    }
    
    public void Clear()
    {
        visualizer.Clear();
        foreach (var obj in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            DestroyImmediate(obj);
        }
        
        
       
    }
    

    private void RunRandomWalk(HashSet<Vector2Int> floorPositions, Vector2Int position)
    {
        var currentPos = position;
        for (int i = 0; i < _mapSettings.iterations; i++)
        {
            var path = RandomGenerationAlgs.SimpleRandomWalk(currentPos, _mapSettings.walkLen);
            floorPositions.UnionWith(path);
            if (startRandomEachIteraion)
            {
                currentPos = floorPositions.ElementAt(Random.Range(0, floorPositions.Count));
            }
        }
    }
    
    
}

[CustomEditor(typeof(RandomWalkGenerator))]
public class LevelGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        //Reference to our script
        RandomWalkGenerator levelGen = (RandomWalkGenerator) target;

        //Only show the mapsettings UI if we have a reference set up in the editor
        if (levelGen._mapSettings != null)
        {
            Editor mapSettingEditor = CreateEditor(levelGen._mapSettings);
            mapSettingEditor.OnInspectorGUI();

            if (GUILayout.Button("Generate"))
            {
                levelGen.RunProceduralGeneration();
            }

            if (GUILayout.Button("Clear"))
            {
                levelGen.Clear();
            }
        }
    }
}
