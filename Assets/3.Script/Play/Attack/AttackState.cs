using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : PlayerState
{
    public override string StateType => "Attack";


    public override void Enter()
    {
    }

    public override void Update()
    {
        UpdateInput();
    }

    private void UpdateInput()
    {
        var weapon = Player.localPlayer.currentWeapon;
        if (weapon is null)
            return;

        if (Input.GetMouseButton(0))
        {
            // TODO: weapon 이벤트 처리 로직은 LocalPlayer에서 처리하는게 좋을듯
            bool successFire = weapon.Fire();
            if (successFire)
            {
                PlayerAnimator.ResetTrigger(PlayerController.FIRE);
                PlayerAnimator.SetTrigger(PlayerController.FIRE);
            }
        }
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            Context.ToAttackState("Reload");
        }
    }


    public override void Exit()
    {
    }
}

