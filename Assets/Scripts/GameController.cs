using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private static GameController ins;
    
    public static GameController instance {get{return instance;}}

    [SerializeField] public GameObject mainMenuCanvas;
    public GameSM gameSM;
    
    
    void Start()
    {
        Debug.Log("GameController started");
        GameSMMainMenuState newState = new GameSMMainMenuState();
        gameSM = new GameSM(newState);
    }

}
