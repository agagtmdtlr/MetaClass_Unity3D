using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ScratchState : BossState
{
    public Scratch[] scratches;
    
    private Animator animator;
    private BossMonster bossMonster;
    
    public override StateName Name => StateName.ScratchState;

    public override void Initialize(BossMonster bossMonster)
    {
        animator = bossMonster.Animator;
        this.bossMonster = bossMonster;

        TriggerScratch(false);
    }

    private void Update()
    {
        var currentState = animator.GetCurrentAnimatorStateInfo(0);
        if (currentState.IsName(AnimatorStateName) == false) return;

        if (currentState.normalizedTime > ExitTime)
        {
            bossMonster.ChangeState(StateName.IdleState);
        }
    }

    void TriggerScratch(bool value)
    {
        foreach (Scratch scratch in scratches)
        {
            scratch.enabled = value;
        }
    }
    
    public override void Enter()
    {
        BossMonster.CurrentSceneBossMonster.BeginAttack();
        TriggerScratch(true);
    }

    public override void Exit()
    {
        TriggerScratch(false);
    }
}
