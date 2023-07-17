using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using StateMachine.General;


public class StateManager : SerializedMonoBehaviour
{
    public Enemy_01FSM enemy_01FSM;
    public float playerSteps=0;
    private void Start() 
    {
        enemy_01FSM = GetComponent<Enemy_01FSM>();
        enemy_01FSM.enemyParameter.enemyAnimator=GetComponent<Animator>();
        
    }
    private void Update() 
    {
        if(playerSteps>3)//三个回合后
        {
            enemy_01FSM.enemyParameter.isActived=true;

        }
        if(!enemy_01FSM.enemyParameter.isActived)
        {
            enemy_01FSM.TransitState(StateType.Idle);
        }
        if(enemy_01FSM.enemyParameter.currentHealth<=0)
        {
            enemy_01FSM.TransitState(StateType.Dead);
        }
        
        
    }
}
