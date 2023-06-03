using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Cell : MonoBehaviour
{
    private GameObject model;
    public Floor floor;
    public CellType cellType;
    public Vector3Int pos;
    public bool visited = false;

    [SerializeField] public GameObject FloorCellObject;
    [SerializeField] public GameObject HiddenCellObject;
    [SerializeField] public GameObject EmptyCellObject;
    
    public GameObject EventObject;
    
    public enum CellType
    {
        FLOOR,
        HIDDEN,
        EMPTY
    }

    public void Initialize(Floor floor, Vector3Int pos, Tile tileData)
    {
        this.floor = floor;
        Grid grid = this.floor.floorTilemap.layoutGrid;
        this.pos = pos;
        
        // set cell type from tileData
        // TODO: make this detect more than just if it exists or not
        if (tileData == null)
        {
            this.cellType = CellType.EMPTY;
        }
        else
        {
            if (tileData.sprite.name == "tilemap_48")
            {
                this.cellType = CellType.FLOOR;
            }
            else if (tileData.sprite.name == "tilemap_42")
            {
                this.cellType = CellType.HIDDEN;
            }
        }
        
        // set world pos
        this.transform.position = grid.CellToWorld(pos);
        switch (cellType)
        {
            case CellType.FLOOR:
                model = Instantiate(FloorCellObject, this.transform);
                break;
            case CellType.HIDDEN:
                model = Instantiate(HiddenCellObject, this.transform);
                break;
            case CellType.EMPTY:
                model = Instantiate(EmptyCellObject, this.transform);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(cellType), cellType, null);
        }
    }
}
