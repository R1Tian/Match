using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine.General;

public class TreatState : IState
{
    private FSM manager;
    private EnemyParameter enemyParameter;
    private bool isAnimationFinished = false;

    public TreatState(FSM _manager)
    {
        manager = _manager;
        enemyParameter=manager.enemyParameter;
    }
    public void OnEnter()
    {
        //TODO:播放怪物治疗动画，播放结束后怪物回血
        enemyParameter.enemyAnimator.Play("Treat");
    }

    public void OnExit()
    {
        
    }

    public void OnUpdate()
    {
        
    }
    public void OnAnimationFinished()
        {
            isAnimationFinished = true;
        }

    public void OnLateUpdate()
        {
            // 在LateUpdate中检查动画是否完成
            if (isAnimationFinished)
            {
                if(enemyParameter.currentHealth+enemyParameter.treatMent*enemyParameter.attackCoefficien>enemyParameter.maxHealth)
                {
                    enemyParameter.currentHealth=enemyParameter.maxHealth;
                }
                
                //治疗逻辑
                enemyParameter.currentHealth+=enemyParameter.treatMent*enemyParameter.attackCoefficien;
                manager.TransitState(StateType.Idle);
            }
        }
}
