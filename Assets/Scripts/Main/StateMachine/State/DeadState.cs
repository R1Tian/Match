using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine.General;

public class DeadState : IState
{
    private FSM manager;
    private EnemyParameter enemyParameter;

    public DeadState(FSM _manager)
    {
        manager = _manager;
        enemyParameter=manager.enemyParameter;
    }
    public void OnEnter()
    {
        //播放死亡动画
        
    }

    public void OnExit()
    {
        
    }

    public void OnUpdate()
    {
        
    }
}

