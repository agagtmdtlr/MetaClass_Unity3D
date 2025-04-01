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

    private void Start()
    {
        CombatSystem.Instance.RegisterMonster(this);
    }

    public void TakeDamage(CombatSystem combatEvent)
    {
    }

}
