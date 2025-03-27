using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class IdleState : BossState
{
    
    private BossState.StateName[] bossAttacks; 
    Animator animator;

    private string AniamatorStateName;
    public float ExitTime;

    public override StateName Name => StateName.Idle;
    
    private BossMonster bossMonster;

    public override void Intialize(BossMonster bossMonster)
    {
        this.bossMonster = bossMonster;
        animator = bossMonster.animator;
        bossAttacks = new BossState.StateName[] { StateName.Scratch, StateName.Breath };
    }

    private void Update()
    {
        var currentState = animator.GetCurrentAnimatorStateInfo(0);
        if (currentState.IsName(AniamatorStateName) == false) return;

        if (currentState.normalizedTime >= ExitTime)
        {
            int newState = Random.Range(0, bossAttacks.Length);
            bossMonster.ChangeState(bossAttacks[newState]);
        }
    }

    public override void Enter()
    {
    }

    public override void Exit()
    {
    }
}
