using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using StateMachine.General;

namespace StateMachine.General
{
   
    public class Enemy_01FSM : FSM
    {
        private void Awake() 
        {
            stateDic.Add(StateType.Idle, new IdleState(this));
            stateDic.Add(StateType.Attack, new AttackState(this));
            stateDic.Add(StateType.Dead, new DeadState(this));
            stateDic.Add(StateType.Treat, new TreatState(this));
            stateDic.Add(StateType.Charging, new ChargingState(this));
             
        }

    }
}
