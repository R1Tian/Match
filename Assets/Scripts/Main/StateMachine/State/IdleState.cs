using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine.General;

public class IdleState : IState
{
    private FSM manager;
    private EnemyParameter enemyParameter;

    public IdleState(FSM _manager)
    {
        manager = _manager;
        enemyParameter=manager.enemyParameter;
    }
    public void OnEnter()
    {
        //TODO:播放怪物Idle动画
    }

    public void OnExit()
    {
        
    }

    public void OnUpdate()
    {
        
    }
}
