using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ColorType
{
    red,
    blue,
    yellow,
    green,
}
[SerializeField]
public class Card : MonoBehaviour
{
    // �ֿ��п����ڱ�����Ϸ�е�Ψһ id
    public int id;

    //��ɫ����(��ö����ѡ����ɫ������ÿ����������ɫ��
    public ColorType ColorType { get; set; }

    // ��������
    public string Name { get; set; }

    //��״
    public string Shape { get; set; }

    // ��ɫ����
    public Color Color { get; set; }

    // Tetromino����
    public Tetromino Tetromino { get; set; }

    // ����Ч����������
    public Action SkillEffect { get; set; }

    //����˵��
    public string SkillDes { get; set; }

    // �������Ժͷ���...

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


    // ʾ����ʹ����ɫ��Tetromino�ͼ���Ч������
    public void UseCard()
    {
        Debug.Log("ʹ�ÿ��ƣ�" + "��ɫ��" + Color.ToString() + "Tetromino��" + Tetromino.ToString() + "ִ�м���Ч��...");
        if (SkillEffect != null) SkillEffect();
    }
}
