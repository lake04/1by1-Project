using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorridorFirstMapGenerator : SimpleRandomWallMapGeneration
{
    [SerializeField]
    private int corridorLength = 14, corridorCount = 5;
    [SerializeField]
    [Range(0.1f,1)]
    public float roomPercent =0.8f;
    [SerializeField]
    public SimpleRandomWalkSO roomGenerationParameters;

    protected override void RunProceduralGeneration()
    {
        CorridorFirstGeneration();
    }

    private void CorridorFirstGeneration()
    {
       HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();

        CreateCorridors(floorPositions); 

        tilemapVisualizer.PaintFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, tilemapVisualizer);
    }

    private void CreateCorridors(HashSet<Vector2Int> floorPositions)
    {
        var currentPositions = startPosition;
        for (int i = 0; i < corridorCount; i++)
        {
          
                var corridor = ProceduralGenerationAlgorithm.RandomWalkCorridor(currentPositions, corridorLength);
                currentPositions = corridor[corridor.Count - 1];
                floorPositions.UnionWith(corridor);
        }
    }
}
