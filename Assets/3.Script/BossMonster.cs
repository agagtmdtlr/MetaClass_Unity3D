using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class BossMonster : MonoBehaviour , IDamagable
{
    public GameObject GameObject => gameObject;
    
    private readonly Dictionary<Collider,DamageArea> damageAreas = new Dictionary<Collider, DamageArea>();
    
    private void Start()
    {
        var hitBoxs = GetComponentsInChildren<HitBox>(true);
        foreach (var hitbox in hitBoxs)
        {
            damageAreas[hitbox.hitCollider] = hitbox.damageArea;
            CombatSystem.Instance.RegisterMonster(hitbox.hitCollider, this);
        }
    }

    public void TakeDamage(CombatEvent combatEvent)
    {
        var hitCollider = combatEvent.Collider;
        var monster = CombatSystem.Instance.GetMonsterOrNull(hitCollider);
    }

    public DamageArea GetDamageArea(Collider collider)
    {
        return damageAreas[collider];
    }

    public DamageSurface GetDamageSurface(Collider collider)
    {
        return DamageSurface.Orginic;
    }
    
    
    
}
