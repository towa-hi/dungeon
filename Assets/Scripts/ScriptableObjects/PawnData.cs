using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Pawn", menuName = "Data/Pawn")]
public class PawnData : ScriptableObject
{
    public int id;
    public string name;
    public Team team;
    public int experience;
    public int baseSpeed;
    public int speed;
    public int speedCap;
    
}

public class ItemData : ScriptableObject
{
    public string itemName;
    public int id;
}

public class EquipmentData : ItemData
{
    public int proficiencyPoints;
    public int speed;
}

[CreateAssetMenu(fileName = "New Weapon", menuName = "Data/Weapon")]
public class WeaponData : EquipmentData
{
    public int offense;
    public Status appliedStatus;
    public float proc;

    public DamageVariance damageVariance;
    public DamageType damageType;
    // bool isEquipped
}

[CreateAssetMenu(fileName = "New Armor", menuName = "Data/Armor")]
public class ArmorData : EquipmentData
{
    public int pDefense;
}

[CreateAssetMenu(fileName = "New Helmet", menuName = "Data/Helmet")]
public class HelmetData : EquipmentData
{
    public int mDefense;
}

[CreateAssetMenu(fileName = "New Accessory", menuName = "Data/Accessory")]
public class AccessoryData : EquipmentData
{
    public Stat affectedStat;
    public int amount;
}