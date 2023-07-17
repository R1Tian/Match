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
    }
    public void OnEnter()
    {
        Debug.Log("IdleState OnEnter");
    }

    public void OnExit()
    {
        Debug.Log("IdleState OnExit");
    }

    public void OnUpdate()
    {
        Debug.Log("IdleState OnUpdate");
    }
}
