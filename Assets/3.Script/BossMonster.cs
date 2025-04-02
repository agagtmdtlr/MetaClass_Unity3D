using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class BossMonster : MonoBehaviour , IDamagable
{
    public Collider MainCollider => spineCollider;
    public GameObject GameObject => gameObject;
    
    public Collider spineCollider;

    public Collider headCollider;
    public Collider[] leftArmColliders; 
    public Collider[] rightArmColliders; 
    
    private void Start()
    {
        CombatSystem.Instance.RegisterMonster(spineCollider, this);
        CombatSystem.Instance.RegisterMonster(headCollider, this);
        foreach (Collider collider in leftArmColliders)
        {
            CombatSystem.Instance.RegisterMonster(collider, this);
        }

        foreach (Collider collider in rightArmColliders)
        {
            CombatSystem.Instance.RegisterMonster(collider, this);
        }
    }

    public void TakeDamage(CombatEvent combatEvent)
    {
        var hitCollider = combatEvent.Collider;
        var monster = CombatSystem.Instance.GetMonsterOrNull(hitCollider);
        if (monster == this)
        {
        }
    }

    public DamageArea GetDamageArea(Collider collider)
    {
        if (headCollider == collider)
            return DamageArea.Head;

        if (spineCollider == collider)
            return DamageArea.Body;

        foreach (var armCollider in leftArmColliders)
        {
            if (armCollider == collider)
                return DamageArea.LeftArm;
        }

        foreach (var armCollider in rightArmColliders)
        {
            if (armCollider == collider)
                return DamageArea.RightArm;
        }

        return DamageArea.None;
    }
}
