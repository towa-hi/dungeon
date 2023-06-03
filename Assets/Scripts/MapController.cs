using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapController : MonoBehaviour
{
    private static MapController ins;
    public static MapController instance {get{return ins;}
    }
    [SerializeField] public GameObject cellContainer;
    [SerializeField] public GameObject cellPrefab;
    [SerializeField] public Floor currentFloor;
    [SerializeField] public List<Floor> floorList;
    
    public static Vector2Int mapBounds = new Vector2Int(16, 16);

    public Dictionary<Vector3Int, Cell> mapData;
    
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
    }
    
    public void Initialize()
    {
        // delete all cells
        foreach (Transform child in cellContainer.transform)
        {
            Destroy(child.gameObject);
        }
        // clear mapData
        mapData = new Dictionary<Vector3Int, Cell>();
        // repop cells
        for (int x = 0; x < mapBounds.x; x++)
        {
            for (int y = 0; y < mapBounds.y; y++)
            {
                // z is always 0 regardless of floor
                Vector3Int pos = new Vector3Int(x, y, 0);
                // create a new cell obj as child of cellContainer
                GameObject cellObject = Instantiate(cellPrefab, cellContainer.transform);
                Cell cell = cellObject.GetComponent<Cell>();
                // set cell pos even if it's not needed yet, just in case
                cell.pos = pos;
                mapData.Add(pos, cell);
            }
        }
    }
    
    public void LoadFloor(int floorIndex)
    {
        currentFloor = floorList[floorIndex];
        foreach (Cell cell in mapData.Values)
        {
            // get floor data for this cell's pos from currentfloor
            Tile tileData = currentFloor.floorTilemap.GetTile<Tile>(cell.pos);
            cell.Initialize(currentFloor, cell.pos, tileData);

        }
        // hide currentFloor tile data
        currentFloor.gameObject.SetActive(false);
    }
    
}
