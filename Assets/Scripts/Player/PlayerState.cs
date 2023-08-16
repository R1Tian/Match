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
    private int FlexibilityBuffLayer;
    private int TurtleShellBuffLayer;

    private int BattleCount;//战斗次数（临时用一下，根据战斗次数增加怪物数值）
    private int Damage;
    private List<CardObject> BattleCards;
    private List<CardObject> AllCards;
    private CardRepository CardRepository;
    private bool CanHeal = true;
    #endregion

    public void OnSingletonInit()
    {
        PlayerHP = 1000;
        PlayerMaxHP = 1000;
        AttackBuffLayer = 0;
        DefenceBuffLayer = 0;
        FlexibilityBuffLayer = 0;
        TurtleShellBuffLayer = 0;
        BattleCount = 0;
        CardRepository = new CardRepository();
        InitBag();
    }

    private void InitBag()
    {
        Tetromino LShape = Main.instance.GetTetShape("L型");
        BattleCards = new List<CardObject>();
        AllCards = new List<CardObject>();
        
        AddBattleCards(CardManager.GetCardById(1));
        AddBattleCards(CardManager.GetCardById(4));
        AddBattleCards(CardManager.GetCardById(7));
        AddBattleCards(CardManager.GetCardById(10));
        
        AddAllCards(CardManager.GetCardById(1));
        AddAllCards(CardManager.GetCardById(4));
        AddAllCards(CardManager.GetCardById(7));
        AddAllCards(CardManager.GetCardById(10));
    }

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
        if (CanHeal)
        {
            PlayerHP += hp + GetFlexibilityBuffLayer();
            if (PlayerHP > PlayerMaxHP)
            {
                PlayerHP = PlayerMaxHP;
            }
        }
        
        
    }

    public void TakeTrueDamge(int hp) {
        PlayerHP -= hp;
    }
    
    public void TakeDamge(int hp) {
        if (DefenceBuffLayer >= hp)
        {
            DefenceBuffLayer -= hp;
        }
        else
        {
            DefenceBuffLayer = 0;
            PlayerHP -= hp - DefenceBuffLayer;
        }
        
        
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
        DefenceBuffLayer += layer + GetTurtleShellBuffLayer();
    }

    public void DropDefenceBuffLayer(int layer) {
        DefenceBuffLayer -= layer;
    }

    public int GetDefenceBuffLayer() {
        return DefenceBuffLayer;
    }
    
    public void AddFlexibilityBuffLayer(int layer) {
        FlexibilityBuffLayer += layer;
    }

    public void DropFlexibilityBuffLayer(int layer) {
        FlexibilityBuffLayer -= layer;
    }

    public int GetFlexibilityBuffLayer() {
        return FlexibilityBuffLayer;
    }

    public void AddTurtleShellBuffLayer(int layer) {
        TurtleShellBuffLayer += layer;
    }

    public void DropTurtleShellBuffLayer(int layer) {
        TurtleShellBuffLayer -= layer;
    }

    public int GetTurtleShellBuffLayer() {
        return TurtleShellBuffLayer;
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
    
    public void DeleteDefenceBuffLayer() {
        DefenceBuffLayer = 0;
    }

    public bool GetCanHeal()
    {
        return CanHeal;
    }
    
    public void ForbidHeal()
    {
        CanHeal = false;
    }
    
    public void AllowHeal()
    {
        CanHeal = true;
    }

    // 临时用
    #region BattleCount

    public int GetBattleCount()
    {
        return BattleCount;
    }
    
    public void AddBattleCount()
    {
        BattleCount++;
    }

    #endregion
    
    
    public void Dispose() { SingletonProperty<PlayerState>.Instance.Dispose(); }
}
