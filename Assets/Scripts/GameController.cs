using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameController : MonoBehaviour
{
    private static GameController ins;
    
    public static GameController instance {get{return instance;}}
    
    [SerializeField] public GameObject mainMenuCanvas;
    private GameState state;
    
    public enum GameState
    {
        MainMenu,
        Map,
        Battle
    }
    private void Awake()
    {
        // boilerplate singleton code
        
        // if exists already
        if (ins != null && ins != this)
        {
            // kill self
            Destroy(this.gameObject);
            Debug.LogError("MainMenuController encountered duplicate singleton, deleted self");
            return;
        }

        ins = this;
        
        //make the gameobject for this persist across scenes
        DontDestroyOnLoad(this.gameObject);
    }
    
    void Start()
    {
        Debug.Log("GameController started");
        StartGame();
    }
    void StartGame()
    {
        MapController.instance.Initialize();
        MapController.instance.LoadFloor(0);
        Debug.Log("game started");

    }
    public void SetState(GameState newState)
    {
        state = newState;
        switch (state)
        {
            case GameState.MainMenu:
                MainMenuController.instance.SetState(MainMenuController.MenuState.Main);
                break;
            case GameState.Map:
                MainMenuController.instance.SetState(MainMenuController.MenuState.Off);
                break;
            case GameState.Battle:
                MainMenuController.instance.SetState(MainMenuController.MenuState.Off);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

}
