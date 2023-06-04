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
    public void Initialize()
    {
        
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

    // Update is called once per frame
    void Update()
    {
        
    }

    void MoveDirection(Vector2 direction)
    {
        JamGameController.instance.mapController.MovePlayer(direction);
    }
}

