using System;
using UnityEngine;

namespace SkillRelated {
    public class Skill
    {
        
        #region DamageRelated
        /// <summary>
        /// 造成伤害(6-7-9)
        /// </summary>
        public static void Damage(int level)
        {
            //Debug.Log(TestFunc.EnemyHP);
            //TestFunc.EnemyHP -= 2 + PlayerState.instance.GetDamageBuff();
            switch (level)
            {
                case 1:
                    EnemyState.instance.TakeDamge(6);
                    break;
                case 2:
                    EnemyState.instance.TakeDamge(7);
                    break;
                case 3:
                    EnemyState.instance.TakeDamge(9);
                    break;
            }
            
            //Debug.Log(TestFunc.EnemyHP);
        }
        
        /// <summary>
        /// 造成伤害(4-5-6)
        /// </summary>
        public static void LowDamage(int level)
        {
            //Debug.Log(TestFunc.EnemyHP);
            //TestFunc.EnemyHP -= 2 + PlayerState.instance.GetDamageBuff();
            switch (level)
            {
                case 1:
                    EnemyState.instance.TakeDamge(4);
                    break;
                case 2:
                    EnemyState.instance.TakeDamge(5);
                    break;
                case 3:
                    EnemyState.instance.TakeDamge(6);
                    break;
            }
            
            //Debug.Log(TestFunc.EnemyHP);
        }

        /// <summary>
        /// 根据护甲值造成伤害(0.5-1-2倍)
        /// </summary>
        public static void DamageWithDefence(int level)
        {
            int defence = PlayerState.instance.GetDefenceBuffLayer();
            switch (level)
            {
                case 1:
                    EnemyState.instance.TakeDamge(Mathf.FloorToInt(defence * 0.5f));
                    break;
                case 2:
                    EnemyState.instance.TakeDamge(Mathf.FloorToInt(defence * 1f));
                    break;
                case 3:
                    EnemyState.instance.TakeDamge(Mathf.FloorToInt(defence * 2f));
                    break;
            }
            
            //Debug.Log(TestFunc.EnemyHP);
        }
        #endregion

        #region BuffRelate
        /// <summary>
        /// 力量（1-2-3）
        /// </summary>
        /// <param name="level"></param>
        public static void Power(int level)
        {
            switch (level)
            {
                case 1:
                    PlayerState.instance.AddAttackBuff(1);
                    break;
                case 2:
                    PlayerState.instance.AddAttackBuff(2);
                    break;
                case 3:
                    PlayerState.instance.AddAttackBuff(3);
                    break;
            }
            

        }

        /// <summary>
        /// 给敌人增加破甲buff
        /// </summary>
        /// <param name="level"></param>
        public static void AddArmorPenetrationToEnemy(int level)
        {
            switch (level)
            {
                case 1:
                    EnemyState.instance.AddArmorPenetrationBuffLayer(1);
                    break;
                case 2:
                    EnemyState.instance.AddArmorPenetrationBuffLayer(2);
                    break;
                case 3:
                    EnemyState.instance.AddArmorPenetrationBuffLayer(3);
                    break;
            }
        }
        
        /// <summary>
        /// 生成迅疾buff（1-2-3）
        /// </summary>
        /// <param name="level"></param>
        public static void AddFlexibility(int level)
        {
            switch (level)
            {
                case 1:
                    PlayerState.instance.AddFlexibilityBuffLayer(1);
                    break;
                case 2:
                    PlayerState.instance.AddFlexibilityBuffLayer(2);
                    break;
                case 3:
                    PlayerState.instance.AddFlexibilityBuffLayer(3);
                    break;
            }
        }

        /// <summary>
        /// 增加龟甲buff
        /// </summary>
        /// <param name="level"></param>
        public static void AddTurtleShell(int level)
        {
            switch (level)
            {
                case 1:
                    PlayerState.instance.AddTurtleShellBuffLayer(1);
                    break;
                case 2:
                    PlayerState.instance.AddTurtleShellBuffLayer(2);
                    break;
                case 3:
                    PlayerState.instance.AddTurtleShellBuffLayer(3);
                    break;
            }
        }
        
        /// <summary>
        /// 不准治疗
        /// </summary>
        /// <param name="level"></param>
        public static void ForbidHeal(int level)
        {
            PlayerState.instance.ForbidHeal();
            BuffManager.instance.ApplyBuffByID(4, 4 - level,4 - level, BuffManagerUI.PlayerBuff,() => PlayerState.instance.AllowHeal());
        }

        /// <summary>
        /// 获得护甲回馈
        /// </summary>
        /// <param name="level"></param>
        public static void AddArmorFeedback(int level)
        {
            PlayerState.instance.AllowArmorFeedback();
            BuffManager.instance.ApplyBuffByID(6,level,level, BuffManagerUI.PlayerBuff,() => PlayerState.instance.ForbidArmorFeedback());
        }
        
        /// <summary>
        /// 获得治疗转化
        /// </summary>
        /// <param name="level"></param>
        public static void AddHealingConversion(int level)
        {
            PlayerState.instance.AllowIsHealingConversion();
            BuffManager.instance.ApplyBuffByID(9,level,level, BuffManagerUI.PlayerBuff,() => PlayerState.instance.ForbidIsHealingConversion());
        }
        
        #endregion

        #region HealRelated

        /// <summary>
        /// 持续治疗
        /// </summary>
        /// <param name="level"></param>
        public static void ContLowHeal(int level)
        {
            BuffManager.instance.ApplyBuffByID(7,1,level, BuffManagerUI.PlayerBuff,() =>FixedHeal(1));
        }
        
        /// <summary>
        /// 治疗(8-10-12)
        /// </summary>
        public static void Heal(int level)
        {
            switch (level)
            {
                case 1:
                    PlayerState.instance.HealHealth(8);
                    break;
                case 2:
                    PlayerState.instance.HealHealth(10);
                    break;
                case 3:
                    PlayerState.instance.HealHealth(12);
                    break;
            }
        }
        
        /// <summary>
        /// 固定量治疗
        /// </summary>
        /// <param name="count">治疗量</param>
        public static void FixedHeal(int count)
        {
            PlayerState.instance.HealHealth(count);
        }

        /// <summary>
        /// 治疗（6-7-8）
        /// </summary>
        /// <param name="level"></param>
        public static void LowHeal(int level)
        {
            switch (level)
            {
                case 1:
                    PlayerState.instance.HealHealth(6);
                    break;
                case 2:
                    PlayerState.instance.HealHealth(7);
                    break;
                case 3:
                    PlayerState.instance.HealHealth(8);
                    break;
            }
        }

        /// <summary>
        /// 治疗（5-10-20）
        /// </summary>
        /// <param name="level"></param>
        public static void HighHeal(int level)
        {
            switch (level)
            {
                case 1:
                    PlayerState.instance.HealHealth(5);
                    break;
                case 2:
                    PlayerState.instance.HealHealth(10);
                    break;
                case 3:
                    PlayerState.instance.HealHealth(20);
                    break;
            }
        }

        
        #endregion
        
        #region DefendRelated
        /// <summary>
        /// 防御（6-7-9）
        /// </summary>
        /// <param name="level"></param>
        public static void Defend(int level)
        {
            switch (level)
            {
                case 1:
                    PlayerState.instance.AddDefenceBuffLayer(6);
                    break;
                case 2:
                    PlayerState.instance.AddDefenceBuffLayer(7);
                    break;
                case 3:
                    PlayerState.instance.AddDefenceBuffLayer(9);
                    break;
            }
            
        }

        /// <summary>
        /// 防御（4-5-6）
        /// </summary>
        /// <param name="level"></param>
        public static void LowDefend(int level)
        {
            switch (level)
            {
                case 1:
                    PlayerState.instance.AddDefenceBuffLayer(4);
                    break;
                case 2:
                    PlayerState.instance.AddDefenceBuffLayer(5);
                    break;
                case 3:
                    PlayerState.instance.AddDefenceBuffLayer(6);
                    break;
            }
            
        }

        /// <summary>
        /// 倍率防御
        /// </summary>
        /// <param name="level">倍率（等级）</param>
        /// <param name="count">固定量</param>
        public static void MagnificationDefend(int level,int count)
        {
            FixedDefend(level * count);
        }
        
        /// <summary>
        /// 固定量防御
        /// </summary>
        /// <param name="count">数值</param>
        public static void FixedDefend(int count)
        {
            PlayerState.instance.AddDefenceBuffLayer(count);
        }
        
        /// <summary>
        /// 持续防御
        /// </summary>
        /// <param name="level"></param>
        public static void ContDefend(int level)
        {
            BuffManager.instance.ApplyBuffByID(8,1,level, BuffManagerUI.PlayerBuff,() =>FixedDefend(1));
        }

        /// <summary>
        /// 根据已损失生命值生成护甲
        /// </summary>
        /// <param name="level"></param>
        public static void DefendWithCurHurt(int level)
        {
            int curHurt = PlayerState.instance.GetMaxHP() - PlayerState.instance.GetHP();
            switch (level)
            {
                case 1:
                    PlayerState.instance.AddDefenceBuffLayer(Mathf.FloorToInt(0.02f * curHurt));
                    break;
                case 2:
                    PlayerState.instance.AddDefenceBuffLayer(Mathf.FloorToInt(0.03f * curHurt));
                    break;
                case 3:
                    PlayerState.instance.AddDefenceBuffLayer(Mathf.FloorToInt(0.04f * curHurt));
                    break;
            }
        }
        

        #endregion
        
        
        

        

        
    }
}
