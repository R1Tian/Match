using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourcesManager
{
    public static GameObject LoadPrefeb(string path)
    {
        return Resources.Load<GameObject>(path);
    }

    public static CardObject[] LoadAllCards(string path) {
        return Resources.LoadAll<CardObject>(path);
    }

    public static EnemyObject LoadEnemy(string path)
    {
        return Resources.Load<EnemyObject>("Enemys/" + path);
    }
    
    public static List<BuffObject> LoadAllBuffs()
    {
        return Resources.LoadAll<BuffObject>("Buffs/").ToList();
    }
}
