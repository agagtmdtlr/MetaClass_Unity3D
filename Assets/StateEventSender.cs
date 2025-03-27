using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class StateEventSender : StateMachineBehaviour
{
    [Range(0, 1)] public float StartNormalizedTime;
    private bool isPassStart;

    [Range(0, 1)] public float EndNormalizedTime; 
    private bool isPassEnd;

    private PlayerCharacter playerCharacter;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerCharacter = animator.GetComponent<PlayerCharacter>();
        isPassStart = false;
        isPassEnd = false;
        
        Debug.Log("StateEventSender OnStateEnter");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("StateEventSender OnStateUpdate");
        if (!isPassStart && StartNormalizedTime < stateInfo.normalizedTime)
        {
            playerCharacter.BeginHit();
            isPassStart = true;
        }

        if (!isPassEnd && EndNormalizedTime < stateInfo.normalizedTime)
        {
            playerCharacter.EndHit();
            isPassEnd = true;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("StateEventSender OnStateExit");
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
