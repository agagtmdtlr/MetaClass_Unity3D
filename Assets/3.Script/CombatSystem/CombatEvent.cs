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

