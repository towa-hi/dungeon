using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JamPlayerController : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    [SerializeField] private InputActionReference movement;
    private MapInput controls;
    private bool initialized;
    public void Initialize()
    {
        initialized = true;
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    // Start is called before the first frame update
    void Awake()
    {
        controls = new MapInput();
        controls.Map.Movement.performed += ctx => MoveDirection(ctx.ReadValue<Vector2>());
        
    }

    private bool isTargeting = false;
    // Update is called once per frame
    void Update()
    {
        if (!initialized)
        {
            return;
        }
        isTargeting = controls.Map.Ztarget.IsPressed();
        if (isTargeting)
        {
            Targeting();
            // await movement keys
        }
        else
        {
            foreach (JamCell cell in JamGameController.instance.mapController.currentLevel.cellDictionary.Values)
            {
                cell.ClearSigns();
            }
        }

    }

    void MoveDirection(Vector2 direction)
    {
        if (isTargeting)
        {
            AttackDirection(direction);
        }
        else
        {
            JamGameController.instance.mapController.MovePlayer(direction);
        }
    }

    void Targeting()
    {
        JamGameController.instance.currentForm.Target();
    }

    void AttackDirection(Vector2 direction)
    {
        Debug.Log("attack direction " + direction);
    }
}

