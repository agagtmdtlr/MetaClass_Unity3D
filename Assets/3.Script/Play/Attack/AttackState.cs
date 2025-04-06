using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : PlayerState
{
    private static readonly int FIRE = Animator.StringToHash("Fire");

    public override string StateType => "Attack";


    public override void Enter()
    {
    }

    public override void Update()
    {
        UpdateInput();
        Transition();
    }

    private void UpdateInput()
    {
        if (playerController.currentWeapon is null)
            return;

        if (Input.GetMouseButton(0))
        {
            //Debug.Log("Trigger Fire");
            bool successFire = playerController.currentWeapon.Fire();
            if (successFire)
            {
                //Debug.Log("Success Fire");
                playerAnimator.ResetTrigger(FIRE);
                playerAnimator.SetTrigger(FIRE);
            }
        }
    }


    public override void Exit()
    {
    }

    public override void Transition()
    {
        if (playerController.currentWeapon is null)
            return;

        if (Input.GetKeyDown(KeyCode.R))
        {
            playerController.ToAttackState("Reload");
        }
    }
}

