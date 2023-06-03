using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    private static BattleController ins;
    
    public static BattleController instance {get{return instance;}}

    public List<Pawn> playerTeam;
    public List<Pawn> enemyTeam;
    
    public void Awake()
    {
        // boilerplate singleton code
        
        // if exists already
        if (ins != null && ins != this)
        {
            // kill self
            Destroy(this.gameObject);
            Debug.LogError("BattleController encountered duplicate singleton, deleted self");
            return;
        }

        ins = this;
        //make the gameobject for this persist across scenes
        DontDestroyOnLoad(this.gameObject);
    }

    public float timeElapsed = 0f;
    public bool isBattle;
    void Update()
    {
        // update timeElapsed
        timeElapsed += 1f;
        Debug.Log("timeElapsed: " + timeElapsed);
    }

    void Start()
    {
        StartBattle();
    }
    public void StartBattle()
    {
        // populate player and enemyteams with dummy 
        Weapon sword = new Weapon();
        sword.offense = 10;
        sword.speed = 5;
        sword.proficiencyPoints = 5;
        sword.itemName = "sword";
        Weapon teeth = new Weapon();
        teeth.offense = 5;
        teeth.speed = 10;
        teeth.itemName = "teeth";
        Weapon club = new Weapon();
        club.offense = 20;
        club.itemName = "club";
        Helmet plateHelm = new Helmet();
        plateHelm.mDefense = 100;
        Helmet cap = new Helmet();
        cap.mDefense = 1;
        cap.speed = 10;
        Armor plateChest = new Armor();
        plateChest.pDefense = 100;
        Armor leather = new Armor();
        leather.pDefense = 1;
        leather.speed = 15;
        
        PawnState heavy = new PawnState();
        PawnState light = new PawnState();
        Pawn gimli = new Pawn(warrior);
        Pawn aragorn = new Pawn(light);
        // apply equipment 
        /* should have a setter function that does this in the "equip" menu */
        gimli.initialState.EquipWeaponRight(sword);
        gimli.initialState.EquipHelmet(plateHelm);
        gimli.initialState.EquipArmor(plateChest);
        aragorn.initialState.EquipWeaponRight(sword);
        aragorn.initialState.EquipHelmet(cap);
        aragorn.initialState.EquipArmor(leather);
    }
    
    
}
