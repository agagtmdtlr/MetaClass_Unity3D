using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapState : MonoBehaviour, IState
{
    private static readonly int SWAP = Animator.StringToHash("Swap");

    public PlayerController player;
    [SerializeField] private Animator animator;

    private float duration;
    public string StateType => "Swap";

    public void Enter()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            player.SwapWeapon(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            player.SwapWeapon(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            player.SwapWeapon(2);
        }

        duration = 1f;
    }

    public void Update()
    {
        duration -= Time.deltaTime;
        Transition();
    }

    public void Exit()
    {
    }

    public void Transition()
    {
        if(duration < 0)
            player.ToAttackState("Attack");
    }
}
