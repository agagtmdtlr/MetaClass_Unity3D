using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
public class Grenade : Bullet
{
    private void Awake()
    {
        bulletCollider.isTrigger = true;
    }

    public override void Launch(float force)
    {
        bulletRigidbody.AddForce(-transform.up * force, ForceMode.Impulse);
    }
    
    private void OnTriggerEnter(Collider other)
    { 
        HitBox hitbox = other.GetComponent<HitBox>();
        if (hitbox is not null)
        {
            var monster= CombatSystem.Instance.GetMonsterOrNull(hitbox);

            var hitPoint = hitbox.HitCollider.ClosestPoint(transform.position);
            var hitNormal = (hitPoint - hitbox.HitCollider.bounds.center).normalized;
            CombatEvent combatEvent = new CombatEvent()
            {
                HitBox =  hitbox,
                Damage = Damage,
                Sender = Player.localPlayer,
                Receiver = monster,
                HitPosition = transform.position,
                HitNormal = hitNormal
            };
            CombatSystem.Instance.AddCombatEvent(combatEvent);
            
            var explosion = ObjectPoolManager.Instance.GetObjectOrNull("Explosion") as ParticleEffect;
            explosion.transform.position = transform.position;
            ReturnToPool();
        } 
        
    }
}
