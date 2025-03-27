using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : PlayerState
{
    private static readonly int ATTACK = Animator.StringToHash("Attack");
    public override StateName stateName =>  StateName.Attack;

    private void OnEnable()
    {
        animator.SetTrigger(ATTACK);
    }

    private void OnDisable()
    {
        
    }
}
