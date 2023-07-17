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
        enemyParameter.enemyAnimator.Play("Idle");
    }

    public void OnExit()
    {
        
    }

    public void OnUpdate()
    {
        if(enemyParameter.currentStep>2)
        {
            enemyParameter.currentStep=0;
            enemyParameter.attackCoefficien++;
        }
        if(enemyParameter.isActived&&enemyParameter.currentStep==0)
        {
            enemyParameter.isActived=false;
            enemyParameter.currentStep++;
            manager.TransitState(StateType.Attack);
        }
        else if(enemyParameter.isActived&&enemyParameter.currentStep==1)
        {
            enemyParameter.isActived=false;
            enemyParameter.currentStep++;
            manager.TransitState(StateType.Treat);
        }
        else if(enemyParameter.isActived&&enemyParameter.currentStep==2)
        {
            enemyParameter.isActived=false;
            enemyParameter.currentStep++;
            manager.TransitState(StateType.Charging);
        }
        
    }
}
