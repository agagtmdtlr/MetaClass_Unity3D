using System;
using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class SummonState : BossState
{
    [SerializeField] private CreatureSpawner creatureSpawner;
    
    [SerializeField, Range(0f,1f)] private float summonTime = 0f;
    bool spawned = false;
    public override StateName StateType => StateName.SummonState;
    public override void Initialize(BossMonster bossMonster)
    {
        InitializeDefault(bossMonster);
    }
    
    

    public override void Enter()
    {
        spawned = false;
    }

    public override void Update()
    {
        var currentState = Animator.GetCurrentAnimatorStateInfo(0);
        if (currentState.IsName(AnimatorStateName) == false) return;

        if (currentState.normalizedTime >= summonTime && spawned == false)
        {
            spawned = true;
            creatureSpawner.Spawn();
        }

        if (currentState.normalizedTime > ExitTime)
        {
            Context.ChangeState(StateName.IdleState);
        }
    }

    public override void Exit()
    {
    }
}
