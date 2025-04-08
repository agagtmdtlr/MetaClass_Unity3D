using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HitBox : MonoBehaviour
{
    private Collider hitCollider;
    [SerializeField] private DamageArea damageArea; 
    [SerializeField] private DamageSurface damageSurface;
    
    public Collider HitCollider => hitCollider;
    public DamageArea DamageArea => damageArea;
    public DamageSurface DamageSurface => damageSurface;
    
    public void GetHitPositionAndNormal(Vector3 hitPoint, out Vector3 hitPosition, out Vector3 hitNormal)
    {
        hitPosition = hitCollider.ClosestPoint(hitPoint);
        hitNormal = (hitPosition - hitCollider.bounds.center).normalized;
    }
    
    private void Awake()
    {
        hitCollider = GetComponent<Collider>();
        if(hitCollider == null)
            Debug.LogWarning("HitBox: Collider is null");
    }
}

