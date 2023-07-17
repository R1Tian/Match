using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine.General;

public class TreatState : IState
{
    private FSM manager;
    private EnemyParameter enemyParameter;

    public TreatState(FSM _manager)
    {
        manager = _manager;
        enemyParameter=manager.enemyParameter;
    }
    public void OnEnter()
    {
        //TODO:播放怪物治疗动画，播放结束后怪物回血
    }

    public void OnExit()
    {
        
    }

    public void OnUpdate()
    {
        
    }
}
