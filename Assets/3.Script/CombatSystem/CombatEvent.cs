using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatEvent
{
    public IDamagable Sender { get; set; }
    public IDamagable Receiver { get; set; }
    
    public int Damage { get; set; }
    public Vector3 HitPosition { get; set; }
    public Vector3 HitNormal { get; set; }
    public HitBox HitBox { get; set; }
}

public class TakeDamageEvent
{
    public IDamagable Taker { get; set; }
    public int Damage { get; set; }
    public Vector3 HitPosition { get; set; }
    public Vector3 HitNormal { get; set; }
    public HitBox HitBox { get; set; }
}

public class DeathEvent
{
    public IDamagable Dead { get; set; }
    public Vector3 DeathPosition { get; set; }
}


