using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using File = UnityEngine.Windows.File;

public class GameController : MonoBehaviour
{
    private static GameController ins;
    
    public static GameController instance {get{return ins;}}
    
    [SerializeField] public GameObject mainMenuCanvas;
    public GameState state;

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
    }

    public void StartNewGame()
    {
        InitializeGame();
        SerializedGameState newGame = SerializedGameState.CreateNew();
        SaveGame(newGame);
        LoadGame();
        SetState(GameState.Map);
        
        BattleController.instance.StartBattle();
    }

    public void LoadGameFromMenu()
    {
        
        InitializeGame();
        LoadGame();
        SetState(GameState.Map);
        
    }
    public void InitializeGame()
    {
        ItemDictionary = new Dictionary<int, Item>();
        PawnDictionary = new Dictionary<int, PawnState>();
        Debug.Log("game started");
        // load equipment
        EquipmentData[] AllAccessories = Resources.LoadAll<EquipmentData>("Data/ItemData/EquipmentData/AccessoryData");
        EquipmentData[] AllArmor = Resources.LoadAll<EquipmentData>("Data/ItemData/EquipmentData/ArmorData");
        EquipmentData[] AllHelmets = Resources.LoadAll<EquipmentData>("Data/ItemData/EquipmentData/HelmetData");
        EquipmentData[] AllWeapons = Resources.LoadAll<EquipmentData>("Data/ItemData/EquipmentData/WeaponData");
        List<EquipmentData> allEquipment = new List<EquipmentData>();
        allEquipment.AddRange(AllAccessories);
        allEquipment.AddRange(AllArmor);
        allEquipment.AddRange(AllHelmets);
        allEquipment.AddRange(AllWeapons);
        foreach (EquipmentData equipmentData in allEquipment)
        {
            Equipment equipment = new Equipment();
            equipment.id = equipmentData.id;
            equipment.itemName = equipmentData.itemName;
            equipment.equipmentType = equipmentData.equipmentType;
            equipment.proficiencyPoints = equipmentData.proficiencyPoints;
            equipment.speed = equipmentData.speed;
            equipment.offense = equipmentData.offense;
            equipment.damageVariance = equipmentData.damageVariance;
            equipment.damageType = equipmentData.damageType;
            equipment.proc = equipmentData.proc;
            equipment.mDefense = equipmentData.mDefense;
            equipment.pDefense = equipmentData.pDefense;
            equipment.affectedStat = equipmentData.statAffected;
            equipment.amount = equipmentData.amount;
            if (ItemDictionary.ContainsKey(equipment.id))
            {
                Debug.LogError("Duplicate item id encountered: " + equipmentData.id + "name: " + equipmentData.itemName + " other name" + ItemDictionary[equipmentData.id].itemName);
                continue;
            }
            ItemDictionary.Add(equipment.id, equipment);
            Debug.Log("loaded equipment " + equipment.id);
        }
        
        // load all pawn and item defs
        
        PawnData[] allPlayerPawns = (PawnData[])Resources.LoadAll<PawnData>("Data/PawnData/Player");
        PawnData[] allEnemyPawns = (PawnData[])Resources.LoadAll<PawnData>("Data/PawnData/Enemy");
        List<PawnData> allPawns = new List<PawnData>();
        allPawns.AddRange(allPlayerPawns);
        allPawns.AddRange(allEnemyPawns);
        foreach (PawnData pawnData in allPawns)
        {
            PawnState pawn = new PawnState();
            pawn.id = pawnData.id;
            pawn.name = pawnData.name;
            pawn.team = pawnData.team;
            pawn.experience = pawnData.experience;
            pawn.baseSpeed = pawnData.baseSpeed;
            pawn.speed = pawnData.speed;
            pawn.speedCap = pawnData.speedCap;
            pawn.mapSprite = pawnData.mapSprite;
            Equipment unarmed = (Equipment)ItemDictionary[500];
            pawn.EquipWeaponLeft(unarmed);
            pawn.EquipWeaponRight(unarmed);
            if (ItemDictionary.ContainsKey(pawnData.id))
            {
                Debug.LogError("Duplicate pawn id encountered: " + pawnData.id + "name: " + pawnData.name + " other name" + ItemDictionary[pawnData.id].itemName);
                continue;
            }
            PawnDictionary.Add(pawn.id, pawn);
            Debug.Log("loaded pawn " + pawn.id);
            pawn.afflictedStatuses = new List<Status>();
        }
    }

    void Update()
    {
        
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
    public SerializedGameState gameState;

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
        
        MapController.instance.Initialize();
        MapController.instance.LoadFloor(gameState.floor);
    }
    
    public void SaveGame(SerializedGameState saveGame)
    {
        string data = JsonUtility.ToJson(saveGame);
        print(data);
        System.IO.File.WriteAllText(savePath, data);
        Debug.Log("saved save file at " + savePath);
        SetState(GameState.Map);
    }
    
}
