using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalPlayer : Player, IDamagable
{
    private void Awake()
    {
        Player.localPlayer = this;
    }

    public Collider MainCollider { get; }
    public GameObject GameObject { get; }
    public void TakeDamage(CombatEvent combatEvent)
    {
    }

    public DamageArea GetDamageArea(Collider collider)
    {
        return DamageArea.None;
    }

    public DamageSurface GetDamageSurface(Collider collider)
    {
        return DamageSurface.Orginic;
    }
}
