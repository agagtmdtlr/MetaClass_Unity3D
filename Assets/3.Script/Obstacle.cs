using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Obstacle : MonoBehaviour, IDamagable
{
    HitBox hitBox;
    public DamageSurface surfaceType;
    
    // Start is called before the first frame update
    void Start()
    {
        hitBox = GetComponent<HitBox>();
        CombatSystem.Instance.RegisterMonster(hitBox, this);
    }

    public GameObject GameObject => gameObject;
    public void TakeDamage(CombatEvent combatEvent)
    {
    }

    public DamageArea GetDamageArea(Collider collider)
    {
        return DamageArea.Unknown;
    }

    public DamageSurface GetDamageSurface(Collider collider)
    {
        return surfaceType;
    }
}
