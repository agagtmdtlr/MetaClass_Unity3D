using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreathState : BossState
{
    [Header("Breath Ref")]
    public Breath breath;
    public Transform breathPoint;
    
    [Range(0f, 1f)]
    public float breathTime;

    public override StateName StateType => StateName.BreathState;
    
    public override void Initialize(BossMonster bossMonster)
    {
        InitializeDefault(bossMonster);
        
        breath.SetProperty(breathPoint, transform);
        breath.gameObject.SetActive(false);
    }

    public override void Update()
    {
        var currentState = Animator.GetCurrentAnimatorStateInfo(0);
        if (currentState.IsName(AnimatorStateName) == false) return;

        if (currentState.normalizedTime > breathTime && breath.gameObject.activeSelf == false)
        {
            breath.gameObject.SetActive(true);
        }
        
        if (currentState.normalizedTime > ExitTime)
        {
            Context.ChangeState(StateName.IdleState);
        }
    }
    
    public override void Enter()
    {
        Context.BeginAttack();
        breath.ResetColliders();
    }

    public override void Exit()
    {
        breath.gameObject.SetActive(false);
    }
}
