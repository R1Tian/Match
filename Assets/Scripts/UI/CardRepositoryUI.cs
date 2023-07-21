using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;

public class CardRepositoryUI : MonoBehaviour
{
    public GameObject cardPrefab;
    public Transform battleCardContainer;
    public Transform repositoryCardContainer;
    public ScrollRect battleCardScrollRect;
    public ScrollRect repositoryCardScrollRect;

    private List<Card> battleCards;
    private List<Card> repositoryCards;//allCards去除battleCards
    private List<Card> allCards;

    private float cardWidth;

    [ShowInInspector]
    private List<GameObject> battleCardPrefabs;
    [ShowInInspector]
    private List<GameObject> repositoryCardPrefabs;

    public float doTweenTime = 0.1f;

    private void Awake()
    {
        Main.instance.OnSingletonInit();
        PlayerState.instance.OnSingletonInit();
        cardWidth = cardPrefab.GetComponent<RectTransform>().rect.width;
        battleCardPrefabs = new List<GameObject>();
        repositoryCardPrefabs = new List<GameObject>();
    }

    void Start()
    {

        // 假设这里从PlayerState获取卡牌数据
        allCards = PlayerState.instance.GetAllCards();
        battleCards = PlayerState.instance.GetBattleCards();

        // 初始化战斗卡牌列表
        foreach (Card card in battleCards)
        {
            allCards.Remove(card);
            GameObject cardObj = Instantiate(cardPrefab, battleCardContainer);
            //todo 初始化战斗卡牌位置（如果所有战斗卡牌没超过显示范围，则将所有卡牌视作一个整体，整体中心与显示范围中心一致，如果超出显示范围，那么默认初始状态为从显示范围一开始生成第一张卡牌，然后一直向右生成卡牌，保证第一张显示在最左侧,两张卡牌之间要有一个间隙）
            cardObj.GetComponent<Button>().onClick.AddListener(() => RemoveFromBattleCards(card,cardObj));
            battleCardPrefabs.Add(cardObj);
        }
        repositoryCards = allCards;

        // 初始化仓库的卡牌列表
        foreach (Card card in repositoryCards)
        {
            GameObject cardObj = Instantiate(cardPrefab, repositoryCardContainer);
            //todo 初始化仓库卡牌位置（一行一行生成，从左至右生成，两张卡牌之间要有一个间隙）
            cardObj.GetComponent<Button>().onClick.AddListener(() => AddToBattleCards(card, cardObj));
            repositoryCardPrefabs.Add(cardObj);
            //battleCards.Add(card);
        }
    }

    private void AddToBattleCards(Card card, GameObject cardObj)
    {
        Debug.Log(card.Name);
        Debug.Log(cardObj.name);
        repositoryCards.Remove(card);
        battleCards.Add(card);

        if (battleCardPrefabs.Count == 0)
        {
            cardObj.GetComponent<RectTransform>().DOAnchorPos(Vector2.zero, doTweenTime);
        }
        else
        {
            Vector3 lastBattleCardPrefabPos = battleCardPrefabs[battleCardPrefabs.Count].GetComponent<RectTransform>().position;
            cardObj.GetComponent<RectTransform>().DOAnchorPos(lastBattleCardPrefabPos + new Vector3(cardWidth, 0, 0), doTweenTime);
        }
        battleCardPrefabs.Add(cardObj);
        repositoryCardPrefabs.Remove(cardObj);
        UpdateCardListsUI();

        cardObj.GetComponent<Button>().onClick.RemoveListener(() => AddToBattleCards(card, cardObj));
        cardObj.GetComponent<Button>().onClick.AddListener(() => RemoveFromBattleCards(card, cardObj));
    }

    private void RemoveFromBattleCards(Card card, GameObject cardObj)
    {
        battleCards.Remove(card);
        repositoryCards.Add(card);
        
        if (repositoryCardPrefabs.Count == 0)
        {
            cardObj.GetComponent<RectTransform>().DOAnchorPos(Vector2.zero, doTweenTime);
        }
        else
        {
            Vector3 lastRepositoryCardPrefabPos = repositoryCardPrefabs[repositoryCardPrefabs.Count].GetComponent<RectTransform>().position;
            cardObj.GetComponent<RectTransform>().DOAnchorPos(lastRepositoryCardPrefabPos + new Vector3(cardWidth, 0, 0), doTweenTime);
        }
        repositoryCardPrefabs.Add(cardObj);
        battleCardPrefabs.Remove(cardObj);
        UpdateCardListsUI();

        cardObj.GetComponent<Button>().onClick.RemoveListener(() => RemoveFromBattleCards(card, cardObj));
        cardObj.GetComponent<Button>().onClick.AddListener(() => AddToBattleCards(card, cardObj));
    }

    //private void UpdateCardLists()
    //{
    //    // 清空显示
    //    foreach (Transform child in battleCardContainer)
    //    {
    //        Destroy(child.gameObject);
    //    }

    //    foreach (Transform child in repositoryCardContainer)
    //    {
    //        Destroy(child.gameObject);
    //    }

    //    // 更新显示
    //    foreach (Card card in battleCards)
    //    {
    //        GameObject cardObj = Instantiate(cardPrefab, battleCardContainer);
    //        cardObj.GetComponent<Button>().onClick.AddListener(() => RemoveFromBattleCards(card));
    //    }

    //    foreach (Card card in repositoryCards)
    //    {
    //        GameObject cardObj = Instantiate(cardPrefab, repositoryCardContainer);
    //        cardObj.GetComponent<Button>().onClick.AddListener(() => AddToBattleCards(card));
    //    }

    //    // 更新滚动视图内容
    //    Canvas.ForceUpdateCanvases();
    //    battleCardScrollRect.horizontalNormalizedPosition = 1f;
    //    repositoryCardScrollRect.verticalNormalizedPosition = 1f;
    //    Canvas.ForceUpdateCanvases();
    //}

    private void UpdateCardListsUI()
    {

        // 更新显示
        //float xOffset = 0f;
        //foreach (Card card in battleCards)
        //{
        //    //GameObject cardObj = Instantiate(cardPrefab, battleCardContainer);
        //    //cardObj.GetComponent<Button>().onClick.AddListener(() => RemoveFromBattleCards(card));
        //    RectTransform rectTransform = cardObj.GetComponent<RectTransform>();
        //    rectTransform.anchoredPosition = new Vector2(xOffset, 0f);
        //    xOffset += cardWidth;
        //}

        if (battleCardPrefabs.Count != 0)
        {
            foreach (GameObject cardObj in battleCardPrefabs)
            {
                RectTransform rectTransform = cardObj.GetComponent<RectTransform>();
                rectTransform.DOAnchorPosX(rectTransform.rect.x - cardWidth, doTweenTime);
            }
        }


        if (repositoryCardPrefabs.Count != 0)
        {
            foreach (GameObject cardObj in repositoryCardPrefabs)
            {
                RectTransform rectTransform = cardObj.GetComponent<RectTransform>();
                rectTransform.DOAnchorPosX(rectTransform.rect.x - cardWidth, doTweenTime);
            }
        }

        

       

        //foreach (Card card in repositoryCards)
        //{
        //    GameObject cardObj = Instantiate(cardPrefab, repositoryCardContainer);
        //    cardObj.GetComponent<Button>().onClick.AddListener(() => AddToBattleCards(card));
        //}

        // 更新滚动视图内容
        Canvas.ForceUpdateCanvases();
        battleCardScrollRect.horizontalNormalizedPosition = 1f;
        repositoryCardScrollRect.verticalNormalizedPosition = 1f;
        Canvas.ForceUpdateCanvases();
    }
}
