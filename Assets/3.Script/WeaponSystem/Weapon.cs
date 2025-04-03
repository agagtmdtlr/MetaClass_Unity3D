using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Header("Gun Data")]
    public GunData data;
    
    [SerializeField] Bullet bulletSample;
    ObjectPool bulletPool;
    
    public int CurrentAmmo { get; protected set; }
    public float CurrentFireRate { get; protected set; }

    public abstract bool Fire();


    private bool isPoolInitialized = false;
    public Bullet GetBullet()
    {
        if (isPoolInitialized is false)
        {
            bulletPool = new ObjectPool();
            bulletPool.Initialize(
                bulletSample.GameObject.transform,
                bulletSample,
                "Bullet",
                4 
                );
        }
        
        var bullet = bulletPool.Get() as Bullet;
        bullet.weapon = this; 

        return bulletPool.Get() as Bullet;
    }
    
    public void ReturnToWeapon(Bullet bullet)
    {
        bulletPool.Return(bullet);
    }
}
