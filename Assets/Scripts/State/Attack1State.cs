using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Attack1State : AttackState
{
    private static readonly int ATTACK = Animator.StringToHash("Attack-1");
    public override StateName stateName =>  StateName.Attack1;
    bool nextAttack;
    
    public override void EnterState()
    {
        base.EnterState();
        stateAnimator.CrossFade(ATTACK,0.1f);
        nextAttack = false;
    }

    protected override void Update()
    {
        base.Update();
        
        var stateInfo = stateAnimator.GetCurrentAnimatorStateInfo(0);
        float normalizeTime = stateInfo.normalizedTime;
        if (Input.GetMouseButtonDown(0) && 0.4f < normalizeTime && normalizeTime < 0.85f)
        {
            nextAttack = true;
        }

        if (normalizeTime > 1f)
        {
            if (nextAttack)
            {
                stateCharacter.ChangestState(StateName.Attack2);
            }
            else
            {
                stateCharacter.ChangestState(StateName.Move);
            }
        }
    }
}
