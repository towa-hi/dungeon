enum stat
{
    phyDef, magDef, phyEva, magEva, profPoints, speed
}

public class Pawn
{
    string name;
    boolean isPlayable;
    boolean isLeader;
    boolean isInParty;
    Status debuff; /* poisoned, confused, dead, etc */
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
    
    /* In DE, pawns need to be found on the map.
     * Additionally, they need to be picked up when they fall in battle.
     * Or, you can drop them off at certain spots to juggle between your 4 slots.
     * May want to ignore this mechanic and just have a basic caravan.
     */
    
    // Vector3int position

    // battle stuff here? need ints for temp def, ATB gauge?
}