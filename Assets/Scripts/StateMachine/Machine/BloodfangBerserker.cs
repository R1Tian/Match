using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodfangBerserker : FSMGeneral
{
    public override void OnExcute()
    {
        EnemyState.instance.AddBasicDamage(3);
        PlayerState.instance.TakeDamge(EnemyState.instance.GetDamage());
    }
}
