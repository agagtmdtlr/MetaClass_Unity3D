using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameEvent
{
    public enum EventType
    {
        Unknown,
        Combat,
        TakeDamage,
        Death
    }
    public abstract EventType Type { get; }
}

public class CombatEvent : GameEvent
{
    public IAttackable Sender { get; set; }
    public IDamagable Receiver { get; set; }
    
    public Vector3 HitPosition { get; set; }
    public Vector3 HitNormal { get; set; }
    public HitBox HitBox { get; set; }
    public override EventType Type => EventType.Combat;
}

public class TakeDamageEvent : GameEvent
{
    public IDamagable Taker { get; set; }
    public int Damage { get; set; }
    public Vector3 HitPosition { get; set; }
    public Vector3 HitNormal { get; set; }
    public HitBox HitBox { get; set; }
    public override EventType Type => EventType.TakeDamage;
}

public class DeathEvent : GameEvent
{
    public IDamagable Dead { get; set; }
    public Vector3 DeathPosition { get; set; }
    public override EventType Type => EventType.Death;
}


