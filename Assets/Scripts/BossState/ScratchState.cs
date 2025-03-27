using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScratchState : BossState
{
    [Header("Scratch Ref")]
    public Scratch scratch;
    public Transform scratchPoint;
    Animator animator;
    
    public float ExitTime;
    BossMonster bossMonster;


    public override StateName Name => StateName.Scratch;

    public override void Intialize(BossMonster bossMonster)
    {
        animator = bossMonster.GetComponent<Animator>();
    }

    private void Update()
    {
        var currentState = animator.GetCurrentAnimatorStateInfo(0);
        if (currentState.normalizedTime >= ExitTime)
        {
            bossMonster.ChangeState(StateName.Idle);
        }
    }

    public override void Enter()
    {
        animator.SetTrigger(BossMonster.SCRATCH);
        scratch.gameObject.SetActive(true);
    }

    public override void Exit()
    {
        scratch.gameObject.SetActive(false);
    }
}
