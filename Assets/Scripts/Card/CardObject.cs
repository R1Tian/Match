
using UnityEngine;
using Sirenix.OdinInspector;
using System;
using System.Threading;
using QFramework;

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

    [HideInInspector] public CardShow CardShow;
    
    public void InitLevel()
    {
        Level = 1;
    }
    
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

    public void Do()
    {
        NeedToUse();
        if (Level > StrategyList.Length)
        {
            SkillEffect = StrategyList[StrategyList.Length - 1];
        }
        else {
            SkillEffect = StrategyList[Level - 1];
        }
        SkillExcute();
    }

    private void SkillExcute() {
        SkillEffect.ExcuteStrategy();
        SkillEffect.ExcuteStrategyByInput(Level);
    }

    private void NeedToUse()
    {
        CardShow.Animator.SetBool("NeedToUse",true);
        AudioKit.PlaySound(SoundManager.GetSE_Path() + "CardShowEffect");
        CardShowMouseWheelScroll.UpdateViewport();
        ShowCardInBattle.MoveToFirst(CardShow);
    }
    
}
