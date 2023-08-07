using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager
{
    private static Dictionary<int, CardObject> CardDic;

    public static void OnInitCardDatabase()
    {
        List<CardObject> CardList = new List<CardObject>(ResourcesManager.LoadAllCards("Cards"));

        foreach (var item in CardList) {
            CardDic.TryAdd(item.id, item);
        }
    }

    public static CardObject GetCardById(int ID)
    {
        return CardDic[ID];
    }
}
