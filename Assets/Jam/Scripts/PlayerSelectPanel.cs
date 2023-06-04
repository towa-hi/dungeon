using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelectPanel : MonoBehaviour
{
    public bool initialized = false;
    public void Initialize()
    {
        initialized = true;
        foreach (FormPanel child in GetComponentsInChildren<FormPanel>())
        {
            child.Initialize(JamGameController.instance.formData[child.pos]);
        }
    }

    private void Update()
    {
        if (initialized)
        {
            // stuff
        }
    }
}
