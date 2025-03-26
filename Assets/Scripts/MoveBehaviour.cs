using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBehaviour : StateMachineBehaviour
{
    public Transform target;
    public float Speed = 5.0f;

    private static readonly int ATTACK = Animator.StringToHash("Attack");

    private static readonly int MOVE_Z = Animator.StringToHash("MoveZ");

    private static readonly int MOVE_X = Animator.StringToHash("MoveX");

    private static readonly int SPEED = Animator.StringToHash("Speed");
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector2 axisInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        
        float InputSpeed = 1f;
        Input.GetKey(KeyCode.LeftShift); 
        if (Input.GetKey(KeyCode.LeftShift))
            InputSpeed = 2.0f;

        axisInput *= InputSpeed;
        
        animator.SetFloat(MOVE_Z, axisInput.y);
        animator.SetFloat(MOVE_X, axisInput.x);
        animator.SetFloat(SPEED, InputSpeed);

        target.Translate(new Vector3(axisInput.x, 0, axisInput.y) * (Time.deltaTime * Speed) );
        
        if (animator.IsInTransition(0)) return;

        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            animator.SetTrigger(ATTACK);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
