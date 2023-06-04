using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JamBillboard : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void LateUpdate()
    {
        gameObject.transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward, mainCamera.transform.rotation * Vector3.up);
    }
}
