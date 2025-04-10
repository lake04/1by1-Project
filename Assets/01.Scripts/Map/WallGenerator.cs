using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Windows;

public static class WallGenerator 
{ 
  public static void CreateWalls(HashSet<Vector2Int> _floorPositions,TilemapVisualizer _tilemapVisualizer)
  {
        var basicWallPositions = FindWallsInDirections(_floorPositions,Direction2D.cardinalDirecionsList);
        foreach (var position in basicWallPositions)
        {
            _tilemapVisualizer.PaintSingleBasicWall(position);
        }
  }

    private static HashSet<Vector2Int> FindWallsInDirections(HashSet<Vector2Int> floorPositions, List<Vector2Int> directionList)
    {
        HashSet<Vector2Int> wallPositions = new HashSet<Vector2Int>();
        foreach(var position in floorPositions)
        {
            foreach(var direction in directionList)
            {
                var neighbourPosition = position + direction;
                if(floorPositions.Contains(neighbourPosition) == false)
                {
                    wallPositions.Add(neighbourPosition);
                }
            }
        }
        return wallPositions;
    }
}
