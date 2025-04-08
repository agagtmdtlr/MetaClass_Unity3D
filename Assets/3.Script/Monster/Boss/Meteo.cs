using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteo : MonoBehaviour , IAttackable
{
    public int Damage => 1;
    
    public Vector3 direction;
    public Vector3 destination;
    
    public float speed = 10f;
    
    public void SetProperty(Vector3 direction, Vector3 destination)
    {
        this.direction = direction;
        this.destination = destination;
        
        transform.forward = direction;
    }

    private void Update()
    {
        if (Vector3.Dot(destination - transform.position , direction) < 0f)
        {
            Explosion();
        }
        
        transform.position += direction * (speed * Time.deltaTime);
    }

    void Explosion()
    {
        var effect = ObjectPoolManager.Instance.GetObjectOrNull("Explosion").GameObject.GetComponent<ParticleEffect>();
        effect.transform.position = transform.position;
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Player.localPlayer.MainCollider.Equals(other))
        {
            HitBox hitBox = other.GetComponent<HitBox>();
            hitBox.GetHitPositionAndNormal(transform.position, out Vector3 hitPoint, out Vector3 hitNormal);
            
            CombatEvent combatEvent = new CombatEvent()
            {
                Sender = this,
                Receiver = Player.localPlayer,
                HitBox = hitBox,
                HitPosition = hitPoint,
                HitNormal = hitNormal
            };
            CombatSystem.Instance.AddGameEvent(combatEvent);
            Explosion();
        }
    }
}
