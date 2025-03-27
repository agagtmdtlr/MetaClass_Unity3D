using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehaviour : StateMachineBehaviour
{
    public string Name;
    
    private static readonly int ATTACK = Animator.StringToHash("Attack");
    private static readonly int ATTACK1 = Animator.StringToHash("Attack-1");
    private static readonly int ATTACK2 = Animator.StringToHash("Attack-2");
    private static readonly int ATTACK3 = Animator.StringToHash("Attack-3");

    public delegate void AttackBehaviourEvent();
    public event AttackBehaviourEvent beginHitEvent;
    public event AttackBehaviourEvent endHitEvent;

    [Range(0,1), SerializeField] public float enableHitBoxTime;
    bool isTriggeredEnableHitBox = false;
    
    public bool isComboLastAttack = false;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        isTriggeredEnableHitBox = false;
        Debug.Log($"Enter {Name}");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
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

        if (isTriggeredEnableHitBox == false && enableHitBoxTime <= stateInfo.normalizedTime)
        {
            beginHitEvent?.Invoke();
            isTriggeredEnableHitBox = true;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log($"Exit {Name}");
        /*var nextStateInfo = animator.GetNextAnimatorStateInfo(layerIndex);
        bool toAttackState = IsAttackState(nextStateInfo.shortNameHash);
        if(toAttackState)
            endHitEvent?.Invoke();*/
        
        endHitEvent?.Invoke();
    }

    bool IsAttackState(int id)
    {
        return id == ATTACK1 || id == ATTACK2 || id == ATTACK3;
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
