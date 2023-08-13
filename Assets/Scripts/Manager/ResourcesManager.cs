using System.Collections;
using System.Collections.Generic;
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

    public static EnemyObject[] LoadAllEnemys(string path)
    {
        return Resources.LoadAll<EnemyObject>(path);
    }
    
}
