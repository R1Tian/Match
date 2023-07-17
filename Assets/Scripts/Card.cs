using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    // 卡牌名称
    public string Name { get; set; }
    
    // 颜色属性
    public Color Color { get; set; }

    // Tetromino属性
    public Tetromino Tetromino { get; set; }

    // 技能效果函数属性
    public System.Action SkillEffect { get; set; }

    // 其他属性和方法...

    public Card(string name,Color color, Tetromino tetromino, System.Action skillEffect)
    {
        Name = name;
        Color = color;
        Tetromino = tetromino;
        SkillEffect = skillEffect;
    }

    

    // 示例：使用颜色、Tetromino和技能效果函数
    public void UseCard()
    {
        Debug.Log("使用卡牌：" + "颜色：" + Color.ToString() + "Tetromino：" + Tetromino.ToString() + "执行技能效果...");
        SkillEffect?.Invoke();
    }
}
