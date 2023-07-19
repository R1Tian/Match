using QFramework;
using UnityEngine;

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
    private Card[] PlayerCards;
    #endregion

    public void OnSingletonInit()
    {
        PlayerHP = 10;
        PlayerMaxHP = 10;
        AttackBuffLayer = 0;
        DefenceBuffLayer = 0;

        InitBag();
    }

    private void InitBag()
    {
        Tetromino LShape = Main.instance.GetTetShape("L型");
        Card Card1 = new Card("AA",Color.red, LShape, Skill.Damage, "L型");
        Card Card2 = new Card("BB", Color.yellow, LShape, Skill.Power, "L型");
        Card Card3 = new Card("CC", Color.blue, LShape, Skill.Defend, "L型");
        Card Card4 = new Card("DD", Color.green, LShape, Skill.Heal, "L型");

        PlayerCards = new Card[] { Card1, Card2, Card3 , Card4};
    }

    public Card[] GetCards() {
        return PlayerCards;
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
