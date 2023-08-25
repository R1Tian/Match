using System.Collections;
using System.Collections.Generic;using QFramework;
using UnityEngine;

public class Buff
{

    #region Attribute

    private int id;
    private string name;
    private string des;
    private Sprite sprite;
    

    #endregion
    
    public void ReadBuffData(BuffObject buff) {
        id = buff.id;
        name = buff.name;
        des = buff.des;
        sprite = buff.sprite;
    }

    public int GetID()
    {
        return id;
    }
    
    public string GetName()
    {
        return name;
    }
    public string GetDes()
    {
        return des;
    }
    public Sprite GetSprite()
    {
        return sprite;
    }
}
