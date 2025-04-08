using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
public abstract class Bullet : MonoBehaviour , IObjectPoolItem , IAttackable
{
    public Weapon weapon;
    
    public int Damage { get; set; }
    [SerializeField] protected Rigidbody bulletRigidbody;
    [SerializeField] protected Collider  bulletCollider;
    public string Key { get; set; }
    public GameObject GameObject => gameObject;
    public void ReturnToPool()
    {
        weapon.ReturnToWeapon(this);
    }


    public abstract void Launch(float force);
}
