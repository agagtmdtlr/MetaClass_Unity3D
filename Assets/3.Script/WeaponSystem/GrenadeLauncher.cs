using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GrenadeLauncher : Weapon
{
    [Header("Grenade Launcher")]
    public Transform grenadeEjectTransform;
    public float force = 10f;

    public override WeaponType Type  => WeaponType.GrenadeLauncher;

    protected override void Start()
    {
        base.Start();
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
        if( IsReadyToFire() == false)
            return false;
        
        muzzleFlash.transform.localRotation 
            *= Quaternion.AngleAxis(Random.Range(0,360), Vector3.right);
        
        muzzleFlash.SetActive(true);

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
