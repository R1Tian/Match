using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cards : MonoBehaviour
{
    // 颜色属性
    public Color Color { get; set; }

    // Tetromino属性
    public Tetromino Tetromino { get; set; }

    // 技能效果函数属性
    public System.Action SkillEffect { get; set; }

    // 其他属性和方法...

    // 示例：使用颜色、Tetromino和技能效果函数
    public void UseCard()
    {
        Debug.Log("使用卡牌：");
        Debug.Log("颜色：" + Color.ToString());
        Debug.Log("Tetromino：" + Tetromino.ToString());
        Debug.Log("执行技能效果...");
        SkillEffect?.Invoke();
    }
}
