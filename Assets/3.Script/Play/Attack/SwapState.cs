using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapState : PlayerState
{
    private static readonly int SWAP = Animator.StringToHash("Swap");

    public PlayerController player;
    [SerializeField] private Animator animator;

    public override string StateType => "Swap";

    public override void Enter()
    {
        animator.ResetTrigger(SWAP);
        animator.SetTrigger(SWAP);
    }

    public override void Update()
    {
    }

    public override void Exit()
    {
    }

    public override void Transition()
    {
    }
}
