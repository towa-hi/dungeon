using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Tilemaps;
using File = UnityEngine.Windows.File;

public class GameController : MonoBehaviour
{
    private static GameController ins;
    
    public static GameController instance {get{return instance;}}
    
    [SerializeField] public GameObject mainMenuCanvas;
    private GameState state;

    public Dictionary<int, Item> ItemDictionary;
    public Dictionary<int, PawnState> PawnDictionary;
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
        
        savePath =  Application.persistentDataPath + "/saveData.json";
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
        // load all pawn and item defs
        PawnData[] allPawns = (PawnData[])Resources.FindObjectsOfTypeAll(typeof(PawnData));
        foreach (PawnData pawnData in allPawns)
        {
            
        }
        SerializedGameState newGame = SerializedGameState.CreateNew();
        SaveGame(newGame);
        LoadGame();

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

    private string savePath;
    private SerializedGameState gameState;

    public void LoadGame()
    {
        if (!File.Exists(savePath))
        {
            Debug.LogError("No save game found at " + savePath);
            return;
        }

        string fileContents = System.IO.File.ReadAllText(savePath);
        SerializedGameState loadedGame = JsonUtility.FromJson<SerializedGameState>(fileContents);
        Debug.Log("loaded save file at " + savePath);
        gameState = loadedGame;
        
        
    }
    
    public void SaveGame(SerializedGameState saveGame)
    {
        string data = JsonUtility.ToJson(saveGame);
        print(data);
        System.IO.File.WriteAllText(savePath, data);
        Debug.Log("saved save file at " + savePath);
    }
    
}
