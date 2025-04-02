using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum DamageArea
{
    None,
    Head,
    Body,
    LeftArm,
    RightArm,
}

public enum DamageSurface
{
    Wood,
    Metal,
    Orginic
}

public interface IDamagable
{
    public Collider MainCollider { get; }
    public GameObject GameObject { get; }
    
    public void TakeDamage(CombatEvent combatEvent);
    
    public DamageArea GetDamageArea(Collider collider);

    public DamageSurface GetDamageSurface(Collider collider);
}
