using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VenomousSpider : FSMGeneral
{
    public override void OnExcute()
    {
        PlayerState.instance.TakeDamge(EnemyState.instance.GetDamage());
        Debug.Log("!!!!!!!!!!");
    }
}
