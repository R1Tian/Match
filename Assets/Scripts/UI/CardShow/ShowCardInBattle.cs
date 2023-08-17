using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.UIElements;

public class ShowCardInBattle : MonoBehaviour
{
    //public List<Sprite> sprites;
    public List<GameObject> Prefebs;
    private List<CardObject> PlayerCards;

    private static List<CardShow> CardShows;
    public static List<Vector3> Positions;
    
    public static float DGDuration = 0.1f;
    //private Sequence AllSequence = DOTween.Sequence();
    

    public float Offset = 100f;
    private float CurOffset = 0f;
    // Start is called before the first frame update
    void Start()
    {
        PlayerCards = PlayerState.instance.GetBattleCards();
        GameObject parent = GameObject.Find("CardShow");

        CardShows = new List<CardShow>();
        Positions = new List<Vector3>();
        
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
            CardShows.Add(instance);
            Positions.Add(instance.transform.position);

            PlayerCards[i].CardShow = instance;
            
            instance.Name.text = PlayerCards[i].Name;
            instance.Shape.text = PlayerCards[i].Shape;

            //instance.BG.color = PlayerCards[i].Color;
            instance.Description.text = PlayerCards[i].SkillDes;
            instance.gameObject.SetActive(true);
        }
    }

    public static async UniTask MoveToFirst(CardShow cardShow,CancellationToken cancellationToken)
    {
        if (cardShow != CardShows[0])
        {
            
            int index = CardShows.IndexOf(cardShow);
            
            //操作显示
            Sequence curSequence = DOTween.Sequence();
            Vector3 firstPosition = CardShows[0].transform.position;
            Vector3 indexPosition = cardShow.transform.position;
            Tweener tweener = cardShow.transform.DOMove(firstPosition, DGDuration);
            curSequence.Append(tweener);
            for (int i = 0; i < index; i++)
            {
                Tweener tweener1 = CardShows[i].transform.DOMove(Positions[i + 1], DGDuration);
                curSequence.Join(tweener1);
                await UniTask.WhenAll(curSequence.ToUniTask(cancellationToken: cancellationToken)
                    .SuppressCancellationThrow());
            }

            //curSequence.WaitForCompletion();
            
            
            //操作数据
            CardShows.Remove(cardShow);
            CardShows.Insert(0,cardShow);
        }
        
    }
    
    public static void MoveToFirst1(CardShow cardShow)
    {
        if (cardShow != CardShows[0])
        {
            
            int index = CardShows.IndexOf(cardShow);
            
            //操作显示
            Sequence curSequence = DOTween.Sequence();
            Vector3 firstPosition = CardShows[0].transform.position;
            Vector3 indexPosition = cardShow.transform.position;
            Tweener tweener = cardShow.transform.DOMove(firstPosition, DGDuration);
            curSequence.Append(tweener);
            for (int i = 0; i < index; i++)
            {
                Tweener tweener1 = CardShows[i].transform.DOMove(Positions[i + 1], DGDuration);
                curSequence.Join(tweener1);
                
            }

            //curSequence.WaitForCompletion();
            
            
            //操作数据
            CardShows.Remove(cardShow);
            CardShows.Insert(0,cardShow);
        }
        
    }
    
}
