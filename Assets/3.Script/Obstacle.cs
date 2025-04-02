using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Obstacle : MonoBehaviour, IDamagable
{
    Collider obstacleCollider;
    public DamageSurface surfaceType;
    
    // Start is called before the first frame update
    void Start()
    {
        obstacleCollider = GetComponent<Collider>();
        CombatSystem.Instance.RegisterMonster(obstacleCollider, this);
    }

    public Collider MainCollider => obstacleCollider;
    public GameObject GameObject => gameObject;
    public void TakeDamage(CombatEvent combatEvent)
    {
    }

    public DamageArea GetDamageArea(Collider collider)
    {
        return DamageArea.None;
    }

    public DamageSurface GetDamageSurface(Collider collider)
    {
        return surfaceType;
    }
}
