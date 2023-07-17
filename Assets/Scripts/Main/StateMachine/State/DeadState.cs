using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine.General;

public class DeadState : IState
{
    private FSM manager;
    private EnemyParameter enemyParameter;
    private bool isAnimationFinished = false;

    public DeadState(FSM _manager)
    {
        manager = _manager;
        enemyParameter=manager.enemyParameter;
    }
    public void OnEnter()
    {
        //播放死亡动画
        enemyParameter.enemyAnimator.Play("Dead");
        // 注册动画事件
        AnimationClip clip = enemyParameter.enemyAnimator.GetCurrentAnimatorClipInfo(0)[0].clip;
        AnimationEvent animationEvent = new AnimationEvent();
        animationEvent.functionName = "OnAnimationFinished";
        animationEvent.time = clip.length;
        clip.AddEvent(animationEvent);
        
    }

    public void OnExit()
    {
        
    }

    public void OnUpdate()
    {
        
    }
    public void OnAnimationFinished()
        {
            isAnimationFinished = true;
        }

        public void OnLateUpdate()
        {
            // 在LateUpdate中检查动画是否完成
            if (isAnimationFinished)
            {
                //死亡逻辑
            }
        }

}

