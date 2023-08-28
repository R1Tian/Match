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
            })
            .OnGUI(() => { });
        
        PlayerFSM.State(OrganismsState.Hurt)
            .OnCondition(() => true)
            .OnEnter(async () =>
            {
                await UniTask
                    .WaitUntil(Player.GetComponent<ObjectAnimation>().GetIdleComplete,
                        cancellationToken: CancellationTokenManager.battleCancellationToken)
                    .SuppressCancellationThrow();
                Player.GetComponent<ObjectAnimation>().Sparkle();
            })
            .OnExit(() =>
            {
                Player.transform.DOKill();
            })
            .OnUpdate(() => { })
            .OnGUI(() => { });
        
        PlayerFSM.State(OrganismsState.Attack)
            .OnCondition(() => true)
            .OnEnter(() => { })
            .OnExit(() =>
            {
                Player.transform.DOKill();
            })
            .OnUpdate(() => { })
            .OnGUI(() => { });
        
        EnemyFSM.State(OrganismsState.Idle)
            .OnCondition(() => true)
            .OnEnter(() => { })
            .OnExit(() =>
            {
                Enemy.transform.DOKill();
            })
            .OnUpdate(() => { })
            .OnGUI(() => { });
        
        EnemyFSM.State(OrganismsState.Hurt)
            .OnCondition(() => true)
            .OnEnter(() => { })
            .OnExit(() =>
            {
                Enemy.transform.DOKill();
            })
            .OnUpdate(() => { })
            .OnGUI(() => { });
        
        EnemyFSM.State(OrganismsState.Attack)
            .OnCondition(() => true)
            .OnEnter(() => { })
            .OnExit(() =>
            {
                Enemy.transform.DOKill();
            })
            .OnUpdate(() => { })
            .OnGUI(() => { });
        
        PlayerFSM.StartState(OrganismsState.Idle);
    }

    private void Update()
    {
        PlayerFSM.Update();
    }

    private void OnGUI()
    {
    }
}
