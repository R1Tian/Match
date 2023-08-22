using System.Collections;
using System.Collections.Generic;
using System.Linq;
using QFramework;
using Unity.VisualScripting;
using UnityEngine;

public class ShopPanel : BasePanel
{
    public GameObject fullCardPrefab;

    public GameObject emptyPrefab;
    
    public GameObject gridLayoutGroup;

    private List<MonoFullCard> allMonoFullCards = new List<MonoFullCard>();

    private List<int> allCardsObjectId = new List<int>();

    public int needToGenerateCount = 8;

    public float xoffset = 300f;

    public float yoffset = 0f;
    
    public override void OnInit()
    {
        layer = PanelManager.Layer.panel;
    }

    public override void OnShow(params object[] objects)
    {
        Init();
        CreateRandomCards(needToGenerateCount);
        CheckMoney();
    }
    
    /// <summary>
    /// 生成一定数量的是商店卡牌
    /// </summary>
    /// <param name="count">生成数量</param>
    private void CreateRandomCards(int count)
    {
        foreach (var pair in CardManager.GetAllCardObjectInResources().ToList())
        {
            allCardsObjectId.Add(pair.Key);
        }

        List<int> selectedCardObjectId = GetRandomItems(allCardsObjectId, count);

        for(int i = 0;i < selectedCardObjectId.Count;i++)
        {
            int id = selectedCardObjectId[i];
            allMonoFullCards.Add(CreateFullCard(CardManager.GetCardById(id), gridLayoutGroup.transform, count, i));
        }

    }

    /// <summary>
    /// 从List中取出几个不相同的元素
    /// </summary>
    /// <param name="originalList">要被取的List</param>
    /// <param name="count">取的数量</param>
    /// <typeparam name="T">List类型</typeparam>
    /// <returns></returns>
    List<T> GetRandomItems<T>(List<T> originalList, int count)
    {
        List<T> selectedItems = new List<T>();
        List<T> remainingItems = new List<T>(originalList);

        int itemsToSelect = Mathf.Min(count, originalList.Count);

        for (int i = 0; i < itemsToSelect; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, remainingItems.Count);
            selectedItems.Add(remainingItems[randomIndex]);
            remainingItems.RemoveAt(randomIndex);
        }

        return selectedItems;
    }

    private MonoFullCard CreateFullCard(CardObject card, Transform parent, int count, int index)
    {
        MonoFullCard monoFullCard = Instantiate(fullCardPrefab, parent).GetComponent<MonoFullCard>();

        // if (count % 2 == 1)
        // {
        //     monoFullCard.gameObject.GetComponent<RectTransform>().localPosition += new Vector3((index + 2 - ((count + 1) / 2)) * xoffset, 0, 0);
        //
        // }
        // else
        // {
        //     monoFullCard.gameObject.GetComponent<RectTransform>().localPosition += new Vector3((index + 2 - ((count / 2) + 0.5f)) * xoffset, 0, 0);
        // }
        monoFullCard.ShopInit(card);
        monoFullCard.clickButton.onClick.RemoveAllListeners();
        monoFullCard.clickButton.onClick.AddListener(async () => OnCardClick(monoFullCard));
    
        return monoFullCard;
    }

    void OnCardClick(MonoFullCard monoFullCardcard)
    {
        if (PlayerState.instance.GetMoney() >= monoFullCardcard.costText.text.ToInt())
        {
            if (PlayerState.instance.GetAllCards().Contains(monoFullCardcard.card))
            {
                CardManager.CardLevelUp(monoFullCardcard.card);
            }
            else
            {
                PlayerState.instance.AddAllCards(monoFullCardcard.card);
            }
            PlayerState.instance.ReduceMoney(monoFullCardcard.costText.text.ToInt());
            int index = allMonoFullCards.IndexOf(monoFullCardcard);
            Destroy(monoFullCardcard.gameObject);
            GameObject empty = Instantiate(emptyPrefab,gridLayoutGroup.transform);
            empty.transform.SetSiblingIndex(index);
            CheckMoney();
        }
        else
        {
            Debug.Log(false);
        }
        

        
    }

    void CheckMoney()
    {
        foreach (MonoFullCard monoFullCard in allMonoFullCards)
        {
            if (monoFullCard.costText.text.ToInt() > PlayerState.instance.GetMoney())
            {
                monoFullCard.costText.color = Color.red;
            }
        }
        
    }

    public void SelectOver()
    {
        Close();
        PanelManager.MapPanel.SetActive(true);
    }

    void Init()
    {
        allMonoFullCards = new List<MonoFullCard>();
        allCardsObjectId = new List<int>();
    }
}
