using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

public class TileMapVisualizer : MonoBehaviour
{
    [FormerlySerializedAs("floorTiles")] [SerializeField]
    private Tilemap floorTilemap;

    [SerializeField]
    private Tilemap wallTileMap;
    
    [SerializeField]
    private Tilemap itemsTileMap;

    [SerializeField]
    private TileBase floorTile, wallTile, key;

    public void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileBase tile)
    {
        foreach (var pos in positions)
        {
            PaintOneTile(tilemap, tile, pos);
        }
    }

    public void PaintFloor(IEnumerable<Vector2Int> fllorPositions)
    {
        PaintTiles(fllorPositions, floorTilemap, floorTile);
    }

    public void PaintOneTile(Tilemap tilemap, TileBase tile, Vector2Int pos)
    {
        var tilePosition = tilemap.WorldToCell((Vector3Int)pos);
        tilemap.SetTile(tilePosition, tile);
    }

    public void Clear()
    {
        floorTilemap.ClearAllTiles();
        wallTileMap.ClearAllTiles();
        itemsTileMap.ClearAllTiles();
    }

    public void PaintOneWall(Vector2Int wall)
    {
        PaintOneTile(wallTileMap, wallTile, wall);
    }
    
    public void PaintKey(Vector2Int keyPos)
    {
        PaintOneTile(itemsTileMap, key, keyPos);
    }
    

    public void SpawnPlayer(Transform player, Vector2Int spawnPoint)
    {
        var tilePosition = itemsTileMap.WorldToCell((Vector3Int)spawnPoint);
        player.position = floorTilemap.CellToWorld(tilePosition);
    }
}