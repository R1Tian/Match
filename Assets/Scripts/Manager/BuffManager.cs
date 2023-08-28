using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using QFramework;
using UnityEngine;

public class BuffManager : ISingleton
{
    private List<BuffObject> buffObjects = ResourcesManager.LoadAllBuffs();
    private List<BuffObject> activeBuffs = new List<BuffObject>();
    private List<GameObject> activeBuffGameObjects = new List<GameObject>();
    private int currentTurn;
    
    public static BuffManager instance
    {
        get { return SingletonProperty<BuffManager>.Instance; }
    }

    private BuffManager() { }
    public void OnSingletonInit()
    {
        // if (activeBuffGameObjects.Count != 0)
        // {
        //     foreach (GameObject BuffGameObject in activeBuffGameObjects)
        //     {
        //         GameObject.Destroy(BuffGameObject);
        //     }
        // }
        //
        // activeBuffGameObjects.Clear();
        activeBuffs.Clear();
        currentTurn = 1;
    }


    private void AddBuff(BuffObject buffObject)
    {
        activeBuffs.Add(buffObject);
    }

    public async UniTask UpdateBuffs()
    {
        int newTurn = Main.instance.GetTurn();

        if (newTurn != currentTurn)
        {
            currentTurn = newTurn;

            BuffManagerUI.UpdateBuffPrefab();

            for (int i = activeBuffs.Count - 1; i >= 0; i--)
            {
                BuffObject buff = activeBuffs[i];
    
                for (int j = buff.delayEffects.Count - 1; j >= 0; j--)
                {
                    DelayEffect delayEffect = buff.delayEffects[j];
        
                    if (delayEffect.remainingDuration > 0)
                    {
                        delayEffect.remainingDuration--;

                        if (delayEffect.action != null)
                        {
                            if (delayEffect.remainingDuration % delayEffect.interval == 0)
                            {
                                delayEffect.action.Invoke();
                            }
                            if (delayEffect.remainingDuration == 0)
                            {
                                buff.delayEffects.Remove(delayEffect);
                            }
                        }
                    }
                }

                if (buff.delayEffects == null || buff.delayEffects.Count == 0)
                {
                    activeBuffs.Remove(buff);
                }
            }
        }
        
        if (activeBuffs != null && activeBuffs.Count != 0)
        {
            foreach (BuffObject buff in activeBuffs)
            {
                Debug.Log(buff.name);
            }
        }
        else
        {
            Debug.Log("没buff");
        }
        
    }

    #region 弃用

    // /// <summary>
    // /// 添加buff
    // /// </summary>
    // /// <param name="interval">间隔时间</param>
    // /// <param name="duration">持续时间</param>
    // /// <param name="actions">执行效果</param>
    // public void ApplyBuff(int interval, int duration, params Action[] actions)
    // {
    //     BuffObject buffObject = new BuffObject();
    //
    //     foreach (Action action in actions)
    //     {
    //         buffObject.delayEffects.Add(new DelayEffect(action, interval, duration));
    //     }
    //
    //     AddBuff(buffObject);
    //     BuffManagerUI.GenerateBuffPrefab(buffObject,duration);
    // }

    #endregion
    


    /// <summary>
    /// 添加buff
    /// </summary>
    /// <param name="id">buff的ID</param>
    /// <param name="interval">间隔时间</param>
    /// <param name="duration">持续时间</param>
    /// <param name="actions">执行效果</param>
    public void ApplyBuffByID(int id , int interval, int duration,Transform target, params Action[] actions)
    {

        BuffObject buffObject = GetBuffObjectByID(id);
        if (activeBuffs.Contains(buffObject))
        {
            activeBuffs.Remove(buffObject);

            
            foreach (Action action in actions)
            {
                buffObject.delayEffects.Add(new DelayEffect(action, interval, duration));
            }
            AddBuff(buffObject);
            BuffManagerUI.ResetBuffPrefabDuration(buffObject,duration);
        }
        else
        {
            foreach (Action action in actions)
            {
                buffObject.delayEffects.Add(new DelayEffect(action, interval, duration));
            }

            AddBuff(buffObject);
            BuffManagerUI.GenerateBuffPrefab(buffObject,duration,target);
        }
        
    }

    /// <summary>
    /// 添加可叠加buff
    /// </summary>
    /// <param name="id">buff的ID</param>
    /// <param name="interval">间隔时间</param>
    /// <param name="duration">持续时间</param>
    /// <param name="stackLayer">叠加层数（传入的是目前叠加的层数，会直接覆盖之前的层数，因此你需要手动计算层数）</param>
    /// <param name="actions">执行效果</param>
    public void ApplyStackableBuffByID(int id , int interval, int duration,int stackLayer,Transform target, params Action[] actions)
    {

        BuffObject buffObject = GetBuffObjectByID(id);
        if (buffObject.Stackable)
        {
            if (activeBuffs.Contains(buffObject))
            {
                activeBuffs.Remove(buffObject);
            
                foreach (Action action in actions)
                {
                    buffObject.delayEffects.Add(new DelayEffect(action, interval, duration));
                }
                AddBuff(buffObject);
                BuffManagerUI.UpdateStackableBuffPrefab(buffObject,stackLayer);
                BuffManagerUI.ResetBuffPrefabDuration(buffObject,duration);
            }
            else
            {
                foreach (Action action in actions)
                {
                    buffObject.delayEffects.Add(new DelayEffect(action, interval, duration));
                }

                AddBuff(buffObject);
                BuffManagerUI.GenerateStackableBuffPrefab(buffObject,duration,stackLayer,target);
            }
        }
        else
        {
            ApplyBuffByID(id, interval, duration, target,actions);
        }
        
        
    }
    
    


    #region tool

    public BuffObject GetBuffObjectByID(int id)
    {
        return buffObjects.Find(BuffObject => BuffObject.id == id);
    }

    #endregion
    
    public void Dispose() { SingletonProperty<BuffManager>.Instance.Dispose(); }
}
