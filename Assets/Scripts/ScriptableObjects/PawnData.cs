using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Pawn", menuName = "Data/Pawn")]
public class PawnData : ScriptableObject
{
    // PawnData should contain data for the initial starting conditions of that pawn 
    public int id;
    public string name;
    public Team team;
    public int experience;
    public int baseSpeed;
    public int speed;
    public int speedCap;
    public Sprite mapSprite;
}
