using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JamEntity : MonoBehaviour
{
    public Vector2Int pos;
    public bool isPlayer;
    public SpriteRenderer spriteRenderer;

    private void Update()
    {
        if (isPlayer && gameObject.activeSelf)
        {
            spriteRenderer.sprite = JamGameController.instance.currentForm.formSprite;
        }
    }
    
}