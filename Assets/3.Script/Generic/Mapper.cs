

using System;
using Unity.VisualScripting;

public static class Mapper
{
    public static Weapon.WeaponType MapWeapon(Item.ItemType type)
    {
        switch (type)
        {
            case Item.ItemType.AssaultRifle:
                return Weapon.WeaponType.AssaultRifle;
            case Item.ItemType.GrenadeLauncher:
                return Weapon.WeaponType.GrenadeLauncher;
            case Item.ItemType.Shotgun:
                return Weapon.WeaponType.Shotgun;
            default:
                return Weapon.WeaponType.Unknown;
        }
    }

    public static Type MapType(Item.ItemType type)
    {
        switch (type)
        {
            case Item.ItemType.AssaultRifle:
                return typeof(AssultRifle);
            case Item.ItemType.GrenadeLauncher:
                return typeof(GrenadeLauncher);
            case Item.ItemType.Shotgun:
                return typeof(ShotGun);
            default:
                return typeof(Unknown);
        }
    }

}