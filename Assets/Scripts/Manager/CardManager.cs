using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager
{
    private static Dictionary<int, CardObject> CardDic = new Dictionary<int, CardObject>();

    public static void OnInitCardDatabase()
    {
        List<CardObject> CardList = new List<CardObject>(ResourcesManager.LoadAllCards("Cards"));


        foreach (var item in CardList) {
            item.InitStrategy();
            CardDic.TryAdd(item.id, item);
        }
    }

    public static CardObject GetCardById(int ID)
    {
        return CardDic[ID];
    }

    public static Dictionary<int, CardObject> GetAllCardObjectInResources()
    {
        return CardDic;
    }
}
