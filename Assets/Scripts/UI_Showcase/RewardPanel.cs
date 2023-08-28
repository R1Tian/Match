using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Serialization;

public class RewardPanel : BasePanel
{
    private Button BackBtn;
    private Text CoinCount;
    
    public GameObject fullCardPrefab;

    private List<MonoFullCard> allMonoFullCards = new List<MonoFullCard>();

    private List<int> allCardsObjectId = new List<int>();

    public int needToGenerateCount = 3;

    public float xoffset = 300f;

    public float yoffset = 0f;
    public override void OnInit()
    {
        //skinPath = "Reward";
        layer = PanelManager.Layer.TIP;
    }

    public override void OnShow(params object[] objects)
    {
        BackBtn = skin.transform.Find("Back").GetComponent<Button>();
        CoinCount = skin.transform.Find("Coin").GetComponent<Text>();
        
        #region Money

        int count = 0;
        switch (EnemyState.instance.GetEnemyType())
        {
            case EnemyType.easy:
                count = Random.Range(15, 30);
                break;
            case EnemyType.normal:
                count = Random.Range(40, 60);
                break;
            case EnemyType.elite:
                count = Random.Range(90, 110);
                break;
        }
        
        CoinCount.text = count.ToString();
        PlayerState.instance.AddMoney(count);

        #endregion
        
        
        CreateRandomCards(needToGenerateCount);

        BackBtn.onClick.AddListener(() =>
        {
            PanelManager.Close("BattlePanel");
            PanelManager.MapPanel.SetActive(true);
            Close();
        });
    }

    /// <summary>
    /// 生成一定数量的是战后奖励卡牌
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
            //Debug.Log(id);
            allMonoFullCards.Add(CreateFullCard(CardManager.GetCardById(id), gameObject.transform, count, i));
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

        if (count % 2 == 1)
        {
            monoFullCard.gameObject.GetComponent<RectTransform>().localPosition += new Vector3((index + 2 - ((count + 1) / 2)) * xoffset, 0, 0);

        }
        else
        {
            monoFullCard.gameObject.GetComponent<RectTransform>().localPosition += new Vector3((index + 2 - ((count / 2) + 0.5f)) * xoffset, 0, 0);
        }
        monoFullCard.RepositoryInit(card);
        monoFullCard.clickButton.onClick.RemoveAllListeners();
        monoFullCard.clickButton.onClick.AddListener(async () => OnCardClick(monoFullCard.card));
    
        return monoFullCard;
    }

    void OnCardClick(CardObject card)
    {
        if (PlayerState.instance.GetAllCards().Contains(card))
        {
            CardManager.CardLevelUp(card);
        }
        else
        {
            PlayerState.instance.AddAllCards(card);
        }
        
        foreach (MonoFullCard monoFullCard in allMonoFullCards)
        {
            Destroy(monoFullCard.gameObject);
        }
        
    }
}
