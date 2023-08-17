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

    public void Dispose(){SingletonProperty<EnemyState>.Dispose();}
}
