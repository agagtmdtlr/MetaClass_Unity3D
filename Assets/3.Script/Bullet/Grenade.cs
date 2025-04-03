using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
public class Grenade : MonoBehaviour
{
    [SerializeField] private ParticleSystem explosionEffect;
    public int Damage { get; set; }
    
    [SerializeField] private Rigidbody bulletRigidbody;
    [SerializeField] private Collider  bulletCollider;
    private void Awake()
    {
        bulletCollider.isTrigger = true;
    }

    void Update()
    {
        if (explosionEffect.gameObject.activeInHierarchy && explosionEffect.isPlaying is false)
        {
            Destroy(gameObject);
        }
    }

    public void Launch(float force)
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
            explosionEffect.gameObject.SetActive(true);
            
        }
        
    }
}
