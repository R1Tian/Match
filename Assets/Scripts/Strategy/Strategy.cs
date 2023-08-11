using SkillRelated;
using UnityEngine;
namespace StrategyMethod {
    public class Attack_Red_Normal : IStrategy
    {
        public void ExcuteStrategy()
        {
            Skill.Damage();
        }

        public void ExcuteStrategyByInput(int level)
        {
            return;
        }
    }

    public class Heal_Green_Heal : IStrategy {
        public void ExcuteStrategy() {
            Skill.Heal();
        }

        public void ExcuteStrategyByInput(int level)
        {
            return;
        }
    }

    public class Power_Yellow_Power : IStrategy {
        public void ExcuteStrategy() {
            Skill.Power();
        }

        public void ExcuteStrategyByInput(int level)
        {
            return;
        }
    }
}
