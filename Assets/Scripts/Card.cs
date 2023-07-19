using UnityEngine;
using System;

public class Card
{
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

    // 其他属性和方法...

    public Card(string name,Color color, Tetromino tetromino, Action skillEffect, string shape)
    {
        Name = name;
        Color = color;
        Tetromino = tetromino;
        SkillEffect = skillEffect;
        Shape = shape;
    }
    // 示例：使用颜色、Tetromino和技能效果函数
    public void UseCard()
    {
        Debug.Log("使用卡牌：" + "颜色：" + Color.ToString() + "Tetromino：" + Tetromino.ToString() + "执行技能效果...");
        if (SkillEffect != null) SkillEffect();
    }
}
