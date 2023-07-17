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
    }
    public void OnEnter()
    {
        Debug.Log("DeadState OnEnter");
    }

    public void OnExit()
    {
        Debug.Log("DeadState OnExit");
    }

    public void OnUpdate()
    {
        Debug.Log("DeadState OnUpdate");
    }
}

