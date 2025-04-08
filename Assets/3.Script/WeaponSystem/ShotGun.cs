using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class ShotGun : Weapon
{
    [FormerlySerializedAs("muzzleTransform")] [Header("Shot Settings")]
    public Transform hitPoint;
    public int MultipleBullets = 30;

    [Range(0f, 0.5f)] 
    public float HitRange; 
    
    public override WeaponType Type  => WeaponType.Shotgun;
    
    void Update()
    {
        CurrentFireRate += Time.deltaTime;
        CurrentMuzzleFlashDuration += Time.deltaTime;
    }
    
    public override bool Fire()
    {
        if( IsReadyToFire() == false)
            return false;
        
        for (int i = 0; i < MultipleBullets; i++)
        {
            var randInCircle = UnityEngine.Random.insideUnitCircle;
            randInCircle *= HitRange;
            
            var dir = hitPoint.forward;
            dir += randInCircle.x * hitPoint.right;
            dir += randInCircle.y * hitPoint.up;
            Ray ray = new Ray(hitPoint.position, dir.normalized);
            
            if (Physics.Raycast(ray, out RaycastHit hit, data.range, LayerMask.GetMask("Enemy")))
            {
                HitBox hitBox = hit.collider.GetComponent<HitBox>();
                if (hitBox is null)
                {
                    return false;
                }
            
                IDamagable enemy = CombatSystem.Instance.GetMonsterOrNull(hitBox);
                if (enemy != null)
                {
                    CombatEvent combatEvent = new CombatEvent
                    {
                        Sender = this,
                        Receiver = enemy,
                        HitPosition = hit.point,
                        HitNormal = hit.normal,
                        HitBox = hitBox
                    };
                    CombatSystem.Instance.AddGameEvent(combatEvent);
                }
            }
        }

        
        var bullet 
            = ObjectPoolManager.Instance.GetObjectOrNull("ShotGunBullet") as ParticleEffect;
        var direction = hitPoint.forward;
        bullet.transform.position = hitPoint.position;
        bullet.transform.forward = direction;
        
        var flare 
            = ObjectPoolManager.Instance.GetObjectOrNull("ShotGunFlare") as ParticleEffect;
        flare.transform.position = hitPoint.position;
        
        
        return true;
    }
}
