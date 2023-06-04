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
    
    
    public List<JamEntity> entityList;
    
    public void Initialize(int levelNumber)
    {
        foreach (JamLevel level in levelList)
        {
            level.gameObject.SetActive(false);
        }
        
        ClearEntityList();
        currentLevel = levelList[levelNumber];
        
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
    
}
