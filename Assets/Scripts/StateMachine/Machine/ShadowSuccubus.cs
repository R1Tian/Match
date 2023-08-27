using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowSuccubus : FSMGeneral
{
    public override void OnExcute()
    {
        PlayerState.instance.TakeDamge(EnemyState.instance.GetDamage());
        Debug.Log("!!!!!!!!!!");
    }
}