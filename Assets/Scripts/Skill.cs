using UnityEngine;

namespace SkillRelated {
    public class Skill
    {
        /// <summary>
        /// 造成伤害(2-3-5)
        /// </summary>
        public static void Damage(int level)
        {
            //Debug.Log(TestFunc.EnemyHP);
            //TestFunc.EnemyHP -= 2 + PlayerState.instance.GetDamageBuff();
            switch (level)
            {
                case 1:
                    EnemyState.instance.TakeDamge(2);
                    break;
                case 2:
                    EnemyState.instance.TakeDamge(3);
                    break;
                case 3:
                    EnemyState.instance.TakeDamge(5);
                    break;
            }
            
            //Debug.Log(TestFunc.EnemyHP);
        }
        
        /// <summary>
        /// 造成伤害(1-2-3)
        /// </summary>
        public static void LowDamage(int level)
        {
            //Debug.Log(TestFunc.EnemyHP);
            //TestFunc.EnemyHP -= 2 + PlayerState.instance.GetDamageBuff();
            switch (level)
            {
                case 1:
                    EnemyState.instance.TakeDamge(1);
                    break;
                case 2:
                    EnemyState.instance.TakeDamge(2);
                    break;
                case 3:
                    EnemyState.instance.TakeDamge(3);
                    break;
            }
            
            //Debug.Log(TestFunc.EnemyHP);
        }
        
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
        /// 持续治疗
        /// </summary>
        /// <param name="level"></param>
        public static void ContLowHeal(int level)
        {
            BuffManager.instance.ApplyDelayEffect(1,level, () =>FixedHeal(1));
        }
        
        /// <summary>
        /// 治疗(2-3-4)
        /// </summary>
        public static void Heal(int level)
        {
            switch (level)
            {
                case 1:
                    PlayerState.instance.HealHealth(2);
                    break;
                case 2:
                    PlayerState.instance.HealHealth(3);
                    break;
                case 3:
                    PlayerState.instance.HealHealth(4);
                    break;
            }
            
            if (PlayerState.instance.GetHP() > PlayerState.instance.GetMaxHP())
            {
                PlayerState.instance.HealHealth(PlayerState.instance.GetMaxHP() - PlayerState.instance.GetHP());
            }
            //Debug.Log(PlayerState.instance.GetHP());
        }
        
        /// <summary>
        /// 固定量治疗
        /// </summary>
        /// <param name="count">治疗量</param>
        public static void FixedHeal(int count)
        {
            PlayerState.instance.HealHealth(count);
            if (PlayerState.instance.GetHP() > PlayerState.instance.GetMaxHP())
            {
                PlayerState.instance.HealHealth(PlayerState.instance.GetMaxHP() - PlayerState.instance.GetHP());
            }
        }

        /// <summary>
        /// 治疗（1-2-3）
        /// </summary>
        /// <param name="level"></param>
        public static void LowHeal(int level)
        {
            switch (level)
            {
                case 1:
                    PlayerState.instance.HealHealth(1);
                    break;
                case 2:
                    PlayerState.instance.HealHealth(2);
                    break;
                case 3:
                    PlayerState.instance.HealHealth(3);
                    break;
            }
            
            if (PlayerState.instance.GetHP() > PlayerState.instance.GetMaxHP())
            {
                PlayerState.instance.HealHealth(PlayerState.instance.GetMaxHP() - PlayerState.instance.GetHP());
            }
            //Debug.Log(PlayerState.instance.GetHP());
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
        /// 防御（2-3-4）
        /// </summary>
        /// <param name="level"></param>
        public static void Defend(int level)
        {
            switch (level)
            {
                case 1:
                    PlayerState.instance.AddDefenceBuffLayer(2);
                    break;
                case 2:
                    PlayerState.instance.AddDefenceBuffLayer(3);
                    break;
                case 3:
                    PlayerState.instance.AddDefenceBuffLayer(4);
                    break;
            }
            
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
            BuffManager.instance.ApplyDelayEffect(1,level, () =>FixedDefend(1));
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
    }
}
