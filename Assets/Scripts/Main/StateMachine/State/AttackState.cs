using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine.General;

public class AttackState : IState
{
    private FSM manager;
    private EnemyParameter enemyParameter;

    public AttackState(FSM _manager)
    {
        manager = _manager;
        enemyParameter=manager.enemyParameter;
    }
    public void OnEnter()
    {
        //TODO:播放攻击动画，播放结束后对玩家造成伤害
    }

    public void OnExit()
    {
        
    }

    public void OnUpdate()
    {
        
    }
}

