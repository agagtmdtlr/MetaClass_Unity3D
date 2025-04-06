using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapState : PlayerState
{
    private static readonly int SWAP = Animator.StringToHash("Swap");

    public PlayerController player;
    [SerializeField] private Animator animator;

    private float duration;
    public override string StateType => "Swap";

    public override void Enter()
    {
        animator.ResetTrigger(SWAP);
        animator.SetTrigger(SWAP);
        duration = 1f;
    }

    public override void Update()
    {
        duration -= Time.deltaTime;
        Transition();
    }

    public override void Exit()
    {
    }

    public override void Transition()
    {
        if(duration < 0)
            player.ToAttackState("Attack");
    }
}
