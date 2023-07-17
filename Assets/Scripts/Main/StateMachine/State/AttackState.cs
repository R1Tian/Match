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
    }
    public void OnEnter()
    {
        Debug.Log("AttackState OnEnter");
    }

    public void OnExit()
    {
        Debug.Log("AttackState OnExit");
    }

    public void OnUpdate()
    {
        Debug.Log("AttackState OnUpdate");
    }
}

