using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class TilemapVisualizer : MonoBehaviour
{
    [SerializeField]
    private Tilemap floorTilemap, wallTilemap;
    [SerializeField]
    private TileBase floorTile,wallTop;

    public void PaintFloorTiles(IEnumerable<Vector2Int> _floorPosition)
    {
        PaintTiles(_floorPosition, floorTilemap, floorTile);
    }

    private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap _tilemap, TileBase _tile)
    {
        foreach (var _positon in positions)
        {
            PaintSingleTile(_tilemap, _tile, _positon);
        }
    }

    internal void PaintSingleBasicWall(Vector2Int position)
    {
        PaintSingleTile(wallTilemap,wallTop,position);
    }

    private void PaintSingleTile(Tilemap _tilemap, TileBase _tile, Vector2Int _position)
    {
        var tilePosition = _tilemap.WorldToCell((Vector3Int)_position);
        _tilemap.SetTile(tilePosition, _tile);
    }

    public void Clear()
    {
        floorTilemap.ClearAllTiles();
        wallTilemap.ClearAllTiles();
    }
}
