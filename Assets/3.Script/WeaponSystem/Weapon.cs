using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public enum WeaponType
    {
        Unknown,
        AssaultRifle,
        GrenadeLauncher,
        Shotgun
    }
    
    public class Events
    {
        public Action<int> OnChangeAmmo;
    }
    
    public Events events = new Events();
    
    [Header("Gun Data")]
    public GunData data;
    public WeaponType Type { get; }
    
    [Header("Bullet Data")]
    [SerializeField] Bullet bulletSample;
    ObjectPool bulletPool;
    
    [Header("Visual")]
    public GameObject muzzleFlash;
    public float muzzleFlashDuration = 0.1f;
    
    public int CurrentAmmo { get; protected set; }
    public float CurrentFireRate { get; protected set; }
    
    protected float CurrentMuzzleFlashDuration { get; set; }

    public abstract bool Fire();
    public void Reload()
    {
        CurrentAmmo = data.totalAmmo;
        events.OnChangeAmmo?.Invoke(CurrentAmmo);
    }
    
    public bool IsReadyToFire()
    {
        if (CurrentFireRate < data.fireRate)
        {
            //Debug.Log("Still Ready to Fire");
            return false;
        }

        if (CurrentAmmo <= 0)
        {
            //Debug.Log("No Ammo Left");
            return false;
        }
        
        CurrentAmmo--;
        events.OnChangeAmmo?.Invoke(CurrentAmmo);
        
        CurrentFireRate = 0f;
        CurrentMuzzleFlashDuration = 0f;
        return true;
    }
    
    protected virtual void Awake()
    {
        CurrentAmmo = data.totalAmmo;
    }

    protected virtual void Start()
    {
        CombatSystem.Instance.Events.OnDeathEvent += OnDeathEvent;
    }

    protected virtual void OnDestroy()
    {
        CombatSystem.Instance.Events.OnDeathEvent -= OnDeathEvent;
    }

    private void OnDeathEvent(DeathEvent deathEvent)
    {
        CurrentAmmo += 5;
        CurrentAmmo = Mathf.Min(CurrentAmmo, data.totalAmmo);
        events.OnChangeAmmo?.Invoke(CurrentAmmo);
    }

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
