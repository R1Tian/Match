using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFunc : MonoBehaviour
{
    public static int PlayerHP;
    public static int EnemyHP;

    private void Awake()
    {
        PlayerHP = 10;
        EnemyHP = 10;
    }
}
