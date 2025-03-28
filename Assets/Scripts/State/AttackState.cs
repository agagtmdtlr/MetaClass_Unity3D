using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackState : PlayerState
{
    public override int stateLayerIndex => 1;
    
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
        stateAnimator.SetLayerWeight(stateLayerIndex,1f);
    }

    public override void ExitState()
    {
        stateCharacter.EndHit();
        stateAnimator.SetLayerWeight(stateLayerIndex,0f);
    }

}
