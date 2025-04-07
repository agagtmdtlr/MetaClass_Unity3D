using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ScratchState : BossState
{
    public Scratch[] scratches;
    
    public override StateName StateType => StateName.ScratchState;

    public override void Initialize(BossMonster bossMonster)
    {
        InitializeDefault(bossMonster);
        TriggerScratch(false);
    }

    public override void Update()
    {
        var currentState = Animator.GetCurrentAnimatorStateInfo(0);
        if (currentState.IsName(AnimatorStateName) == false) return;

        if (currentState.normalizedTime > ExitTime)
        {
            Context.ChangeState(StateName.IdleState);
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
        Context.BeginAttack();
        TriggerScratch(true);
    }

    public override void Exit()
    {
        TriggerScratch(false);
    }
}
