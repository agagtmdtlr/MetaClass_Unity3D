using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ReadyAttackState : PlayerState
{
    public override StateName stateName => StateName.ReadyAttack;
    public override int stateLayerIndex => 1;

    public override void EnterState()
    {
        stateAnimator.CrossFade("ReadyAttack", 0.1f,  stateLayerIndex);
        stateAnimator.SetLayerWeight(stateLayerIndex,0f);
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log($"Goto Attack1 {Time.frameCount}");
            stateCharacter.ChangestState(StateName.Attack1, stateLayerIndex);
        }
    }

    public override void ExitState()
    {
    }
}
