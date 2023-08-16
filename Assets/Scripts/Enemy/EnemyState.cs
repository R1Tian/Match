using QFramework;
using System.Collections.Generic;
using UnityEngine;


public class EnemyState : ISingleton
{
    public static EnemyState instance
    {
        get { return SingletonProperty<EnemyState>.Instance; }
    }
    private EnemyState() { }

    

    #region Attribute
    private int EnemyMaxHP;
    private int EnemyHP;
    private int AttackBuffLayer;
    private int DefenceBuffLayer;
    private int WeakBuffLayer;
    private int FragileBuffLayer;
    private int ArmorPenetrationBuffLayer;
    private int Damage;
    //private List<Task> Tasks = new List<Task>();

    #endregion
    public void OnSingletonInit()
    {
        EnemyMaxHP = 100;
        EnemyHP = EnemyMaxHP;
        AttackBuffLayer = 0;
        DefenceBuffLayer = 0;
        WeakBuffLayer = 0;
        FragileBuffLayer = 0;
        ArmorPenetrationBuffLayer = 0;
        Damage = 8;
        //InitTasks();

    }

    public void HealHealth(int hp) {
        EnemyHP += hp;
    }
    public void TakeDamge(int hp)
    {
        EnemyHP -= (int)Mathf.Floor((hp + PlayerState.instance.GetAttackBuff()) * Mathf.Pow(1.5f, GetArmorPenetrationBuffLayer()));
    }
    public int GetHP() {
        return EnemyHP;
    }
    public int GetMaxHP()
    {
        return EnemyMaxHP;
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
    public void AddWeakBuffLayer(int layer) {
        WeakBuffLayer += layer;
    }
    public void DropWeakBuffLayer(int layer) {
        WeakBuffLayer -= layer;
    }
    public int GetWeakBuffLayer() {
        return WeakBuffLayer;
    }
    public void AddFragileBuffLayer(int layer) {
        FragileBuffLayer += layer;
    }
    public void DropFragileBuffLayer(int layer) {
        FragileBuffLayer -= layer;
    }
    public int GetFragileBuffLayer() {
        return FragileBuffLayer;
    }
    
    public void AddArmorPenetrationBuffLayer(int layer) {
        ArmorPenetrationBuffLayer += layer;
    }
    public void DropArmorPenetrationBuffLayer(int layer) {
        ArmorPenetrationBuffLayer -= layer;
    }
    public int GetArmorPenetrationBuffLayer() {
        return ArmorPenetrationBuffLayer;
    }
    public void AddDamage(int damage) {
        Damage += damage;
    }

    public int GetDamage()
    {
        return Damage;
    }

    public void AddMaxHP(int hp)
    {
        EnemyMaxHP += hp;
    }

    public void AddHPToMax()
    {
        EnemyHP = EnemyMaxHP;
    }

    public int GetDamageBuff() {
        int res = AttackBuffLayer;
        AttackBuffLayer = 0;
        return res;
    }

  


    public void Dispose(){SingletonProperty<EnemyState>.Dispose();}



}
