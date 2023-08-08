using UnityEngine;
using Sirenix.OdinInspector;
using System;
using System.Reflection;

public enum ColorType
{
    Red,
    Blue,
    Yellow,
    Green,
}

[CreateAssetMenu(fileName = "Data", menuName = "Scriptable_Object/CardObject")]
public class CardObject : SerializedScriptableObject
{
    // 仓库中卡牌在本局游戏中的唯一 id
    [ShowInInspector]
    public int id;

    // 卡牌名称
    [ShowInInspector]
    public string Name { get; set; }

    //型状
    [ShowInInspector]
    public string Shape { get; set; }

    [ShowInInspector]
    public int Quality { get; set; }

    // 颜色属性
    [ShowInInspector]
    public Color Color { get; set; }

    // 颜色类型
    [ShowInInspector]
    public ColorType ColorType { get; set; }
    
    // Tetromino类型
    [ShowInInspector]
    public TetrominoType TetrominoType { get; set; }

    public Sprite CardFace;

    // 技能效果函数属性
    [ShowInInspector]
    public string[] SkillName;

    //技能说明
    [ShowInInspector]
    public string SkillDes { get; set; }

    private Action SkillEffect { get; set; }

    public void Do() {
        if (SkillEffect == null) { InitEffect(); }
        SkillEffect();
    }

    private void InitEffect() {
        Type type = typeof(Skill);

        foreach (var item in SkillName) {
            MethodInfo methodInfo = type.GetMethod(item);

            if (methodInfo != null) {
                Action action = () => methodInfo.Invoke(type, null);
                SkillEffect += action;
            }
        }
    }
}
