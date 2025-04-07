using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadState : PlayerState
{
    public override string StateType => "Reload";
    public override void Enter()
    {
        PlayerAnimator.ResetTrigger(PlayerController.RELOAD);
        PlayerAnimator.SetTrigger(PlayerController.RELOAD);
    }

    public override void Update()
    {
    }

    public override void Exit()
    {
    }

}
