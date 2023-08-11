using UnityEngine;

namespace SkillRelated {
    public class Skill
    {
        /// <summary>
        /// 造成伤害
        /// </summary>
        public static void Damage()
        {
            //Debug.Log(TestFunc.EnemyHP);
            //TestFunc.EnemyHP -= 2 + PlayerState.instance.GetDamageBuff();
            EnemyState.instance.TakeDamge(2 + PlayerState.instance.GetDamageBuff());
            //Debug.Log(TestFunc.EnemyHP);
        }
        /// <summary>
        /// 治疗
        /// </summary>
        public static void Heal()
        {
            PlayerState.instance.HealHealth(10);
            if (PlayerState.instance.GetHP() > PlayerState.instance.GetMaxHP())
            {
                PlayerState.instance.HealHealth(PlayerState.instance.GetMaxHP() - PlayerState.instance.GetHP());
            }
            Debug.Log(PlayerState.instance.GetHP());
        }


        public static void Power()
        {
            PlayerState.instance.AddAttackBuff(2);

        }

        public static void Defend()
        {
            PlayerState.instance.AddDefenceBuffLayer(2);
        }
    }
}
