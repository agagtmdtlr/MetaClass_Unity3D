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

    public GameObject GameObject => gameObject;
    public void TakeDamage(CombatEvent combatEvent)
    {
    }

    public DamageArea GetDamageArea(Collider collider)
    {
        return DamageArea.Unknown;
    }

    public DamageSurface GetDamageSurface(Collider collider)
    {
        return DamageSurface.Skin;
    }
}
