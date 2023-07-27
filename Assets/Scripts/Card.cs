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
    // �ֿ��п����ڱ�����Ϸ�е�Ψһ id
    [ShowInInspector]
    public int id;
    [ShowInInspector]
    //��ɫ����(��ö����ѡ����ɫ������ÿ����������ɫ��
    public ColorType ColorType { get; set; }
    [ShowInInspector]
    // ��������
    public string Name { get; set; }
    [ShowInInspector]
    //��״
    public string Shape { get; set; }
    [ShowInInspector]
    // ��ɫ����
    public Color Color { get; set; }
    [ShowInInspector,OnValueChanged("OnTetrominoTypeChanged")]
    // Tetromino����
    public TetrominoType TetrominoType { get; set; }
    [ShowInInspector]
    // Tetromino����
    public Tetromino Tetromino { get; set; }
    [ShowInInspector]
    // ����Ч����������
    public Action SkillEffect { get; set; }
    [ShowInInspector]
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

    // ʾ����ʹ����ɫ��Tetromino�ͼ���Ч������
    public void UseCard()
    {
        Debug.Log("ʹ�ÿ��ƣ�" + "��ɫ��" + Color.ToString() + "Tetromino��" + Tetromino.ToString() + "ִ�м���Ч��...");
        if (SkillEffect != null) SkillEffect();
    }
}
