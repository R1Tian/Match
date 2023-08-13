using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private static Dictionary<int, EnemyObject> enemyDic = new Dictionary<int, EnemyObject>();
    
    public static void OnInitEnemyDatabase()
    {
        List<EnemyObject> enemyList = new List<EnemyObject>(ResourcesManager.LoadAllEnemys("Enemys"));


        foreach (var item in enemyList) {
            
            enemyDic.TryAdd(item.id, item);
        }
    }
}
