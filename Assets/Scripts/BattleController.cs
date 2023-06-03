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
        Weapon genericWeapon = new Weapon();
        PawnState firstGuysPawnState = new PawnState();
        Pawn firstGuy = new Pawn(firstGuysPawnState);
        // apply equipment 
        
        
    }
    
    
}
