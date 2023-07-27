using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public enum ColorType
{
    red,
    blue,
    yellow,
    green,
}
[SerializeField]
public class Card : SerializedMonoBehaviour
{
    // 仓库中卡牌在本局游戏中的唯一 id
    [ShowInInspector]
    public int id;
    [ShowInInspector]
    //颜色种类(用枚举类选择颜色，不用每次再设置颜色）
    public ColorType ColorType { get; set; }
    [ShowInInspector]
    // 卡牌名称
    public string Name { get; set; }
    [ShowInInspector]
    //型状
    public string Shape { get; set; }
    [ShowInInspector]
    // 颜色属性
    public Color Color { get; set; }
    [ShowInInspector,OnValueChanged("OnTetrominoTypeChanged")]
    // Tetromino类型
    public TetrominoType TetrominoType { get; set; }
    [ShowInInspector]
    // Tetromino属性
    public Tetromino Tetromino { get; set; }
    [ShowInInspector]
    // 技能效果函数属性
    public Action SkillEffect { get; set; }
    [ShowInInspector]
    //技能说明
    public string SkillDes { get; set; }

    // 其他属性和方法...


    public Card(string name, Color color, Tetromino tetromino, Action skillEffect, string shape, string skilldes)
    {
        Name = name;
        Color = color;
        Tetromino = tetromino;
        SkillEffect = skillEffect;
        Shape = shape;
        SkillDes = skilldes;
    }

    public Card(int id, string name, Color color, Tetromino tetromino, Action skillEffect, string shape, string skilldes)
    {
        this.id = id;
        Name = name;
        Color = color;
        Tetromino = tetromino;
        SkillEffect = skillEffect;
        Shape = shape;
        SkillDes = skilldes;
    }

    public Card(int id, string name, ColorType colorType, Color color, Tetromino tetromino, Action skillEffect, string shape, string skilldes)
    {
        this.id = id;
        Name = name;
        ColorType = colorType;
        Color = color;
        Tetromino = tetromino;
        SkillEffect = skillEffect;
        Shape = shape;
        SkillDes = skilldes;
    }

    public Card(int id, string name, ColorType colorType, Color color, TetrominoType tetrominoType, Action skillEffect, string shape, string skilldes)
    {
        this.id = id;
        Name = name;
        ColorType = colorType;
        Color = color;
        TetrominoType = tetrominoType;
        Tetromino = Main.instance.GetTetType(TetrominoType);
        SkillEffect = skillEffect;
        Shape = shape;
        SkillDes = skilldes;
    }

    private void OnTetrominoTypeChanged()
    {
        Tetromino = Main.instance.GetTetType(TetrominoType);
    }

    // 示例：使用颜色、Tetromino和技能效果函数
    public void UseCard()
    {
        Debug.Log("使用卡牌：" + "颜色：" + Color.ToString() + "Tetromino：" + Tetromino.ToString() + "执行技能效果...");
        if (SkillEffect != null) SkillEffect();
    }
}
