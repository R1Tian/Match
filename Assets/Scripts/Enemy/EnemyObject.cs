using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
[CreateAssetMenu(fileName = "Data", menuName = "Scriptable_Object/EnemyObject")]
public class EnemyObject : ScriptableObject
{
    // 怪物id
    [ShowInInspector]
    public int id;
    
    // 怪物名称
    [ShowInInspector]
    public string Name ;
    
    // 最大血量
    [ShowInInspector]
    public int EnemyMaxHP;

    // 怪物难易度
    [ShowInInspector]
    public EnemyType Type;

    //攻击
    [ShowInInspector]
    public int BasicAttack;

    //怪物形象
    [ShowInInspector]
    public Sprite Image;
}
