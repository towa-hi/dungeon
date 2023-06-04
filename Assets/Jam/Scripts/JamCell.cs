using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JamCell : MonoBehaviour
{
    public Vector2Int pos;
    public bool hasUpWall;
    public bool hasRightWall;
    
    public GameObject model;

    public GameObject fullModel;
    public GameObject upModel;
    public GameObject rightModel;
    public GameObject upRightModel;
    
    public bool isSpawn = false;
    public bool isMonsterSpawn = false;
    public bool isExit;
    
    public GameObject upExit;
    public GameObject downExit;
    public GameObject rightExit;
    public GameObject leftExit;
    
    public GameObject spawnPoint;
    public GameObject monsterSpawnPoint;
    
    public Vector2Int exitDirection = new Vector2Int(0,0);
    public void Initialize(Grid grid)
    {
        pos = new Vector2Int((int)transform.position.x, (int)transform.position.z);
        if (fullModel.activeSelf)
        {
            hasUpWall = false;
            hasRightWall = false;
        }
        else if (upModel.activeSelf)
        {
            hasUpWall = true;
            hasRightWall = false;
        }
        else if (rightModel.activeSelf)
        {
            hasUpWall = false;
            hasRightWall = true;
        }
        else if (upRightModel.activeSelf)
        {
            hasUpWall = true;
            hasRightWall = true;
        }

        isExit = false;
        if (upExit.activeSelf)
        {
            exitDirection = Vector2Int.up;
            isExit = true;
        }
        else if (downExit.activeSelf)
        {
            exitDirection = Vector2Int.down;
            isExit = true;
        }
        else if (rightExit.activeSelf)
        {
            exitDirection = Vector2Int.right;
            isExit = true;
        }
        else if (leftExit.activeSelf)
        {
            exitDirection = Vector2Int.left;
            isExit = true;
        }

        isSpawn = false || spawnPoint.activeSelf;
        isMonsterSpawn = false || monsterSpawnPoint.activeSelf;
    }
}
