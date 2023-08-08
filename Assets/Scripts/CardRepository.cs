using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardRepository
{
    private List<CardObject> allCards;
    private List<CardObject> battleCards;

    public CardRepository()
    {
        allCards = new List<CardObject>();
        battleCards = new List<CardObject>();
    }
    
    public List<CardObject> GetAllCards()
    {
        return allCards;
    }

    public void AddCard(CardObject card)
    {
        allCards.Add(card);
    }

    public void RemoveCard(CardObject card)
    {
        allCards.Remove(card);
        if (battleCards.Contains(card))
        {
            battleCards.Remove(card);
        }
    }

    public void ShowAllCards()
    {
        foreach (CardObject card in allCards)
        {
            //Debug.Log("Card Name: " + card.Name + ", Color: " + card.Color.ToString() + ", Tetromino: " + card.Tetromino.ToString());
        }
    }

    //public void SetBattleCards(Card[] cards)
    //{
    //    battleCards.Clear();
    //    battleCards.AddRange(cards);
    //}

    public void AddBattleCard()
    {

    }

    public void RemoveBattleCard()
    {

    }

    public CardObject[] GetBattleCards()
    {
        return battleCards.ToArray();
    }
}