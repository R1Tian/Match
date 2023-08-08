using UnityEngine;
using Sirenix.OdinInspector;
using System;
using System.Reflection;

[CreateAssetMenu(fileName = "Data", menuName = "Scriptable_Object/CardObject")]
public class CardObject : SerializedScriptableObject
{
    // 仓库中卡牌在本局游戏中的唯一 id
    public int id;

    // 卡牌名称
    public string Name;

    //型状

    public string Shape;


    public int Quality;

    // 颜色属性
    public Color Color;

    // Tetromino类型
    public TetrominoType TetrominoType;

    public Sprite CardFace;

    // 技能效果函数属性
    public string[] SkillName;

    //技能说明
    public string SkillDes;

    private Action SkillEffect;

    public void Do() {
        if (SkillEffect == null) { InitEffect(); }
        else SkillEffect();
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
