using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhantomWraithwolf : FSMGeneral
{
    public override void OnExcute()
    {
        if (Main.instance.GetTurn() == 2)
        {
            PlayerState.instance.TakeDamge(EnemyState.instance.GetDamage() + 5);
        }
        else if (Main.instance.GetTurn() == 3)
        {
            PlayerState.instance.TakeDamge(EnemyState.instance.GetDamage() + 10);
        }
        else
        {
            PlayerState.instance.TakeDamge(EnemyState.instance.GetDamage());
        }
    }
}
