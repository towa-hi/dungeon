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
    private int inBattle = 0;
    void Update()
    {
        if (inBattle == 0)
        {
            return;
        }
        // update timeElapsed
        timeElapsed += 1f;
        Debug.Log("timeElapsed: " + timeElapsed);
        string loopPrint = "";
        foreach (Pawn player in playerTeam)
        {
            player.ATB += (float)player.GetEffectiveSpeed();
            Debug.Log("player speed is " + player.GetEffectiveSpeed().ToString());
            loopPrint += player.initialState.name + " hp: " + player.initialState.hp.ToString() + 
                         " speed: " + player.ATB + "/" + maxATB + "\n";
            if (player.ATB >= maxATB)
            {
                // select random target to attack
                doAttack(player, selectRandomTarget(enemyTeam));
                player.ATB = 0;
            }
        }

        foreach (Pawn enemy in enemyTeam)
        {
            enemy.ATB += (float)enemy.GetEffectiveSpeed();
            Debug.Log("enemy speed is " + enemy.GetEffectiveSpeed().ToString());
            loopPrint += enemy.initialState.name + " hp: " + enemy.initialState.hp.ToString() + 
                         " speed: " + enemy.ATB + "/" + maxATB + "\n";
            if (enemy.ATB >= maxATB)
            {
                doAttack(enemy, selectRandomTarget(playerTeam));
                enemy.ATB = 0;
            }
        }
        Debug.Log(loopPrint);
        
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
        Debug.Log("pawn " + attacker.currentState.name + " attacks " + attackTargets[0].initialState.name);
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
    }
    public void StartBattle()
    {
        playerTeam = new List<Pawn>();
        enemyTeam = new List<Pawn>();
        // populate player and enemyteams with dummy 
        PawnState testHero = GameController.instance.PawnDictionary[0];
        PawnState testEnemy = GameController.instance.PawnDictionary[100];
        Equipment unarmed = (Equipment)GameController.instance.ItemDictionary[500];
        Equipment sword = (Equipment)GameController.instance.ItemDictionary[501];
        testHero.proficiencyCap = 100;
        testEnemy.proficiencyCap = 100;
        testHero.EquipWeaponRight(sword);
        testEnemy.EquipWeaponRight(unarmed);
        
        Pawn testHeroName = new Pawn(testHero);
        Pawn testEnemyName = new Pawn(testEnemy);
        

        playerTeam.Add(testHeroName);
        enemyTeam.Add(testEnemyName);
        foreach (Pawn hero in playerTeam)
        {
            // these should be a function huh
            //hero.currentState.speed = hero.GetEffectiveSpeed();
            hero.currentState.phyDefense = hero.currentState.GetPhyDefense();
            hero.currentState.magDefense = hero.currentState.GetMagDefense();
            // get evasion too
            hero.currentState.hp = hero.currentState.GetHealth();
        }
        foreach (Pawn enemy in enemyTeam)
        {
            //enemy.currentState.speed = enemy.GetEffectiveSpeed();
            enemy.currentState.phyDefense = enemy.currentState.GetPhyDefense();
            enemy.currentState.magDefense = enemy.currentState.GetMagDefense();
            // evasion
            enemy.currentState.hp = enemy.currentState.GetHealth();
        }

        inBattle = 1;
    }
    
    
}
