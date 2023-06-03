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
    
    public List<PawnState> allPlayerPawns;
    public List<int> pawnsInParty;
    public List<Item> inventory;

    public static SerializedGameState CreateNew()
    {
        SerializedGameState newState = new SerializedGameState();
        newState.money = 10;
        newState.totalVisitedTiles = 1;
        
        newState.floor = 0;
        newState.playerPos = new Vector3Int(8, 8, 0);
        newState.visitedTiles = new HashSet<Vector3Int>();
        newState.visitedTiles.Add(newState.playerPos);
        // add default pawn
        PawnState lia = GameController.instance.PawnDictionary[1];
        newState.leaderPawnId = lia.id;
        newState.allPlayerPawns = new List<PawnState>();
        newState.allPlayerPawns.Add(lia);
        newState.pawnsInParty = new List<int>();
        newState.pawnsInParty.Add(lia.id);
        
        return newState;
    }
}
