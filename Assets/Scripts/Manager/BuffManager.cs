using System;
using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;

public class BuffManager : ISingleton
{
    private List<Buff> activeBuffs = new List<Buff>();
    private int currentTurn;
    
    public static BuffManager instance
    {
        get { return SingletonProperty<BuffManager>.Instance; }
    }

    private BuffManager() { }
    public void OnSingletonInit()
    {
        activeBuffs.Clear();
        currentTurn = 0;
    }


    private void AddBuff(Buff buff)
    {
        activeBuffs.Add(buff);
    }

    public void UpdateBuffs()
    {
        int newTurn = Main.instance.GetTurn();

        if (newTurn != currentTurn)
        {
            currentTurn = newTurn;

            foreach (Buff buff in activeBuffs)
            {
                foreach (DelayEffect delayEffect in buff.delayEffects)
                {
                    if (delayEffect.remainingDuration > 0)
                    {
                        delayEffect.remainingDuration--;

                        if (delayEffect.remainingDuration == 0)
                        {
                            delayEffect.action.Invoke();
                        }
                        else if (delayEffect.remainingDuration % delayEffect.interval == 0)
                        {
                            delayEffect.action.Invoke();
                        }
                    }
                }
            }
        }
    }
    
    /// <summary>
    /// 添加buff
    /// </summary>
    /// <param name="interval">间隔时间</param>
    /// <param name="duration">持续时间</param>
    /// <param name="actions">执行效果</param>
    public void ApplyDelayEffect(int interval, int duration, params Action[] actions)
    {
        Buff buff = new Buff();

        foreach (Action action in actions)
        {
            buff.delayEffects.Add(new DelayEffect(action, interval, duration));
        }

        AddBuff(buff);
    }

    
    public void Dispose() { SingletonProperty<BuffManager>.Instance.Dispose(); }
}
