using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status
{


    public int duration;
    public StatusType statusType;
    
    public enum StatusType
    {
        Burning,
        Slow
    }
    
    public Status(StatusType statusType, int duration)
    {
        this.statusType = statusType;
        this.duration = duration;
    }
}
