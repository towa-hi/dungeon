
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinBounce : MonoBehaviour
{
    public float speed = 1f;

    public float magnitude;

    private float initialY;
    // Start is called before the first frame update
    void Start()
    {
        initialY = transform.position.y;
    }

    private void OnEnable()
    {
        initialY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (enabled)
        {
            transform.position = new Vector3(transform.position.x, Mathf.Sin(Time.time * speed) * magnitude + initialY, transform.position.z);

        }
    }
}
