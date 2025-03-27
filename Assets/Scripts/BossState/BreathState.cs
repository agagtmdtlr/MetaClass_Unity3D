using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BreathState : BossState
{
    [Header("Breath Ref")]
    public Breath breath;
    public Transform breathPoint;
    Animator animator;
    public float ExitTime;
    BossMonster bossMonster;

    public override StateName Name => StateName.Breath;
    

    public override void Intialize(BossMonster bossMonster)
    {
        this.bossMonster = bossMonster;
        animator = bossMonster.GetComponent<Animator>();
        breath.gameObject.SetActive(false);
        breath.SetProperty(breathPoint, breathPoint);
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
        animator.SetTrigger(BossMonster.BREATH);

        breath.ResetCollider();
        breath.gameObject.SetActive(true);
    }

    public override void Exit()
    {
        breath.gameObject.SetActive(false);
    }
}
