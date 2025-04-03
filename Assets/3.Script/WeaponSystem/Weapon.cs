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
        if (bulletSample is null)
        {
            Debug.LogWarning("Bullet Sample is null");
        }
        
        if (isPoolInitialized is false)
        {
            bulletPool = new ObjectPool();
            bulletPool.Initialize(
                null,
                bulletSample,
                "Bullet",
                4 
                );

            isPoolInitialized = true;
        }
        
        var bullet = bulletPool.Get() as Bullet;
        bullet.gameObject.SetActive(true);
        bullet.weapon = this; 

        return bullet;
    }
    
    public void ReturnToWeapon(Bullet bullet)
    {
        bulletPool.Return(bullet);
    }
}
