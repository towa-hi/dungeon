using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility
{
    // public static Vector2Int worldToGridPos(Vector3 worldPos)
    // {
    //     
    //     return 
    // }

}
public enum Stat
{
    PDEFENCE,
    MDEFENCE,
    PEVASION,
    MEVASION,
    PROFICIENCY,
    SPEED,
    MAXHP,
}

public enum Team
{
    PLAYER,
    ENEMY
}

public enum DamageVariance
{
    RANDOM,
    FIXED
}
public enum DamageType
{
    PHYSICAL,
    MAGIC,
    PURE
}
/* ranged or melee */
public enum Target
{
    MULTI,
    SINGLE
} /* multi = hits all enemies, single = one */