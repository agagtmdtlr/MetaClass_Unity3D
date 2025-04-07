using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ScratchState : BossState
{
    public Scratch[] scratches;
    
    public override StateName Name => StateName.ScratchState;

    public override void Initialize(BossMonster bossMonster)
    {
        InitializeDefault(bossMonster);
        TriggerScratch(false);
    }

    private void Update()
    {
        var currentState = Animator.GetCurrentAnimatorStateInfo(0);
        if (currentState.IsName(AnimatorStateName) == false) return;

        if (currentState.normalizedTime > ExitTime)
        {
            BossMonster.ChangeState(StateName.IdleState);
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
