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
    
    private void Awake()
    {
        hitCollider = GetComponent<Collider>();
        if(hitCollider == null)
            Debug.LogWarning("HitBox: Collider is null");
    }
}

