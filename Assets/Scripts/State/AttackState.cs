using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackState : PlayerState
{
    private static readonly int ATTACK = Animator.StringToHash("Attack");
    
    public override StateName stateName =>  StateName.Attack;

    public override void Initialize(PlayerCharacter character)
    {
        base.Initialize(character);

        var attacks = animator.GetBehaviours<AttackBehaviour>();
        foreach (var attack in attacks)
        {
            attack.beginHitEvent += EnableHitbox;
            attack.endHitEvent += DisableHitbox;
        }
    }

    private void OnEnable()
    {
        animator.SetTrigger(ATTACK);
    }
    
    private void OnDisable()
    {
    }

    private void EnableHitbox()
    {
        character.CurrentWeapon.hitCollider.enabled = true;
    }

    private void DisableHitbox()
    {
        character.CurrentWeapon.hitCollider.enabled = false;
    }

    private void ComboAttack()
    {
        animator.SetTrigger(ATTACK);
    }
}
