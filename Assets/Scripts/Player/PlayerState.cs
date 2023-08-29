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
    private bool ArmorFeedback = false;
    private bool isHealingConversion = false;
    
    //一些状态（动画用）
    private bool isHurt = false;
    private bool isAttack = false;

    private int Money;
    #endregion

    public void OnSingletonInit()
    {
        PlayerHP = 2200;
        PlayerMaxHP = 2200;
        AttackBuffLayer = 0;
        DefenceBuffLayer = 0;
        FlexibilityBuffLayer = 0;
        TurtleShellBuffLayer = 0;
        BattleCount = 0;
        Money = 100;
        CardRepository = new CardRepository();
        InitBag();
    }

    public void ResetInBattle()
    {
        AttackBuffLayer = 0;
        DefenceBuffLayer = 0;
        FlexibilityBuffLayer = 0;
        TurtleShellBuffLayer = 0;

        CanHeal = true;
        ArmorFeedback = false;
        isHealingConversion = false;
        
        isHurt = false;
        isAttack = false;
    }

    public void ResetAll()
    {
        PlayerHP = 1000;
        PlayerMaxHP = 1000;
        AttackBuffLayer = 0;
        DefenceBuffLayer = 0;
        FlexibilityBuffLayer = 0;
        TurtleShellBuffLayer = 0;
        BattleCount = 0;
        Money = 100;
        CardRepository = new CardRepository();
        InitBag();
        ResetInBattle();
    }
    
    private void InitBag()
    {
        Tetromino LShape = Main.instance.GetTetShape("L型");
        BattleCards = new List<CardObject>();
        AllCards = new List<CardObject>();
        
        AddBattleCards(CardManager.GetCardById(1));
        AddBattleCards(CardManager.GetCardById(2));
        AddBattleCards(CardManager.GetCardById(3));
        AddBattleCards(CardManager.GetCardById(4));
        AddBattleCards(CardManager.GetCardById(7));
        AddBattleCards(CardManager.GetCardById(10));
        AddBattleCards(CardManager.GetCardById(14));
        AddBattleCards(CardManager.GetCardById(15));
        AddBattleCards(CardManager.GetCardById(16));
        AddBattleCards(CardManager.GetCardById(17));
        AddBattleCards(CardManager.GetCardById(18));

        AddAllCards(CardManager.GetCardById(1));
        AddAllCards(CardManager.GetCardById(2));
        AddAllCards(CardManager.GetCardById(3));
        AddAllCards(CardManager.GetCardById(4));
        AddAllCards(CardManager.GetCardById(7));
        AddAllCards(CardManager.GetCardById(10));
        AddAllCards(CardManager.GetCardById(14));
        AddAllCards(CardManager.GetCardById(15));
        AddAllCards(CardManager.GetCardById(16));
        AddAllCards(CardManager.GetCardById(17));
        AddAllCards(CardManager.GetCardById(18));
        
/*         //todo 临时测试
        AddBattleCards(CardManager.GetCardById(6));
        AddBattleCards(CardManager.GetCardById(8));
        AddBattleCards(CardManager.GetCardById(28));
        AddBattleCards(CardManager.GetCardById(5));
        AddBattleCards(CardManager.GetCardById(13));
        
        AddAllCards(CardManager.GetCardById(1));
        AddAllCards(CardManager.GetCardById(4));
        AddAllCards(CardManager.GetCardById(7));
        AddAllCards(CardManager.GetCardById(10));
        
        //todo 临时测试
        AddAllCards(CardManager.GetCardById(6));
        AddAllCards(CardManager.GetCardById(8));
        AddAllCards(CardManager.GetCardById(28));
        AddAllCards(CardManager.GetCardById(5));
        AddAllCards(CardManager.GetCardById(13)); */
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
        //Debug.Log(CanHeal);
        if (CanHeal)
        {
            int increment = hp + GetFlexibilityBuffLayer();
            if (isHealingConversion)
            {
                AddDefenceBuffLayer(increment);
            }
            else
            {
                PlayerHP += increment;
                if (PlayerHP > PlayerMaxHP)
                {
                    PlayerHP = PlayerMaxHP;
                }
            }
            
        }
        
        
    }

    public void TakeTrueDamge(int hp) {
        isHurt = true;
        PlayerHP -= hp;
    }
    
    public void TakeDamge(int hp)
    {
        isHurt = true;
        Debug.Log(GetDefenceBuffLayer());
        if (DefenceBuffLayer >= hp)
        {
            DefenceBuffLayer -= hp;
        }
        else
        {
            PlayerHP -= hp - DefenceBuffLayer;
            DefenceBuffLayer = 0;
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
        BuffManager.instance.ApplyStackableBuffByID(1, 3, 3,GetAttackBuff(), BuffManagerUI.PlayerBuff,() => DropAllAttackBuff());
    }

    public void DropAttackBuff(int layer) {
        AttackBuffLayer -= layer;
    }
    
    public void DropAllAttackBuff() {
        AttackBuffLayer = 0;
    }

    public int GetAttackBuff() {
        return AttackBuffLayer;
    }

    public void AddDefenceBuffLayer(int layer)
    {
        int increment = layer + GetTurtleShellBuffLayer();
        DefenceBuffLayer += increment;
        if (ArmorFeedback)
        {
            EnemyState.instance.TakeDamge(increment);
        }
    }

    public void DropAllefenceBuffLayer() {
        DefenceBuffLayer = 0;
    }
    
    
    public void DropDefenceBuffLayer(int layer) {
        DefenceBuffLayer -= layer;
    }

    public int GetDefenceBuffLayer() {
        return DefenceBuffLayer;
    }
    
    public void AddFlexibilityBuffLayer(int layer) {
        FlexibilityBuffLayer += layer;
        BuffManager.instance.ApplyStackableBuffByID(2, 3, 3,GetFlexibilityBuffLayer(), BuffManagerUI.PlayerBuff,() => DropAllFlexibilityBuffLayer());
    }

    public void DropFlexibilityBuffLayer(int layer) {
        FlexibilityBuffLayer -= layer;
    }
    
    public void DropAllFlexibilityBuffLayer() {
        FlexibilityBuffLayer = 0;
    }

    public int GetFlexibilityBuffLayer() {
        return FlexibilityBuffLayer;
    }

    public void AddTurtleShellBuffLayer(int layer) {
        TurtleShellBuffLayer += layer;
        BuffManager.instance.ApplyStackableBuffByID(3, 3, 3, GetTurtleShellBuffLayer(),BuffManagerUI.PlayerBuff,() => DropAllTurtleShellBuffLayer());
    }

    public void DropAllTurtleShellBuffLayer() {
        TurtleShellBuffLayer = 0;
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
    
    public bool GetArmorFeedback()
    {
        return ArmorFeedback;
    }
    
    public void ForbidArmorFeedback()
    {
        ArmorFeedback = false;
    }
    
    public void AllowArmorFeedback()
    {
        ArmorFeedback = true;
    }
    
    public bool GetIsHealingConversion()
    {
        return isHealingConversion;
    }
    
    public void ForbidIsHealingConversion()
    {
        isHealingConversion = false;
    }
    
    public void AllowIsHealingConversion()
    {
        isHealingConversion = true;
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


    public int GetMoney()
    {
        return Money;
    }
    
    public void AddMoney(int val)
    {
        Money += val;
    }
    
    public void ReduceMoney(int val)
    {
        Money -= val;
    }

    public bool GetIsHurt()
    {
        return isHurt;
    }
    
    public void SetIsHurt()
    {
        isHurt = true;
    }
    
    public void SetNotHurt()
    {
        isHurt = false;
    }
    
    public bool GetIsAttack()
    {
        return isAttack;
    }
    
    public void SetIsAttack()
    {
        isAttack = true;
    }
    
    public void SetNotAttack()
    {
        isAttack = false;
    }
    
    public void Dispose() { SingletonProperty<PlayerState>.Instance.Dispose(); }
}
