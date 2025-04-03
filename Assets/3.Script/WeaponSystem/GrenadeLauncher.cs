using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GrenadeLauncher : Weapon
{
    [Header("Grenade Launcher")]
    public Transform grenadeEjectTransform;
    public float force = 10f;
    [Header("Visual")]
    public GameObject muzzleFlash;
    public float muzzleFlashDuration = 0.1f;
    
    private float CurrentMuzzleFlashDuration { get; set; }
    
    void Start()
    {
        muzzleFlash.SetActive(false);
    }
    
    void Update()
    {
        CurrentFireRate += Time.deltaTime;
        CurrentMuzzleFlashDuration += Time.deltaTime;

        if (CurrentMuzzleFlashDuration >= muzzleFlashDuration)
        {
            muzzleFlash.SetActive(false);
        }
    }
    
    public override bool Fire()
    {
        if(CurrentFireRate < data.fireRate)
            return false;

        /*if (CurrentAmmo <= 0)
            return false;*/

        CurrentFireRate = 0f;

        CurrentAmmo--;
        
        muzzleFlash.transform.localRotation 
            *= Quaternion.AngleAxis(Random.Range(0,360), Vector3.right);
        
        muzzleFlash.SetActive(true);
        CurrentMuzzleFlashDuration = 0f;

        var grenade = GetBullet() as Grenade;
        if (grenade is null)
        {
            Debug.LogWarning("Grenade Launcher is missing a Grenade");
        }
        grenade.GameObject.transform.position = grenadeEjectTransform.position;
        grenade.GameObject.transform.rotation = grenadeEjectTransform.rotation;
        grenade.Damage = data.damage;
        grenade.Launch(force);
        
        return true;
    }

}
