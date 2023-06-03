using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PawnState
{
    public int id;
    public string name;
    public Team team;
    public int experience;
    public int hp;
    public int baseSpeed;
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
    public Equipment equippedRight;
    public Equipment equippedLeft;
    public Equipment equippedHelmet;
    public Equipment equippedArmor;
    public Equipment equippedAccessory;

    public int GetBaseSpeed()
    {
        return speed;
    }
    public int GetEquipmentSpeed(Equipment right = null, Equipment left = null, Equipment helmet = null, Equipment armor = null, Equipment accessory = null)
    {
        int newSpeed = 0;
        if (left != null)
        {
            newSpeed += left.speed;
        }
        else if (equippedLeft != null)
        {
            newSpeed += equippedLeft.speed;
        }
        if (right != null)
        {
            newSpeed += right.speed;
        }
        else if (equippedRight != null)
        {
            newSpeed += equippedRight.speed;
        }
        if (helmet != null)
        {
            newSpeed += helmet.speed;
        }
        else if (equippedHelmet != null)
        {
            newSpeed += equippedHelmet.speed;
        }
        if (armor != null)
        {
            newSpeed += armor.speed;
        }
        else if (equippedArmor != null)
        {
            newSpeed += equippedArmor.speed;
        }
        if (accessory != null)
        {
            newSpeed += accessory.speed;
        }
        else if (equippedAccessory != null)
        {
            newSpeed += equippedAccessory.speed;
        }

        return newSpeed;
    }


    public int GetProficiency(Equipment right = null, Equipment left = null, Equipment helmet = null, Equipment armor = null, Equipment accessory = null)
    {
        int newProficiency = 0;
        if (left != null)
        {
            newProficiency += left.proficiencyPoints;
        }
        else if (equippedLeft != null)
        {
            newProficiency += equippedLeft.proficiencyPoints;
        }
        if (right != null)
        {
            newProficiency += right.proficiencyPoints;
        }
        else if (equippedRight != null)
        {
            newProficiency += equippedRight.proficiencyPoints;
        }
        if (helmet != null)
        {
            newProficiency += helmet.proficiencyPoints;
        }
        else if (equippedHelmet != null)
        {
            newProficiency += equippedHelmet.proficiencyPoints;
        }
        if (armor != null)
        {
            newProficiency += armor.proficiencyPoints;
        }
        else if (equippedArmor != null)
        {
            newProficiency += equippedArmor.proficiencyPoints;
        }
        if (accessory != null)
        {
            newProficiency += accessory.proficiencyPoints;
        }
        else if (equippedAccessory != null)
        {
            newProficiency += equippedAccessory.proficiencyPoints;
        }

        return newProficiency;
    }

    public int GetPhyDefense(Equipment armor = null, Equipment accessory = null)
    {
        int newPhyDefense = 0;
        if (armor != null)
        {
            newPhyDefense += armor.pDefense;
        }
        else if (equippedArmor != null)
        {
            newPhyDefense += equippedArmor.pDefense;
        }
        // TODO: handle accessory
        return newPhyDefense;
    }
    public int GetMagDefense(Equipment helmet = null, Equipment accessory = null)
    {
        int newMagDefense = 0;
        if (helmet != null)
        {
            newMagDefense += helmet.mDefense;
        }
        else if (equippedHelmet != null)
        {
            newMagDefense += equippedHelmet.mDefense;
        }
        // TODO: handle accessory
        return newMagDefense;
    }
    // TODO: add evasion getters
    // items can add max speed, defense, evasion. there should be a base value used instead of 0 in the above funcs
    public bool EquipWeaponRight(Equipment rightWeapon)
    {
        if (GetProficiency(right: rightWeapon) <= proficiencyCap)
        {
            equippedRight = rightWeapon;
            return true;
        }
        else
        {
            Debug.Log("Too many proficiency points!");
            return false;
        }
    }
    public bool EquipWeaponLeft(Equipment leftWeapon)
    {
        if (GetProficiency(left: leftWeapon) <= proficiencyCap)
        {
            equippedLeft = leftWeapon;
            return true;
        }
        else
        {
            Debug.Log("Too many proficiency points!");
            return false;
        }
    }
    public bool EquipHelmet(Equipment someHelmet)
    {
        if (GetProficiency( helmet: someHelmet) < proficiencyCap)
        {
            equippedHelmet = someHelmet;
            return true;
        }
        else
        {
            Debug.Log("Too many proficiency points!");
            return false;
        }
    }
    public bool EquipArmor(Equipment someArmor)
    {
        if (GetProficiency(armor: someArmor) < proficiencyCap)
        {
            equippedArmor = someArmor;
            return true;
        }
        else
        {
            Debug.Log("Too many proficiency points!");
            return false;
        }
    }
    public bool EquipAccessory(Equipment someAccessory)
    {
        if (GetProficiency(accessory: someAccessory) < proficiencyCap)
        {
            equippedAccessory = someAccessory;
            // uhh im not doing accessory stat stuff yet that's hard
            return true;
        }
        else
        {
            Debug.Log("Too many proficiency points!");
            return false;
        }
    }
}
