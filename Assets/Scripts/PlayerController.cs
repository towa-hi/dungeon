using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    private static PlayerController ins;
    public static PlayerController instance {get{return ins;}}
    
    Camera mainCamera;
    public SpriteRenderer spriteRenderer;
    public Vector3Int playerPos;
    public Grid grid;
    
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
        controls = new MapInput();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }
    [SerializeField]
    private InputActionReference movement;
    
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        controls.Map.Movement.performed += ctx => Move(ctx.ReadValue<Vector2>());
    }

    void Move(Vector2 direction)
    {
        transform.position += new Vector3(direction.x, 0, direction.y);
        if (CanMove(direction))
        {
            
        }
    }

    bool CanMove(Vector2 direction)
    {
        Vector3Int pos = playerPos + grid.WorldToCell(transform.position + (Vector3)direction);
        Tilemap tilemap = MapController.instance.currentFloor.floorTilemap;
        if (tilemap.HasTile(pos))
        {
            return false;
        }

        return true;
    }
    private MapInput controls;
    
    // Update is called once per frame
    void Update()
    {
        Vector2 movementInput = movement.action.ReadValue<Vector2>();
    }

    private void LateUpdate()
    {
        GameObject spriteObject = spriteRenderer.gameObject;
        Transform mainCameraTransform = mainCamera.transform;
        transform.LookAt(spriteObject.transform.position + mainCameraTransform.rotation * Vector3.forward, mainCameraTransform.rotation * Vector3.up);
    }

    public void SetWorldPos(Vector3Int gridPos)
    {
        this.playerPos = gridPos;
        transform.position = grid.CellToWorld(gridPos);
        
    }
    
    
}
