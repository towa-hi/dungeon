[System.Serializable]
public class Item
{
    public string itemName;
    public int id;
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
    public EquipmentType equipmentType;
    public int proficiencyPoints;
    public int speed = 0;
    
    // fields relevant to weapons
    public int offense;
    public DamageVariance damageVariance;
    public DamageType damageType;
    public Status appliedStatus;
    public float proc;
    
    // fields relevant to helmets
    public int mDefense;
    
    //fields relevant to armor
    public int pDefense;
    
    // fields relevant to accessories
    public Stat affectedStat;
    public int amount;

}
// public class Weapon : Equipment
// {
//     public int offense;
//     /* need:
//      * melee or ranged
//      * magic or physical or pure damage
//      * random or fixed damage numbers
//      * single or multi target
//      */
//
//     public DamageVariance damageVariance;
//     public DamageType damageType;
//     public Status appliedStatus; /* poison, petrify, sleep, steal, etc */
//     public float proc; /* percent chance of status */
//     /* DE doesn't have crits, but if we do crit chances maybe "proc" needs renaming */
//
//     /* DE doesn't display equipped weapons in the inventory screen.
//      * That means it doesn't keep track of them like this,
//      * but this should be easy to show in a menu ig
//      */
//     public bool isEquipped;
// }
//
// /* Helmets add magic defense */
// public class Helmet : Equipment
// {
//     public int mDefense;
// }
//
// /* Armor adds phyical defense */
// public class Armor : Equipment
// {
//     public int pDefense;
// }
//
// /* Accessories buff one or two Pawn stats, also have speed and PP */
// public class Accessory : Equipment
// {
//     public Stat affectedStat;
//     public int amount;
// }