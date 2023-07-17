using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine.General;

public class ChargingState : IState
{
    private FSM manager;
    private EnemyParameter enemyParameter;

    public ChargingState(FSM _manager)
    {
        manager = _manager;
        enemyParameter=manager.enemyParameter;
    }
    public void OnEnter()
    {
        //TODO:播放怪物充能动画，播放结束后跳转到Idle状态
    }

    public void OnExit()
    {
        
    }

    public void OnUpdate()
    {
        
    }
}
