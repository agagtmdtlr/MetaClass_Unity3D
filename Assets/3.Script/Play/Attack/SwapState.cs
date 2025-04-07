using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapState : PlayerState
{

    public PlayerController player;
    [SerializeField] private Animator animator;

    public override string StateType => "Swap";

    public override void Enter()
    {
        animator.ResetTrigger(  PlayerController.SWAP);
        animator.SetTrigger(PlayerController.SWAP);
    }

    public override void Update()
    {
    }

    public override void Exit()
    {
    }
}
