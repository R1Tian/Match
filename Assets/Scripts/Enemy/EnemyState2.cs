using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class EnemyState
{
    public void OnLoadEnemyMachine()
    {
        FSMManager.FindStateMachine(ID).OnExcute();
    }

    public int GetHP()
    {
        return CurHP;
    }

    public int GetMaxHP()
    {
        return MaxHP;
    }

    public int GetDamage()
    {
        return AttackVal;
    }


    //内容缺失
    public int AddMaxHP(int val)
    {
        return val;
    }

    //内容缺失
    public int AddHPToMax() {
        return AttackVal;
    }

    //内容缺失
    public void TakeDamge(int val) {

    }

    //内容缺失
    public void AddArmorPenetrationBuffLayer(int val) { }
}
