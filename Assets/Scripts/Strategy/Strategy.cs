using SkillRelated;
using UnityEngine;
namespace StrategyMethod {
    public class Attack_Red_Normal : IStrategy
    {
        public void ExcuteStrategy()
        {
            return;
        }

        public void ExcuteStrategyByInput(int level)
        {
            Skill.Damage(level);
        }
        
    }

    public class Heal_Green_Heal : IStrategy {
        public void ExcuteStrategy() {
            return;
        }

        public void ExcuteStrategyByInput(int level)
        {
            Skill.Heal(level);
            
        }
    }

    public class Power_Yellow_Power : IStrategy {
        public void ExcuteStrategy() {
            return;
        }

        public void ExcuteStrategyByInput(int level)
        {
            Skill.Power(level);
        }
    }

    public class Vampire_Red_O : IStrategy
    {
        public void ExcuteStrategy() {
            return;
        }

        public void ExcuteStrategyByInput(int level)
        {
            Skill.LowDamage(level);
            Skill.LowHeal(level);
        }
    }

    public class ContHeal_Green_O : IStrategy
    {
        public void ExcuteStrategy() {
            return;
        }

        public void ExcuteStrategyByInput(int level)
        {
            Skill.ContLowHeal(level);
        }
    }
    
    public class Defend_Blue_Z : IStrategy
    {
        public void ExcuteStrategy() {
            return;
        }

        public void ExcuteStrategyByInput(int level)
        {
            Skill.Defend(level);
        }
    }
    
    public class Defend_Blue_O : IStrategy
    {
        public void ExcuteStrategy() {
            return;
        }

        public void ExcuteStrategyByInput(int level)
        {
            Skill.ContDefend(level);
        }
    }
    
    public class AddTurtleShell_Yellow_O : IStrategy
    {
        public void ExcuteStrategy() {
            return;
        }

        public void ExcuteStrategyByInput(int level)
        {
            Skill.AddTurtleShell(level);
        }
    }
    
    public class AddFlexibility_Yellow_L : IStrategy
    {
        public void ExcuteStrategy() {
            return;
        }

        public void ExcuteStrategyByInput(int level)
        {
            Skill.AddFlexibility(level);
        }
    }

    public class AddArmorPenetrationToEnemy_Yellow_I : IStrategy
    {
        public void ExcuteStrategy() {
            return;
        }

        public void ExcuteStrategyByInput(int level)
        {
            Skill.AddArmorPenetrationToEnemy(level);
        }
    }
    
    public class HighHealAndForbidHeal_Green_I : IStrategy
    {
        public void ExcuteStrategy() {
            return;
        }

        public void ExcuteStrategyByInput(int level)
        {
            Skill.HighHeal(level);
            Skill.ForbidHeal(level);
        }
    }
    
    public class DefendWithCurHurt_Blue_T : IStrategy
    {
        public void ExcuteStrategy() {
            return;
        }

        public void ExcuteStrategyByInput(int level)
        {
            Skill.DefendWithCurHurt(level);
        }
    }
    
    public class Eject_Red_T : IStrategy
    {
        public void ExcuteStrategy() {
            return;
        }

        public void ExcuteStrategyByInput(int level)
        {
            Skill.DamageWithDefence(level);
        }
    }
    
    public class GoldenGuardian_Yellow_T : IStrategy
    {
        public void ExcuteStrategy() {
            return;
        }

        public void ExcuteStrategyByInput(int level)
        {
            Skill.AddArmorFeedback(level);
        }
    }
}
