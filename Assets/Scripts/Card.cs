using UnityEngine;
using System;

[Serializable]
public class Card
{
    // 仓库中卡牌在本局游戏中的唯一 id
    public int id;
    
    // 卡牌名称
    public string Name { get; set; }

    //型状
    public string Shape { get; set; }
    
    // 颜色属性
    public Color Color { get; set; }

    // Tetromino属性
    public Tetromino Tetromino { get; set; }

    // 技能效果函数属性
    public Action SkillEffect { get; set; }

    //技能说明
    public string SkillDes { get; set; }

    // 其他属性和方法...

    public Card(string name,Color color, Tetromino tetromino, Action skillEffect, string shape, string skilldes)
    {
        Name = name;
        Color = color;
        Tetromino = tetromino;
        SkillEffect = skillEffect;
        Shape = shape;
        SkillDes = skilldes;
    }
    
    public Card(int id, string name,Color color, Tetromino tetromino, Action skillEffect, string shape, string skilldes)
    {
        this.id = id;
        Name = name;
        Color = color;
        Tetromino = tetromino;
        SkillEffect = skillEffect;
        Shape = shape;
        SkillDes = skilldes;
    }
    
    // 示例：使用颜色、Tetromino和技能效果函数
    public void UseCard()
    {
        Debug.Log("使用卡牌：" + "颜色：" + Color.ToString() + "Tetromino：" + Tetromino.ToString() + "执行技能效果...");
        if (SkillEffect != null) SkillEffect();
    }
}
