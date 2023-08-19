using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class EnemyState
{
    public void OnLoadEnemyMachine()
    {
        //FSMManager.FindStateMachine(ID).OnExcute();
    }

    public int GetHP()
    {
        return CurHP;
    }

    public int GetMaxHP()
    {
        return MaxHP;
    }

    public int GetDamage()
    {
        return AttackVal;
    }

    
    public void AddMaxHP(int val)
    {
        MaxHP += val;
    }
    
    public void AddHPToMax()
    {
        CurHP = MaxHP;
    }
    
    public void TakeDamge(int val) {

        CurHP -= (int)Mathf.Floor((val + PlayerState.instance.GetAttackBuff()) * Mathf.Pow(1.5f, GetArmorPenetrationBuffLayer()));
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
    
    public void AddArmorPenetrationBuffLayer(int layer)
    {
        ArmorPenetrationBuffLayer += layer;
    }

    public void DropArmorPenetrationBuffLayer(int layer) {
        ArmorPenetrationBuffLayer -= layer;
    }
    public int GetArmorPenetrationBuffLayer() {
        return ArmorPenetrationBuffLayer;
    }
}
