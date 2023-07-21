using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowCardInBattle : MonoBehaviour
{
    public GameObject Prefeb;
    private List<Card> PlayerCards;
    private float Offset = 120f;
    private float CurOffset = 0f;
    // Start is called before the first frame update
    void Start()
    {
        PlayerCards = PlayerState.instance.GetBattleCards();
        GameObject parent = GameObject.Find("CardShow");

        for (int i = 0; i < PlayerCards.Count; i++) {
            GameObject obj = Instantiate(Prefeb);

            obj.transform.SetParent(parent.transform, false);
            obj.transform.localPosition = new Vector3(obj.transform.localPosition.x, obj.transform.localPosition.y - CurOffset, 0f);
            CurOffset += Offset;

            CardShow instance = obj.GetComponent<CardShow>();

            instance.Name.text = PlayerCards[i].Name;
            instance.Shape.text = PlayerCards[i].Shape;
            instance.BG.color = PlayerCards[i].Color;
            instance.Description.text = PlayerCards[i].SkillDes;
            instance.gameObject.SetActive(true);
        }
    }
}
