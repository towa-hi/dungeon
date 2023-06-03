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
    public List<int> pawnsInParty;
    public List<Item> inventory;

    public static SerializedGameState CreateNew()
    {
        SerializedGameState newState = new SerializedGameState();
        newState.money = 0;
        newState.leaderPawnId = 0;
        newState.totalVisitedTiles = 1;
        
        newState.floor = 0;
        newState.playerPos = new Vector3Int(8, 8, 0);
        
        return newState;
    }
}
