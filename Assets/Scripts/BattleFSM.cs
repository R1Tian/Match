using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using QFramework;
using UnityEngine;

public class BattleFSM : MonoBehaviour
{
    public enum OrganismsState
    {
        Idle,
        Attack,
        Hurt,
    }

    public FSM<OrganismsState> PlayerFSM = new FSM<OrganismsState>();
    public FSM<OrganismsState> EnemyFSM = new FSM<OrganismsState>();

    public GameObject Player;
    public GameObject Enemy;

    // public void Awake()
    // {
    //     throw new NotImplementedException();
    // }

    private void Start()
    {
        PlayerFSM.State(OrganismsState.Idle)
            .OnCondition(() => true)
            .OnEnter(() =>
            { 
                Player.GetComponent<ObjectAnimation>().Idle(PlayerFSM);
            })
            .OnExit(() => { })
            .OnUpdate(() =>
            {
                if (PlayerState.instance.GetIsHurt())
                {
                    PlayerFSM.ChangeState(OrganismsState.Hurt);
                }

                if (EnemyState.instance.GetIsHurt())
                {
                    PlayerFSM.ChangeState(OrganismsState.Attack);
                }
            })
            .OnGUI(() => { });
        
        PlayerFSM.State(OrganismsState.Hurt)
            .OnCondition(() => true)
            .OnEnter(async () =>
            {
                // await UniTask
                //     .WaitUntil(Player.GetComponent<ObjectAnimation>().GetIdleComplete,
                //         cancellationToken: CancellationTokenManager.battleCancellationToken)
                //     .SuppressCancellationThrow();
                Player.GetComponent<ObjectAnimation>().Sparkle();
            })
            .OnExit(() =>
            {
                PlayerState.instance.SetNotHurt();
                //Player.transform.DOKill();
            })
            .OnUpdate(() =>
            {
                Debug.Log(Player.GetComponent<ObjectAnimation>().GetSparkleComplete());
                if (Player.GetComponent<ObjectAnimation>().GetSparkleComplete())
                {
                    Debug.Log("Hurt切换Idle");
                    PlayerFSM.ChangeState(OrganismsState.Idle);
                }
            })
            .OnGUI(() => { });
        
        PlayerFSM.State(OrganismsState.Attack)
            .OnCondition(() => true)
            .OnEnter(async () =>
            {
                // await UniTask
                //     .WaitUntil(Player.GetComponent<ObjectAnimation>().GetIdleComplete,
                //         cancellationToken: CancellationTokenManager.battleCancellationToken)
                //     .SuppressCancellationThrow();
                Player.GetComponent<ObjectAnimation>().Shake();
                
            })
            .OnExit(() =>
            {
                //EnemyState.instance.SetNotHurt();
                //Player.transform.DOKill();
            })
            .OnUpdate(() =>
            {
                Debug.Log(Player.GetComponent<ObjectAnimation>().GetShakeComplete());
                if (Player.GetComponent<ObjectAnimation>().GetShakeComplete())
                {
                    Debug.Log("Attack切换Idle");
                    PlayerFSM.ChangeState(OrganismsState.Idle);
                }
            })
            .OnGUI(() => { });
        
        EnemyFSM.State(OrganismsState.Idle)
            .OnCondition(() => true)
            .OnEnter(() =>
            {
                Enemy.GetComponent<ObjectAnimation>().Idle(EnemyFSM);
            })
            .OnExit(() =>
            {
                //Enemy.transform.DOKill();
            })
            .OnUpdate(() =>
            {
                Debug.Log("enemy:"+EnemyState.instance.GetIsHurt());
                if (PlayerState.instance.GetIsHurt())
                {
                    EnemyFSM.ChangeState(OrganismsState.Attack);
                }

                if (EnemyState.instance.GetIsHurt())
                {
                    EnemyFSM.ChangeState(OrganismsState.Hurt);
                }
            })
            .OnGUI(() => { });
        
        EnemyFSM.State(OrganismsState.Hurt)
            .OnCondition(() => true)
            .OnEnter(() =>
            {
                Debug.Log("enemy进入hurt");
                Enemy.GetComponent<ObjectAnimation>().Sparkle();
            })
            .OnExit(() =>
            {
                Debug.Log("enemy不受伤");
                EnemyState.instance.SetNotHurt();
                //Enemy.transform.DOKill();
            })
            .OnUpdate(() =>
            {
                if (Enemy.GetComponent<ObjectAnimation>().GetSparkleComplete())
                {
                    EnemyFSM.ChangeState(OrganismsState.Idle);
                }
            })
            .OnGUI(() => { });
        
        EnemyFSM.State(OrganismsState.Attack)
            .OnCondition(() => true)
            .OnEnter(() =>
            {
                Enemy.GetComponent<ObjectAnimation>().Shake();
            })
            .OnExit(() =>
            {
                //Enemy.transform.DOKill();
            })
            .OnUpdate(() =>
            {
                if (Enemy.GetComponent<ObjectAnimation>().GetShakeComplete())
                {
                    EnemyFSM.ChangeState(OrganismsState.Idle);
                }
            })
            .OnGUI(() => { });
        
        PlayerFSM.StartState(OrganismsState.Idle);
        EnemyFSM.StartState(OrganismsState.Idle);
    }

    private void Update()
    {
        PlayerFSM.Update();
        EnemyFSM.Update();
    }

    private void OnGUI()
    {
    }
}
