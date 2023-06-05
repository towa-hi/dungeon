using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquibbyScript : MonoBehaviour
{
    public bool isSquibbing;
    public float speed = 1f;
    public float magnitude = 1f;
    
    public void SetSquib(bool squib)
    {
        if (squib == isSquibbing)
        {
            return;
        }
        transform.localScale = new Vector3(transform.localScale.x, 1, transform.localScale.z);
        isSquibbing = squib;
    }

    void Update()
    {
        if (isSquibbing)
        {
            float y = 1 - Math.Abs(Mathf.Sin(Time.time * speed) * magnitude);
            transform.localScale = new Vector3(transform.localScale.x, y, transform.localScale.z);
        }
    }
}
