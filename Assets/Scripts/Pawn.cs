using System.Collections.Generic;
using UnityEngine;



public class Pawn
{
    public PawnState initialState;
    public PawnState currentState;

    public Pawn(PawnState initialState)
    {
        this.initialState = initialState;
        this.currentState = initialState;
        // this is the pawn constructor. iti s called when a battle actually starts
    }
    // battle stuff here? need ints for temp def, ATB gauge?
    float ATB
    public int GetEffectiveSpeed()
    {
        
    }
}