using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Attack2State : AttackState
{
    private static readonly int ATTACK = Animator.StringToHash("Attack-2");
    public override StateName stateName =>  StateName.Attack2;
    bool nextAttack = false;
    
    public override void EnterState()
    {
        base.EnterState();
        stateAnimator.CrossFade(ATTACK,0.1f, stateLayerIndex);
        nextAttack = false;
    }

    protected override void Update()
    {
        base.Update();
        
        var stateInfo = stateAnimator.GetCurrentAnimatorStateInfo(stateLayerIndex);
        float normalizeTime = stateInfo.normalizedTime;
        if (Input.GetMouseButtonDown(0) && 0.4f < normalizeTime && normalizeTime < 0.85f)
        {
            nextAttack = true;
        }

        if (normalizeTime > 1f)
        {
            if (nextAttack)
            {
                stateCharacter.ChangestState(StateName.Attack2, stateLayerIndex);
            }
            else
            {
                stateCharacter.ChangestState(StateName.ReadyAttack, stateLayerIndex);
            }
        }
    }
}
