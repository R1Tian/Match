using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff
{

    
    public List<DelayEffect> delayEffects = new List<DelayEffect>();
    
}

public class DelayEffect
{
    public Action action;
    public int interval;
    public int duration;
    public int remainingDuration;

    public DelayEffect(Action action, int interval, int duration)
    {
        this.action = action;
        this.interval = interval;
        this.duration = duration;
        this.remainingDuration = duration;
    }
}
