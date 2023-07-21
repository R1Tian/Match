using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardRepository
{
    private List<Card> allCards;
    private List<Card> battleCards;

    public CardRepository()
    {
        allCards = new List<Card>();
        battleCards = new List<Card>();
    }
    
    public List<Card> GetAllCards()
    {
        return allCards;
    }

    public void AddCard(Card card)
    {
        allCards.Add(card);
    }

    public void RemoveCard(Card card)
    {
        allCards.Remove(card);
        if (battleCards.Contains(card))
        {
            battleCards.Remove(card);
        }
    }

    public void ShowAllCards()
    {
        foreach (Card card in allCards)
        {
            Debug.Log("Card Name: " + card.Name + ", Color: " + card.Color.ToString() + ", Tetromino: " + card.Tetromino.ToString());
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

    public Card[] GetBattleCards()
    {
        return battleCards.ToArray();
    }
}