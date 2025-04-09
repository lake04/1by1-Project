using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="SimpleRandomWalkRarameters_", menuName ="PCG/SimpleRadnomWalkData")]
public class SimpleRandomWalkSO : ScriptableObject
{
    public int iterations = 10, walkLength = 10;
    public bool startRandomlyEachIteration = true;


}
