using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : MonoBehaviour , IState
{
    private static readonly int FIRE = Animator.StringToHash("Fire");

    public PlayerController player;
    public string StateType => "Attack";
    [SerializeField] private Animator animator;
    
    
    public void Enter()
    {
    }

    public void Update()
    {
        UpdateInput();
        Transition();
    }
    
    private void UpdateInput()
    {
        if (Input.GetMouseButton(0))
        {
            bool successFire = player.currentWeapon.Fire();

            if (successFire)
            {
                // anystate를 통해 transition
                animator.ResetTrigger(FIRE);
                animator.SetTrigger(FIRE);
            }
        }
    }


    public void Exit()
    {
    }

    public void Transition()
    {
        int loc = -1;
        if (Input.GetKeyDown(KeyCode.Alpha1))
            loc = 0;
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            loc = 1;
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            loc = 2;

        if (loc >= 0 && player.CanSwap(loc))
        {
            player.ToAttackState("Swap");
        }
    }
}
