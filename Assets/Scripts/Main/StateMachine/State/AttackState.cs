using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine.General;

public class AttackState : IState
{
    private FSM manager;
    private EnemyParameter enemyParameter;
    private bool isAnimationFinished = false;

    public AttackState(FSM _manager)
    {
        manager = _manager;
        enemyParameter=manager.enemyParameter;
    }
    public void OnEnter()
    {
        //TODO:播放攻击动画，播放结束后对玩家造成伤害
        enemyParameter.enemyAnimator.Play("Attack");
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
                //TODO:攻击逻辑

                manager.TransitState(StateType.Idle);
            }
        }   
}

