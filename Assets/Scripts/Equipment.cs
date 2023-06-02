public class Item
{
    string name;
}
public class Usabe
{
    // some effect
    stat // hp
    value // +100
    .stat += value
}
public class Equipment
{
    string name;
    int proficiencyPoints;
    int speed;
}
public class Weapon : Equipment
{
    int offense;
    enum variance
    {
        random, fixed
    }
    enum damageType
    {
        physical, magic, pure
    } /* physical or magic damage */
    /* ranged or melee */
    enum target
    {
        multi,
        single
    } /* multi = hits all enemies, single = one */
 
    Status appliedStatus; /* poison, petrify, sleep, steal, etc */
    float proc; /* percent chance of status */
    /* DE doesn't have crits, but if we do crit chances maybe "proc" needs renaming */

    /* DE doesn't display equipped weapons in the inventory screen.
     * That means it doesn't keep track of them like this,
     * but this should be easy to show in a menu ig
     */
    bool isEquipped;
    Pawn equippedTo;
}

/* Helmets add magic defense */
public class Helmet : Equipment
{
    int magDef;
}

/* Armor add phyical defense */
public class Armor : Equipment
{
    int phyDef;
}

/* Accessories buff one or two Pawn stats, also have speed and PP */
public class Accessory : Equipment
{

    int amount;
}