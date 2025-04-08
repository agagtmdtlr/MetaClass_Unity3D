using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
public class Grenade : Bullet
{
    [SerializeField] private float explosionRange;
    private float duration;
    private void Awake()
    {
        bulletCollider.isTrigger = true;
    }

    public override void Launch(float force)
    {
        bulletRigidbody.AddForce(-transform.up * force, ForceMode.Impulse);
        duration = 5f;
    }
    

    public void Update()
    {
        if (duration < 0f)
        {
            ReturnToPool();
        }
        duration -= Time.deltaTime;
    }

    private void Explosion()
    {
        var colliders= Physics.OverlapSphere(transform.position, explosionRange, LayerMask.GetMask("Enemy"));
        foreach (var coll in colliders)
        {
            HitBox hitbox = coll.GetComponent<HitBox>();
            if (hitbox is not null)
            {
                hitbox.GetHitPositionAndNormal(transform.position, out Vector3 hitPoint, out Vector3 hitNormal);
                var monster= CombatSystem.Instance.GetMonsterOrNull(hitbox);
                
                CombatEvent combatEvent = new CombatEvent()
                {
                    HitBox =  hitbox,
                    Sender = this,
                    Receiver = monster,
                    HitPosition = hitPoint,
                    HitNormal = hitNormal
                };
                CombatSystem.Instance.AddCombatEvent(combatEvent);
            }
        }
        var explosion = ObjectPoolManager.Instance.GetObjectOrNull("Explosion") as ParticleEffect;
        explosion.transform.position = transform.position;
        ReturnToPool();
    }

    private void OnTriggerEnter(Collider other)
    { 
        Explosion();
    }
}
