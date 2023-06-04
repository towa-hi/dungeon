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
            }

            if (cell.isExit)
            {
                if (exitFound)
                {
                    Debug.LogError("multiple exit point detected");
                }

                exitPos = cell.pos;
                exitDir = cell.exitDirection;
                if (exitDir == Vector2Int.zero)
                {
                    Debug.LogError("exit direction cant be 0");
                }
            }
        }
        
    }
}
