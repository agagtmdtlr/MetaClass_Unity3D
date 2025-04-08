using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum DamageArea
{
    Unknown,
    Head,
    Body,
    LeftArm,
    RightArm,
    LeftLeg,
    RightLeg
}

public enum DamageSurface
{
    Wood,
    Metal,
    Skin
}

public interface IDamagable
{
    public Collider MainCollider { get; }
    public GameObject GameObject { get; }
    
    public Type DamageableType { get; }
    
    public void TakeDamage(CombatEvent combatEvent);
   
}
