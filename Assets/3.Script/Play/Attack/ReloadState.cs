using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadState : PlayerState
{
    private static readonly int RELOAD = Animator.StringToHash("Reload");
    public override string StateType => "Reload";
    public override void Enter()
    {
        playerAnimator.ResetTrigger(RELOAD);
        playerAnimator.SetTrigger(RELOAD);
    }

    public override void Update()
    {
        Transition();
    }

    public override void Exit()
    {
    }

    public override void Transition()
    {
        var info = playerAnimator.GetCurrentAnimatorStateInfo(0);
        if (info.normalizedTime >= 0.77f)
        {
            // to local player
            //playerController.currentWeapon.Reload();
            playerController.ToAttackState("Attack");
        }
    }

}
