using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JamLevel : MonoBehaviour
{
    public int levelNumber;
    public Vector2Int spawnPos;
    public Vector2Int exitPos;
    public Vector2Int exitDir;
    public Dictionary<Vector2Int, JamCell> cellDictionary;
    public Grid grid;
    
    void Start()
    {
        
    }

    public void Initialize()
    {
        cellDictionary = new Dictionary<Vector2Int, JamCell>();
        grid = JamGameController.instance.mapController.grid;
        foreach (JamCell cell in transform.GetComponentsInChildren<JamCell>())
        {
            cell.Initialize(grid);
            cellDictionary.Add(cell.pos, cell);
        }
        // set self to true
        gameObject.SetActive(true);
        // set spawn point
        bool spawnFound = false;
        bool exitFound = false;
        // always add player first 
        foreach (JamCell cell in cellDictionary.Values)
        {
            if (cell.isSpawn)
            {
                if (spawnFound)
                {
                    Debug.LogError("multiple spawn point detected");
                }
                spawnPos = cell.pos;
                spawnFound = true;
                // spawn player
                GameObject playerPrefab = Instantiate(JamGameController.instance.playerController.playerPrefab, JamGameController.instance.playerController.transform);
                JamEntity player = playerPrefab.GetComponent<JamEntity>();
                JamGameController.instance.mapController.playerEntity = player;
                JamGameController.instance.mapController.entityList.Add(player);
                MoveEntity(player, spawnPos);
            }
        }
        foreach (JamCell cell in cellDictionary.Values)
        {


            if (cell.isMonsterSpawn)
            {
                GameObject enemyPrefab = Instantiate(JamGameController.instance.playerController.enemyPrefab,  JamGameController.instance.playerController.transform);
                JamEntity enemy = enemyPrefab.GetComponent<JamEntity>();
                JamGameController.instance.mapController.entityList.Add(enemy);
                MoveEntity(enemy, cell.pos);
            }

            if (cell.isExit)
            {
                if (exitFound)
                {
                    Debug.LogError("multiple exit point detected");
                }

                exitPos = cell.pos;
                exitDir = cell.exitDirection;
                exitFound = true;
                if (exitDir == Vector2Int.zero)
                {
                    Debug.LogError("exit direction cant be 0");
                }
            }
            
        }

    }
    
    public void MoveEntity(JamEntity entity, Vector2Int pos)
    {
        entity.pos = pos;
        entity.transform.position = grid.CellToWorld((Vector3Int)pos);
        if (entity.isPlayer)
        {
            JamGameController.instance.mapController.CollectManaIfExists(entity.pos);
        }
    }
    
}
