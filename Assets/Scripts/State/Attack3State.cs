using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Attack3State : AttackState
{
    private static readonly int ATTACK = Animator.StringToHash("Attack-3");
    public override StateName stateName =>  StateName.Attack3;
    
    public override void EnterState()
    {
        base.EnterState();
        stateAnimator.CrossFade(ATTACK,0.1f, stateLayerIndex);
    }

    protected override void Update()
    {
        base.Update();
        
        var stateInfo = stateAnimator.GetCurrentAnimatorStateInfo(0);
        float normalizeTime = stateInfo.normalizedTime;
        if (normalizeTime > 1f)
        {
            stateCharacter.ChangestState(StateName.ReadyAttack, stateLayerIndex);
        }
    }

}
