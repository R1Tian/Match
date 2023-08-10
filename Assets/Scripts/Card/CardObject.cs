using UnityEngine;
using Sirenix.OdinInspector;
using System;
using System.Reflection;
using SkillRelated;

public enum ColorType
{
    Red,
    Blue,
    Yellow,
    Green,
}

[CreateAssetMenu(fileName = "Data", menuName = "Scriptable_Object/CardObject")]
public class CardObject : ScriptableObject
{
    // 仓库中卡牌在本局游戏中的唯一 id
    [ShowInInspector]
    public int id;

    // 卡牌名称
    [ShowInInspector]
    public string Name ;

    //型状
    [ShowInInspector]
    public string Shape ;

    [ShowInInspector]
    public int Quality ;

    // 颜色属性
    [ShowInInspector]
    public Color Color ;

    //等级
    [ShowInInspector]
    public int Level;

    // 颜色类型
    [ShowInInspector]
    public ColorType ColorType ;
    
    // Tetromino类型
    [ShowInInspector]
    public TetrominoType TetrominoType ;

    public Sprite CardFace;

    // 技能效果函数属性
    [ShowInInspector]
    public string[] SkillName;

    //可选策略
    private IStrategy[] StrategyList;

    //技能说明
    [ShowInInspector]
    public string SkillDes ;

    private IStrategy SkillEffect ;

    public void InitStrategy() {
        if (SkillName.Length == 0) {
            Debug.Log("这张卡 " + Name + "没有技能效果，请检查");
            return;
        }

        StrategyList = new IStrategy[SkillName.Length];
        Debug.Log(SkillName.Length);
        for (int i = 0; i < SkillName.Length; i++) {
            Type type = Type.GetType("StrategyMethod." + SkillName[i]);
            StrategyList[i] = (IStrategy)Activator.CreateInstance(type);
        }

        SkillEffect = StrategyList[0];
    }

    public void Do() {
        if (Level - 1 > StrategyList.Length) {
            Debug.Log("这张卡 " + Name + "等级出现错误，此时等级为" + Level);
            return;
        }
        SkillEffect = StrategyList[Level - 1];
        SkillEffect.ExcuteStrategy();
    }
}
