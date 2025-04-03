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
}

public enum DamageSurface
{
    Wood,
    Metal,
    Skin
}

public interface IDamagable
{
    public GameObject GameObject { get; }
    
    public void TakeDamage(CombatEvent combatEvent);
   
}
