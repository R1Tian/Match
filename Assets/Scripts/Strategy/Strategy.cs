using SkillRelated;
using UnityEngine;
namespace StrategyMethod {
    public class Attack_Red_Normal : IStrategy
    {
        public void ExcuteStrategy()
        {
            Skill.Damage();
        }
    }

    public class Heal_Green_Heal : IStrategy {
        public void ExcuteStrategy() {
            Skill.Heal();
        }
    }

    public class Power_Yellow_Power : IStrategy {
        public void ExcuteStrategy() {
            Skill.Power();
        }
    }
}
