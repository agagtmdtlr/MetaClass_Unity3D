using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class IdleState : BossState
{
    private int[] bossAttacks;
    
    public override StateName StateType => StateName.IdleState;

    public override void Initialize(BossMonster bossMonster)
    {
        InitializeDefault(bossMonster);
        bossAttacks = new[] { BossMonster.SCRATCH, BossMonster.BREATH, BossMonster.SUMMON };
    }

    public override void Update()
    {
        var currentState = Animator.GetCurrentAnimatorStateInfo(0);
        if (currentState.IsName(AnimatorStateName) == false) return;

        /*if (currentState.normalizedTime > ExitTime)
        {
            int nextAttackTrigger = Random.Range(0, bossAttacks.Length); // 0 => 스크래치, 1 => 브레스
            int stateValue = nextAttackTrigger + 1; // 1 => 스크래치 상태, 2=> 브레스 상태
            Animator.SetTrigger(bossAttacks[nextAttackTrigger]);
            Context.ChangeState((StateName)stateValue);
        }*/
    }
    
    public override void Enter()
    {
        
    }

    public override void Exit()
    {
        
    }

  
}
