using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowCardInBattle : MonoBehaviour
{
    public CardShow[] ShowList = new CardShow[4];
    private Card[] PlayerCards;
    // Start is called before the first frame update
    void Start()
    {
        PlayerCards = PlayerState.instance.GetCards();

        for (int i = 0; i < PlayerCards.Length; i++) {
            ShowList[i].Name.text = PlayerCards[i].Name;
            ShowList[i].Shape.text = PlayerCards[i].Shape;
            ShowList[i].BG.color = PlayerCards[i].Color;
            ShowList[i].gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
