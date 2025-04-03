using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeLauncher : Weapon
{
    [Header("Grenade Launcher")]
    public Transform grenadeEjectTransform;
    public  Grenade grenadePrefab;
    
    [Header("Visual")]
    public GameObject muzzleFlash;
    public float muzzleFlashDuration = 0.1f;
    
    private float CurrentMuzzleFlashDuration { get; set; }
    
    void Start()
    {
        muzzleFlash.SetActive(true);
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
        
        var grenade = Instantiate(grenadePrefab, grenadeEjectTransform.position, grenadeEjectTransform.rotation);
        grenade.Damage = data.damage;
        grenade.Launch(20f);
        
        return true;
    }

}
