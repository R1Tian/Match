using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine.General;

public class ChargingState : IState
{
    private FSM manager;
    private EnemyParameter enemyParameter;
    private bool isAnimationFinished = false;

    public ChargingState(FSM _manager)
    {
        manager = _manager;
        enemyParameter=manager.enemyParameter;
    }
    public void OnEnter()
    {
        //TODO:播放怪物充能动画，播放结束后跳转到Idle状态
        enemyParameter.enemyAnimator.Play("Charging");
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
                //TODO:蓄力逻辑

                manager.TransitState(StateType.Idle);
            }
        }  
}
