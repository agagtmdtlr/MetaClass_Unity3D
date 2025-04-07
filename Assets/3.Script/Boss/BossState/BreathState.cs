using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreathState : BossState
{
    [Header("Breath Ref")]
    public Breath breath;
    public Transform breathPoint;

    public override StateName Name => StateName.BreathState;
    
    public override void Initialize(BossMonster bossMonster)
    {
        InitializeDefault(bossMonster);
        
        breath.SetProperty(breathPoint, transform);
        breath.gameObject.SetActive(false);
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
    
    public override void Enter()
    {
        BossMonster.CurrentSceneBossMonster.BeginAttack();
        breath.gameObject.SetActive(true);
    }

    public override void Exit()
    {
        breath.gameObject.SetActive(false);
    }
}
