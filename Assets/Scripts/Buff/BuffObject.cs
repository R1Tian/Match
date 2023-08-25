using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
[CreateAssetMenu(fileName = "Data", menuName = "Scriptable_Object/BuffObject")]
public class BuffObject : ScriptableObject
{

    public int id;

    public string name;

    public Sprite sprite;

    public string des;
    
    public bool Stackable;
    
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
