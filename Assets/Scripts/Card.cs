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

    // ʾ����ʹ����ɫ��Tetromino�ͼ���Ч������
    public void UseCard()
    {
        Debug.Log("ʹ�ÿ��ƣ�");
        Debug.Log("��ɫ��" + Color.ToString());
        Debug.Log("Tetromino��" + Tetromino.ToString());
        Debug.Log("ִ�м���Ч��...");
        SkillEffect?.Invoke();
    }
}
