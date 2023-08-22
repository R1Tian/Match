using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyMonster : FSMGeneral
{
    public override void OnExcute()
    {
        PlayerState.instance.TakeDamge(EnemyState.instance.GetDamage() + PlayerState.instance.GetBattleCount() * 10);
        Debug.Log("!!!!!!!!!!");
    }
}
