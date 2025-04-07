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
    }

}
