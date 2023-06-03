using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        
        //make the gameobject for this persist across scenes
        DontDestroyOnLoad(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
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
