using System;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] Weapon assaultRifle;
    [SerializeField] Weapon grenadeLauncher;
    [SerializeField] Weapon shotgun;

    public Weapon GetWeapon(Weapon.WeaponType weaponType)
    {
        switch (weaponType)
        {
            case Weapon.WeaponType.AssaultRifle:
                return assaultRifle;
            case Weapon.WeaponType.GrenadeLauncher:
                return grenadeLauncher;
            case Weapon.WeaponType.Shotgun:
                return shotgun;
            default:
                Debug.LogWarning($"invalid weapon type: {weaponType}");
                return null;
        }
    }
        
}