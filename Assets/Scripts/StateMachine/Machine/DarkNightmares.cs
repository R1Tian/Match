using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkNightmares : FSMGeneral
{
    int stack = 1;
    public override void OnExcute()
    {
        
        if (Main.instance.GetTurn() % 3 == 1 )
        {
            PlayerState.instance.TakeDamge(EnemyState.instance.GetDamage() * stack);
        }
        else if (Main.instance.GetTurn() % 3 == 2)
        {
            PlayerState.instance.TakeDamge((EnemyState.instance.GetDamage() + 5) * stack);
        }
        else
        {
            stack *= 2;
        }
    }
}
