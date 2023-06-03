using System.Collections.Generic;
using UnityEngine;



public class Pawn
{
    public string pawnName;
    
    
    
    
    
    public Dictionary<Stat, int> initialStats = new Dictionary<Stat, int>();
    public Dictionary<Stat, int> currentStats = new Dictionary<Stat, int>();
    Status debuff; /* poisoned, confused, dead, etc */
    // change stats to a dictionary using the statistic enum
    double maxHp;
    int level;
    int exp; /* collected experience points */
    int toNextLevel; /* xp needed to reach next level */
    int speed;
    int phyDef; /* physical defense */
    int magDef; /* magic defense */
    int phyEva; /* physical evasion */
    int magEva; /* magic evasion */
    int profPoints; /* proficiency points */

    /* Equipment */
    Weapon leftHand;
    Weapon rightHand;
    Helmet helm;
    Armor armor;
    Accessory accessory;

    // battle stuff here? need ints for temp def, ATB gauge?
}
public class Hero : Pawn
{
    bool isLeader;
    bool isInParty;
    /* In DE, Heros can be found on the map.
     * Additionally, they need to be picked up when they fall in battle.
     * Or, you can drop them off at certain spots to juggle between your 4 slots.
     */
    
    Vector3Int mapPosition;
}

public class Monster : Pawn
{
    int expDroppedOnKill;
    int goldDroppedOnKill;
    bool isFlying;
    bool isReflect;
    Item commonDrop;
    Item rareDrop;
    /* Monsters can also drop items in the shop.
     * These go directly to shop's stock.
     */
    Item commonShop;
    Item rareShop;
    // Status immunities, not sure how to implement
    // Say the final boss is immune to all 7 statuses because we hate the player
    // so it's a Status array
}