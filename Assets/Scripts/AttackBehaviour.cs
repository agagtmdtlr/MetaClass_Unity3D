using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehaviour : StateMachineBehaviour
{
    private static readonly int ATTACK = Animator.StringToHash("Attack");
    private static readonly int ATTACK1 = Animator.StringToHash("Attack-1");
    private static readonly int ATTACK2 = Animator.StringToHash("Attack-2");
    private static readonly int ATTACK3 = Animator.StringToHash("Attack-3");

    public bool isComboLastAttack = false;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        PlayerCharacter.currentPlayerCharacter.BeginHit();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.IsInTransition(layerIndex)) return;
        bool inputAttack = Input.GetMouseButtonDown(0);
        if (!isComboLastAttack && inputAttack)
        {
            float normalizeTime = stateInfo.normalizedTime;
            if (0.4f < normalizeTime && normalizeTime < 0.85f)
            {
                animator.SetTrigger(ATTACK);
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        PlayerCharacter.currentPlayerCharacter.EndHit();
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
