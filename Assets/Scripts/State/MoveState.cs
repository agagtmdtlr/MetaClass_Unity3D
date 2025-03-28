using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MoveState : PlayerState
{
    private static readonly int MOVE = Animator.StringToHash("Move");
    private static readonly int ATTACK = Animator.StringToHash("Attack");

    private static readonly int MOVE_Z = Animator.StringToHash("MoveZ");

    private static readonly int MOVE_X = Animator.StringToHash("MoveX");

    private static readonly int SPEED = Animator.StringToHash("Speed");
    public override StateName stateName => StateName.Move;
    
    public override void EnterState()
    {
        stateAnimator.CrossFade(MOVE,0.1f);
    }

    public override void ExitState()
    {
    }

    public float moveSpeed = 5.0f;

    void Update()
    {
        UpdateMovement();
        UpdateAnimation();
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            stateCharacter.ChangestState(StateName.Attack1);
        }
    }

    void UpdateMovement()
    {
        Vector2 axisInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        float inputSpeed = 1f;
        if (Input.GetKey(KeyCode.LeftShift))
            inputSpeed = 2.0f;
        
        axisInput *= inputSpeed;
        stateCharacter.transform.Translate(new Vector3(axisInput.x, 0, axisInput.y) * (Time.deltaTime * moveSpeed) );
    }

    void UpdateAnimation()
    {
        Vector2 axisInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        
        float inputSpeed = 1f;
        Input.GetKey(KeyCode.LeftShift); 
        if (Input.GetKey(KeyCode.LeftShift))
            inputSpeed = 2.0f;

        axisInput *= inputSpeed;
        
        stateAnimator.SetFloat(MOVE_Z, axisInput.y);
        stateAnimator.SetFloat(MOVE_X, axisInput.x);
        stateAnimator.SetFloat(SPEED, inputSpeed);

        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            stateAnimator.SetTrigger(ATTACK);
        }
    }

}
