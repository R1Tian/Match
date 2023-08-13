using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowCardInBattle : MonoBehaviour
{
    public List<Sprite> sprites;
    public List<GameObject> Prefebs;
    private List<CardObject> PlayerCards;
    public float Offset = 100f;
    private float CurOffset = 0f;
    // Start is called before the first frame update
    void Start()
    {
        PlayerCards = PlayerState.instance.GetBattleCards();
        GameObject parent = GameObject.Find("CardShow");

        for (int i = 0; i < PlayerCards.Count; i++)
        {
            GameObject obj = null;
            switch (PlayerCards[i].ColorType)
            {
                case ColorType.Red:
                    obj = Instantiate(Prefebs[0]);
                    break;
                case ColorType.Green:
                    obj = Instantiate(Prefebs[1]);
                    break;
                case ColorType.Blue:
                    obj = Instantiate(Prefebs[2]);
                    break;
                case ColorType.Yellow:
                    obj = Instantiate(Prefebs[3]);
                    break;
            }

            obj.transform.SetParent(parent.transform, false);
            obj.transform.localPosition = new Vector3(obj.transform.localPosition.x, obj.transform.localPosition.y - CurOffset, 0f);
            CurOffset += Offset;

            CardShow instance = obj.GetComponent<CardShow>();

            PlayerCards[i].CardShow = instance;
            
            instance.Name.text = PlayerCards[i].Name;
            instance.Shape.text = PlayerCards[i].Shape;

            //instance.BG.color = PlayerCards[i].Color;
            instance.Description.text = PlayerCards[i].SkillDes;
            instance.gameObject.SetActive(true);
        }
    }
    
    
}
