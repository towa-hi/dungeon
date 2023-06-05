using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
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
    // set by JamLevel.Initialize()
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
        // check if exiting
        JamCell cell = currentLevel.cellDictionary[entity.pos];
        if (cell.isExit)
        {
            if (dir == cell.exitDirection)
            {
                ExitLevel();
            }
        }
        if (CanMove(entity.pos, dir))
        {
            currentLevel.MoveEntity(entity, destination);
            CollectManaIfExists(destination);
            JamGameController.instance.ChangeForm(dir);
            NextTurn();
        }
        else
        {
            Debug.Log("could not move player to " + destination);
        }
        
    }

    public bool IsEnemyAtLocation(Vector2Int location)
    {
        foreach (JamEntity monster in entityList) 
        {
            if (monster.pos == location)
            {
                return true;
            }
        }

        return false;
    }

    public bool CanMove(Vector2Int origin, Vector2Int direction)
    {
        Vector2Int destination = origin + direction;
        JamCell originCell = currentLevel.cellDictionary[origin];
        if (!currentLevel.cellDictionary.ContainsKey(destination))
        {
            return false;
        }

        if (IsEnemyAtLocation(destination))
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
    public bool NoWalls(Vector2Int origin, Vector2Int direction)
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
        // see if the enemy can attack
        if (GetAdjacentRooms(enemy.pos).Contains(playerEntity.pos))
        {
            Debug.Log("attack!!!!");
            JamGameController.instance.currentForm.remainingHp -= 1;
        }
        else
        {
            //decide what tile the AI moves to
            Vector2Int dir = ChasePlayer(enemy.pos);
            Debug.Log("I am at " + enemy.pos.ToString() + " going in the direction " + dir.ToString());
            if (dir == Vector2Int.zero)
            {
                Debug.Log("going nowhere");
            }
            else if (CanMove(enemy.pos, dir))
            {
                currentLevel.MoveEntity(enemy, enemy.pos + dir);
            }
            else
            {
                Debug.Log("wrong move!!");
            }
        }
        
        NextTurn();
    }

    public List<Vector2Int> GetAdjacentRooms(Vector2Int room)
    {
        List<Vector2Int> adjacents = new List<Vector2Int>();
        if (NoWalls(room, Vector2Int.up))
        {
            adjacents.Add(room + Vector2Int.up);
        }
        if (NoWalls(room, Vector2Int.down))
        {
            adjacents.Add(room + Vector2Int.down);
        }
        if (NoWalls(room, Vector2Int.left))
        {
            adjacents.Add(room + Vector2Int.left);
        }
        if (NoWalls(room, Vector2Int.right))
        {
            adjacents.Add(room + Vector2Int.right);
        }

        return adjacents;
    }

    private Dictionary<Vector2Int, int> weights = new Dictionary<Vector2Int, int>();

    public void PopulateWeights()
    {
        weights.Clear();
        foreach (Vector2Int room in currentLevel.cellDictionary.Keys)
        {
            weights.Add(room, 1000);
        }
    }
    public void AssignWeights(Vector2Int start, int value)
    {
        foreach (Vector2Int room in GetAdjacentRooms(start))
        {
            // Debug.Log("I see value of " + room.ToString() + " as " + weights[room] + " comparing to " + value.ToString());
            if (weights[room] > value)
            {
                weights[room] = value;
                AssignWeights(room, value + 1);
            }
        }
    }
    public Vector2Int ChasePlayer(Vector2Int start)
    {
        PopulateWeights();
        Vector2Int destination = playerEntity.pos;
        weights[destination] = 0;
        AssignWeights(destination, 1);
        int min = 100;
        Vector2Int choice = new Vector2Int();
        List<Vector2Int> rooms = GetAdjacentRooms(start);
        bool picked = false;
        foreach (Vector2Int room in rooms)
        {
            // only pick rooms where there is not currently an enemy
            // Debug.Log("check room " + room.ToString());
            if (!IsEnemyAtLocation(room))
            {
                if ((weights[room] < min))
                {
                    min = weights[room];
                    choice = room;
                    picked = true;
                }
            }
        }

        if (!picked)
        {
            return Vector2Int.zero;
        }

        Vector2Int output = (choice - start);
        if (output.magnitude != 1.0)
        {
            Debug.Log("WRONG WRONG BAD WRONG, choice was " + choice.ToString() + " out of " + rooms.ToString());
        }
        
        return output;
    }

    public void NextTurn()
    {
        
        Debug.Log("turn " + turnIndex + " has ended");
        turnIndex += 1;
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
        // get the entity whos turn it is
        JamEntity entity = entityList[turnIndex];
        // if tne entity is a player, do nothing just wait for input
        if (entity.isPlayer)
        {
            awaitingTurn = true;
            //await input
        }
        else
        {
            awaitingTurn = false;
            // make AI do their turn
            MoveAI(entity);
        }
    }

    public void CollectManaIfExists(Vector2Int position)
    {
        if (!currentLevel.cellDictionary.ContainsKey(position))
        {
            return;
        }

        JamCell cell = currentLevel.cellDictionary[position];
        if (cell.hasMana)
        {
            cell.CollectMana();
            JamGameController.instance.currentMana += 1;
        }
    }

    public void ExitLevel()
    {
        JamGameController.instance.NextLevel();
    }

    public JamCell GetCell(Vector2Int pos)
    {
        if (currentLevel.cellDictionary.ContainsKey(pos))
        {
            return currentLevel.cellDictionary[pos];
        }
        else
        {
            return null;
        }
    }
}
