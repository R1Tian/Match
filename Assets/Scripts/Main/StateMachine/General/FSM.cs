using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace StateMachine.General
{
    public enum StateType
        {
            Idle,
            Attack,
            Dead,
            Treat,
            Charging
        }
    public class FSM : SerializedMonoBehaviour
    {
        public EnemyParameter enemyParameter=new EnemyParameter();
        public IState currentState;
        public Dictionary<StateType, IState> stateDic = new Dictionary<StateType, IState>();

        private void Awake() 
        {
            stateDic.Add(StateType.Idle, new IdleState(this));
            stateDic.Add(StateType.Attack, new AttackState(this));
            stateDic.Add(StateType.Dead, new DeadState(this));
            //TransitState(StateType.Idle);
        }
        public void TransitState(StateType type)
		{
			if (currentState != null)
				currentState.OnExit();
			currentState = stateDic[type];
			currentState.OnEnter();
		}

		//Where the method "XXState.OnUpdate()" run per frame;
		private void Update()
		{
			currentState.OnUpdate();
		}

    }
}

