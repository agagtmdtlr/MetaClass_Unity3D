using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackState : PlayerState
{
    [Range(0,1)]
    public float enableHitBoxTime = 0;
    bool enableHitBox = false;
    [Range(0,1)]
    public float disableHitBoxTime = 1f;
    bool disableHitBox = false;
    
    protected virtual void Update()
    {
        var stateInfo = stateAnimator.GetCurrentAnimatorStateInfo(0);

        if (!enableHitBox && stateInfo.normalizedTime >= enableHitBoxTime)
        {
            stateCharacter.BeginHit();
            enableHitBox = true;
        }

        if (!disableHitBox && stateInfo.normalizedTime >= disableHitBoxTime)
        {
            stateCharacter.EndHit();
            disableHitBox = true;
        }
    }

    public override void EnterState()
    {
        enableHitBox = false;
        disableHitBox = false;
    }

    public override void ExitState()
    {
        stateCharacter.EndHit();
    }

}
