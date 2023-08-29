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
    private EnemyType EnemyType;
    
    //buff
    private int AttackBuffLayer;
    private int DefenceBuffLayer;
    private int WeakBuffLayer;
    private int FragileBuffLayer;
    private int ArmorPenetrationBuffLayer;

    //状态
    private bool isHurt = false;
    private bool isAttack = false;
    #endregion
    public void OnSingletonInit()
    {
    }

    public void ResetInBattle()
    {
        AttackBuffLayer = 0;
        DefenceBuffLayer = 0;
        WeakBuffLayer = 0;
        FragileBuffLayer = 0;
        ArmorPenetrationBuffLayer = 0;
        isHurt = false;
        isAttack = false;
    }
    
    public void ReadEnemyData(EnemyObject enemy) {
        MaxHP = enemy.EnemyMaxHP;
        CurHP = MaxHP;
        ID = enemy.id;
        AttackVal = enemy.BasicAttack;
        sprite = enemy.Image;
        EnemyType = enemy.Type;
    }

    public void ExcuteAction() {
        FSMGeneral EnemyAction = FSMManager.FindStateMachine(ID);
        if (EnemyAction != null) EnemyAction.OnExcute();
        else Debug.Log("Cannot find the machine. ID :" + instance.ID);
    }

    public void Dispose(){SingletonProperty<EnemyState>.Dispose();}
}
