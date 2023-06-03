using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    private static BattleController ins;
    
    public static BattleController instance {get{return ins;}}

    public List<Pawn> playerTeam;
    public List<Pawn> enemyTeam;
    public List<Pawn> EnemyTeam
    {
        get => enemyTeam;
        set => enemyTeam = value;
    }

    public List<Pawn> PlayerTeam
    {
        get => playerTeam;
        set => playerTeam = value;
    }
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
    public float maxATB = 1000;
    void Update()
    {
        /*
        // update timeElapsed
        timeElapsed += 1f;
        Debug.Log("timeElapsed: " + timeElapsed);
        string loopPrint = "";
        foreach (Pawn player in playerTeam)
        {
            player.ATB += player.GetEffectiveSpeed();
            loopPrint += player.initialState.name + " hp: " + player.initialState.hp.ToString() + " speed: " +
                         player.currentState.speed + "/" + maxATB + "/n";
            if (player.ATB >= maxATB)
            {
                // select random target to attack
                doAttack(player, selectRandomTarget(enemyTeam));
            }
        }

        foreach (Pawn enemy in enemyTeam)
        {
            loopPrint += enemy.initialState.name + " hp: " + enemy.initialState.hp.ToString() + " speed: " +
                         enemy.currentState.speed + "/" + maxATB + "/n";
            enemy.ATB += enemy.GetEffectiveSpeed();
            if (enemy.ATB >= maxATB)
            {
                doAttack(enemy, selectRandomTarget(playerTeam));
            }
        }
        */
    }

    /* Create a list of targets
     * For single target attacks, it's a list with one entry
     */
    public List<Pawn> selectRandomTarget(List<Pawn> team)
    {
        List<Pawn> targets = new List<Pawn>();
        int guy = Random.Range(1, team.Count) - 1;
        targets.Add(team[guy]);
        return targets;
    }

    public void doAttack(Pawn attacker, List<Pawn> attackTargets)
    {
        // pawn attacks!
        // select weapon (right, left)
        Equipment weapon = attacker.currentState.equippedRight;
        
        foreach (Pawn target in attackTargets)
        {
            // TODO: account for magic attack type too
            // TODO: account for random attack power
            target.currentState.phyDefense -= weapon.offense;
        }
    }

    void Start()
    {
        StartBattle();
    }
    public void StartBattle()
    {
        PawnState lia = GameController.instance.PawnDictionary[1];
        Equipment unarmed = (Equipment)GameController.instance.ItemDictionary[500];
        // populate player and enemyteams with dummy 

        // Weapon sword = new Weapon();
        // sword.offense = 10;
        // sword.speed = 5;
        // sword.proficiencyPoints = 5;
        // sword.itemName = "sword";
        // Weapon teeth = new Weapon();
        // teeth.offense = 5;
        // teeth.speed = 10;
        // teeth.itemName = "teeth";
        // Weapon club = new Weapon();
        // club.offense = 20;
        // club.itemName = "club";
        // Helmet plateHelm = new Helmet();
        // plateHelm.mDefense = 100;
        // Helmet cap = new Helmet();
        // cap.mDefense = 1;
        // cap.speed = 10;
        // Armor plateChest = new Armor();
        // plateChest.pDefense = 100;
        // Armor leather = new Armor();
        // leather.pDefense = 1;
        // leather.speed = 15;
        //
        // PawnState heavyFighter = new PawnState();
        // PawnState lightFighter = new PawnState();
        // PawnState rat = new PawnState();
        // PawnState orc = new PawnState();
        // // apply equipment 
        // heavyFighter.EquipWeaponRight(sword);
        // heavyFighter.EquipHelmet(plateHelm);
        // heavyFighter.EquipArmor(plateChest);
        // lightFighter.EquipWeaponRight(sword);
        // lightFighter.EquipHelmet(cap);
        // lightFighter.EquipArmor(leather);
        // rat.EquipWeaponRight(teeth);
        // orc.EquipWeaponRight(club);
        // // set base stats
        // heavyFighter.speed = 5;
        // heavyFighter.hp = 100;
        // lightFighter.speed = 20;
        // lightFighter.hp = 100;
        // rat.speed = 10;
        // rat.hp = 30;
        // orc.speed = 1;
        // orc.hp = 300;
        // // make them real pawns
        // Pawn gimli = new Pawn(heavyFighter);
        // Pawn aragorn = new Pawn(lightFighter);
        // Pawn ratA = new Pawn(rat);
        // Pawn ratB = new Pawn(rat);
        // Pawn orcA = new Pawn(orc);
        // /*
        // gimli.currentState.speed = gimli.GetEffectiveSpeed();
        // aragorn.currentState.speed = aragorn.GetEffectiveSpeed();
        // ratA.currentState.speed = ratA.GetEffectiveSpeed();
        // ratB.currentState.speed = ratB.GetEffectiveSpeed();
        // */
        // playerTeam.Add(gimli);
        // playerTeam.Add(aragorn);
        // enemyTeam.Add(ratA);
        // enemyTeam.Add(ratB);

        // real code?
        foreach (Pawn hero in playerTeam)
        {
            // these should be a function huh
            hero.currentState.speed = hero.GetEffectiveSpeed();
            hero.currentState.phyDefense = hero.initialState.GetPhyDefense();
            hero.currentState.magDefense = hero.initialState.GetMagDefense();
            // get evasion too
            hero.currentState.hp = hero.initialState.hp;
        }
        foreach (Pawn enemy in enemyTeam)
        {
            enemy.currentState.speed = enemy.GetEffectiveSpeed();
            enemy.currentState.phyDefense = enemy.initialState.GetPhyDefense();
            enemy.currentState.magDefense = enemy.initialState.GetMagDefense();
            // evasion
            enemy.currentState.hp = enemy.initialState.hp;
        }
        
}
    
    
}
