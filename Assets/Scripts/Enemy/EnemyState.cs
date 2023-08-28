using QFramework;
using System.Collections.Generic;
using UnityEngine;


public partial class EnemyState : ISingleton
{
    public static EnemyState instance
    {
        get { return SingletonProperty<EnemyState>.Instance; }
    }
    private EnemyState() { }



    #region Attribute
    private int MaxHP;
    private int CurHP;
    private int ID;
    private int AttackVal;
    private Sprite sprite;
    
    //buff
    private int AttackBuffLayer;
    private int DefenceBuffLayer;
    private int WeakBuffLayer;
    private int FragileBuffLayer;
    private int ArmorPenetrationBuffLayer;

    //状态
    private bool isHurt = false;
    
    #endregion
    public void OnSingletonInit()
    {
    }

    public void ReadEnemyData(EnemyObject enemy) {
        MaxHP = enemy.EnemyMaxHP;
        CurHP = MaxHP;
        ID = enemy.id;
        AttackVal = enemy.BasicAttack;
        sprite = enemy.Image;
    }

    public void ExcuteAction() {
        FSMGeneral EnemyAction = FSMManager.FindStateMachine(ID);
        if (EnemyAction != null) EnemyAction.OnExcute();
        else Debug.Log("Cannot find the machine. ID :" + instance.ID);
    }

    public void Dispose(){SingletonProperty<EnemyState>.Dispose();}
}
