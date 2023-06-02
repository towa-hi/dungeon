public class Item
{
    string name;
}
public class Usable
{
    /* for usable items
     * all the ones in DE just buff one stat,
     * like "permanently increase max HP by 100"
     * i guess we could do like health potions too
     */
}
public class Equipment : Item
{
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
    }
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

/* Armor adds phyical defense */
public class Armor : Equipment
{
    int phyDef;
}

/* Accessories buff one or two Pawn stats, also have speed and PP */
public class Accessory : Equipment
{
    int amount;
}