using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class FootIK : MonoBehaviour
{
    private Animator animator;
    [Range(0,1)]
    public float IKWeight = 0.5f;
    public Transform LeftKnee;
    public Transform RightKnee;

    public bool RayHitBefore;
    public bool RayHitAfter;
    
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnAnimatorIK(int layerIndex)
    {
        HandleFootIK(AvatarIKGoal.LeftFoot, AvatarIKHint.LeftKnee, LeftKnee);
        HandleFootIK(AvatarIKGoal.RightFoot,AvatarIKHint.RightKnee, RightKnee);
        
    }

    private void HandleFootIK(AvatarIKGoal goal, AvatarIKHint hint, Transform knee)
    {
        var footIkGoalPosition = animator.GetIKPosition(goal);
        var kneeIkHintPosition = animator.GetIKHintPosition(hint);
        Ray ray = new Ray(footIkGoalPosition + Vector3.up * 10f, Vector3.down );

        if(RayHitBefore)
            Debug.DrawLine(ray.origin, ray.origin + Vector3.down * 20f, Color.blue);

        
        if (Physics.Raycast(ray, out RaycastHit hit, 20.0f, LayerMask.GetMask("Ground")))
        {
            Vector3 point = footIkGoalPosition;
            point.y = hit.point.y;
            
            
            if(RayHitAfter)
                Debug.DrawLine(ray.origin, hit.point, Color.red);


            /*if (knee != null)
            {
                animator.SetIKHintPosition(hint, knee.position);
                animator.SetIKHintPositionWeight(hint, IKWeight);
            }*/

            Vector3 kneePoint = kneeIkHintPosition;            
            float offsetY = (hit.point.y - footIkGoalPosition.y); 
            kneePoint.y += offsetY;
            kneePoint.x += (footIkGoalPosition.x - kneeIkHintPosition.x);
            animator.SetIKHintPosition(hint,kneePoint);
            animator.SetIKHintPositionWeight(hint,IKWeight);
            
            animator.SetIKPosition(goal, point);
            animator.SetIKPositionWeight(goal,IKWeight);

            animator.SetIKRotation(goal, Quaternion.LookRotation(transform.forward, hit.normal));
            animator.SetIKRotationWeight(goal,IKWeight);
        }
        else
        {
            animator.SetIKHintPositionWeight(hint,0f);
            animator.SetIKPositionWeight(goal,0f);
            animator.SetIKRotationWeight(goal,0f);

        }
        
    }
    
}
