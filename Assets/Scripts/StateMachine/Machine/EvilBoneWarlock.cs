using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilBoneWarlock : FSMGeneral
{
    public override void OnExcute()
    {
        PlayerState.instance.TakeTrueDamge(EnemyState.instance.GetDamage());
    }
}