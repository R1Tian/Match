using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager
{
    private static List<CardObject> CardDatabase;

    public static void OnInitCardDatabase()
    {
        CardDatabase = new List<CardObject>(ResourcesManager.LoadAllCards("Cards"));
    }

    public static CardObject GetCardById(int ID)
    {
        foreach (var item in CardDatabase)
        {
            if (item.id == ID) return item;
        }

        return null;
    }
}
