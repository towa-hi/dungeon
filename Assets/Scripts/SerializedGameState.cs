using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializedGameState
{
    public int money;
    public int leaderPawnId;

    public int totalVisitedTiles;
    
    public int floor;
    public Vector3Int playerPos;
    public HashSet<Vector3Int> visitedTiles;
    
    public List<PawnState> allPawns;
    public HashSet<int> pawnsInParty;
    public List<Item> inventory;
    
}
