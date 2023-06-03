using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnState
{
    public Guid id;
    public string name;
    public Team team;
    public int experience;
    public int speed;
    public int speedCap;
    public int phyDefense;
    public int magDefense;
    public int phyEvasion;
    public int magEvasion;
    public int proficiency;
    public int proficiencyCap;
    
    // monster specific ones
    public bool isFlying;
    public Item commonDrop;
    public Item rareDrop;
    public int experienceReward;
    public int goldReward;
    
    public List<Status> afflictedStatuses;
    public List<Equipment> equipmentList;
    public Weapon equippedRight;
    public Weapon equippedLeft;
    public Helmet equippedHelmet;
    public Armor equippedArmor;
    public Accessory equippedAccessory;
    
    //
}
