using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public static class ProceduralGenerationAlgorithm 
{
   public static HashSet<Vector2Int> SimpleRandomWalk(Vector2Int startPosition,int walkLength)
    {
        HashSet<Vector2Int> path = new HashSet<Vector2Int>();

        path.Add(startPosition);
        var previousPosition = startPosition;

        for(int i=0; i<walkLength; i++)
        {
            var newPosition = previousPosition + Direction2D.GetRandomCardinalDirection();
            path.Add(newPosition);
            previousPosition = newPosition;
        }
        return path;
    }

    public static class Direction2D
    {
        public static List<Vector2Int> cardinalDirecionsList = new List<Vector2Int>
        {
            new Vector2Int(0,1),    //위
            new Vector2Int(1,0),    //오른쪽
            new Vector2Int(0,-1),   //아래
            new Vector2Int(-1,0)    //왼쪽
        };

        public static Vector2Int GetRandomCardinalDirection()
        {
            return cardinalDirecionsList[Random.Range(0, cardinalDirecionsList.Count)];
        }
    }
}
