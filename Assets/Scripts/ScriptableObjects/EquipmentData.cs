using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Equipment", menuName = "Data/Equipment")]
public class EquipmentData : ItemData
{
    public EquipmentType equipmentType;
    public int proficiencyPoints;
    public int speed;
    
    // data relevant to weapons
    public int offense;
    public Status appliedStatus;
    public float proc;
    public DamageVariance damageVariance;
    public DamageType damageType;
    
    // data relevant to armor
    public int pDefense;
    
    // data relevant to helmet
    public int mDefense;
    
    // data relevant to accessories
    public Stat statAffected;
    public int amount;
    
}
