using QFramework;
using UnityEngine;
using System.Collections.Generic;

public class PlayerState : ISingleton
{
    public static PlayerState instance
    {
        get { return SingletonProperty<PlayerState>.Instance; }
    }
    private PlayerState() { }

    #region Attribute
    private int PlayerHP;
    private int PlayerMaxHP;
    private int AttackBuffLayer;
    private int DefenceBuffLayer;
    private int Damage;
    private List<CardObject> BattleCards;
    private List<CardObject> AllCards;
    private CardRepository CardRepository;
    #endregion

    public void OnSingletonInit()
    {
        PlayerHP = 10;
        PlayerMaxHP = 10;
        AttackBuffLayer = 0;
        DefenceBuffLayer = 0;
        CardRepository = new CardRepository();
        InitBag();
    }

    private void InitBag()
    {
        Tetromino LShape = Main.instance.GetTetShape("L型");
        //Card Card1 = new Card("AA", Color.red, LShape, Skill.Damage, "L型", "消除时，造成2/3/5的数值伤害");
        //Card Card2 = new Card("BB", Color.yellow, LShape, Skill.Power, "L型", "消除时，生成1/2/3层力量buff（每增加1层力量buff攻击牌造成的伤害+1）");
        //Card Card3 = new Card("CC", Color.blue, LShape, Skill.Defend, "L型", "消除时，生成2/3/4点防御值");
        //Card Card4 = new Card("DD", Color.green, LShape, Skill.Heal, "L型", "消除时，恢复2/3/4点生命值");

        //CardRepository.AddCard(Card1);
        //CardRepository.AddCard(Card2);
        //CardRepository.AddCard(Card3);
        //CardRepository.AddCard(Card4);

        //BattleCards = new List<Card> { Card1, Card2, Card3, Card4 };
        BattleCards = new List<CardObject>();
        AllCards = new List<CardObject>();
        
        AddBattleCards(CardManager.GetCardById(0));
        AddBattleCards(CardManager.GetCardById(1));
        AddBattleCards(CardManager.GetCardById(2));
        
        AddAllCards(CardManager.GetCardById(0));
        AddAllCards(CardManager.GetCardById(1));
        AddAllCards(CardManager.GetCardById(2));
    }

    //private void TestForCardObject() {
    //    CardObject test1 = CardManager.GetCardById(0);
    //    CardObject test2 = CardManager.GetCardById(1);

    //    Debug.Log(test1.name);
    //    Debug.Log(test2.Shape);
    //}

    public List<CardObject> GetBattleCards() {
        return BattleCards;
    }

    public void RemoveAllBattleCards() {
        BattleCards = new List<CardObject>();
    }
    public void AddBattleCards(CardObject card)
    {
        BattleCards.Add(card);
    }

    public void AddAllCards(CardObject card)
    {
        AllCards.Add(card);
    }
    
    public List<CardObject> GetAllCards()
    {
        //return CardRepository.GetAllCards();
        return AllCards;
    }

    public void HealHealth(int hp) {
        PlayerHP += hp;
    }

    public void TakeDamge(int hp) {
        PlayerHP -= hp;
    }

    public int GetMaxHP()
    {
        return PlayerMaxHP;
    }

    public int GetHP() {
        return PlayerHP;
    }

    public void AddAttackBuff(int layer) {
        AttackBuffLayer += layer;
    }

    public void DropAttackBuff(int layer) {
        AttackBuffLayer -= layer;
    }

    public int GetAttackBuff() {
        return AttackBuffLayer;
    }

    public void AddDefenceBuffLayer(int layer) {
        DefenceBuffLayer += layer;
    }

    public void DropDefenceBuffLayer(int layer) {
        DefenceBuffLayer -= layer;
    }

    public int GetDefenceBuffLayer() {
        return DefenceBuffLayer;
    }

    public void AddDamage(int damage) {
        Damage += damage;
    }

    public int GetDamageBuff() {
        return  AttackBuffLayer;
    }

    public void DeleteDamageBuff() {
        AttackBuffLayer = 0;
    }

    public void Dispose() { SingletonProperty<PlayerState>.Instance.Dispose(); }
}
