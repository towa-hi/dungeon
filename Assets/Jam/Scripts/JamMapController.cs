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
        currentLevel.MoveEntity(entity, entity.pos + dir);
        NextTurn();
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
