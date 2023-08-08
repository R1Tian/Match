using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MonoFullCard : MonoBehaviour
{
    // FullCard 的状态（在哪里）
    enum FullCardState
    {
        BattleSelected, // 被选为战斗卡牌 ，在 ChooseBattleCardPanel 的上方
        InRepository, // 没有被选为战斗卡牌，在 ChooseBattleCardPanel 的下方
        InShop // 商店卡牌
    }

    [Header("Root")] 
    public GameObject root;
    
    [Header("组件")]
    public Image bgImage; // 背景图片
    public TextMeshProUGUI shapeText; // 形状文字
    public TextMeshProUGUI nameText; // 卡牌名称文字
    public TextMeshProUGUI skillDesText; // 技能描述文字
    public Button clickButton;


    public CardObject card;

    private FullCardState fullCardState;

    public void RepositoryInit(CardObject card)
    {
        this.card = card;

        shapeText.text = card.Shape;
        nameText.text = card.Name;
        skillDesText.text = card.SkillDes;
        bgImage.color = card.Color;
        var temp = bgImage.color;
        temp.a = 100.0f / 255;
        bgImage.color = temp;

        fullCardState = FullCardState.InRepository;
    }

    public void SetDisplayable(bool displayable)
    {
        root.SetActive(displayable);
    }
}