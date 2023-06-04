using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class JamMapController : MonoBehaviour
{
    public Grid grid;
    public Tilemap tilemap;
    public Vector2Int bounds = new Vector2Int(4, 4);
    public GameObject cellPrefab;
    public JamLevel currentLevel;
    public List<JamLevel> levelList = new List<JamLevel>();

    public int turnIndex;
    
    public List<JamEntity> entityList;
    public JamEntity playerEntity;

    private bool initialized = false;
    public void Initialize(int levelNumber)
    {
        foreach (JamLevel level in levelList)
        {
            level.gameObject.SetActive(false);
        }
        turnIndex = 0;
        ClearEntityList();
        currentLevel = levelList[levelNumber];
        initialized = true;
        currentLevel.Initialize();
        
    }

    public void ClearEntityList()
    {
        foreach (JamEntity entity in entityList)
        {
            Destroy(entity.gameObject);
        }
        entityList = new List<JamEntity>();
    }
    
    
    public void MovePlayer(Vector2 direction)
    {
        Vector2Int dir = new Vector2Int((int)direction.x,(int)direction.y);
        JamEntity entity = playerEntity;
        Vector2Int destination = entity.pos + dir;
        if (CanMove(entity.pos, dir))
        {
            currentLevel.MoveEntity(entity, destination);
            NextTurn();
        }
        else
        {
            Debug.Log("could not move player to " + destination);
        }
    }

    public bool CanMove(Vector2Int origin, Vector2Int direction)
    {
        Vector2Int destination = origin + direction;
        JamCell originCell = currentLevel.cellDictionary[origin];
        if (!currentLevel.cellDictionary.ContainsKey(destination))
        {
            return false;
        }
        JamCell destinationCell = currentLevel.cellDictionary[destination];
        // what obstructions do we look for
        if (direction == Vector2Int.up)
        {
            // check origin tile for up wall
            if (originCell.hasUpWall)
            {
                return false;
            }
        }
        if (direction == Vector2Int.right)
        {
            if (originCell.hasRightWall)
            {
                return false;
            }
        }
        if (direction == Vector2Int.down)
        {
            if (destinationCell.hasUpWall)
            {
                return false;
            }
        }
        if (direction == Vector2Int.left)
        {
            if (destinationCell.hasRightWall)
            {
                return false;
            }
        }
        
        
        return true;
    }
    
    public void MoveAI(JamEntity enemy)
    {
        //decide what tile the AI moves to
        NextTurn();
    }

    public void NextTurn()
    {
        
        turnIndex += 1;
        Debug.Log("turn: " + turnIndex);
    }

    public bool awaitingTurn = false;
    void Update()
    {
        if (!initialized)
        {
            return;
        }
        // check whos turn it is
        if (turnIndex >= entityList.Count)
        {
            turnIndex = 0;
            Debug.Log("all turns finished, resetting");
        }
        JamEntity entity = entityList[turnIndex];
        if (entity.isPlayer)
        {
            //await input
        }
        else
        {
            MoveAI(entity);
        }
    }
    
}
