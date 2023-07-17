using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    // ��������
    public string Name { get; set; }
    
    // ��ɫ����
    public Color Color { get; set; }

    // Tetromino����
    public Tetromino Tetromino { get; set; }

    // ����Ч����������
    public System.Action SkillEffect { get; set; }

    // �������Ժͷ���...

    public Card(string name,Color color, Tetromino tetromino, System.Action skillEffect)
    {
        Name = name;
        Color = color;
        Tetromino = tetromino;
        SkillEffect = skillEffect;
    }

    

    // ʾ����ʹ����ɫ��Tetromino�ͼ���Ч������
    public void UseCard()
    {
        Debug.Log("ʹ�ÿ��ƣ�" + "��ɫ��" + Color.ToString() + "Tetromino��" + Tetromino.ToString() + "ִ�м���Ч��...");
        SkillEffect?.Invoke();
    }
}
