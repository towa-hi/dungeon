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
    public void Initialize(int levelNumber)
    {
        foreach (JamLevel level in levelList)
        {
            level.gameObject.SetActive(false);
        }

        currentLevel = levelList[levelNumber];
        currentLevel.Initialize();
    }
}
