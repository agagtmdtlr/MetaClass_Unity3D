using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFactory : Globalable<WeaponFactory>
{
    [System.Serializable]
    public struct ItemPreset
    {
        public Weapon.WeaponType itemType;
        public Weapon prefab;
    }
    public ItemPreset[] itemPresets;
    Dictionary<Weapon.WeaponType,Weapon> itemDict = new Dictionary<Weapon.WeaponType, Weapon>();

    private void Start()
    {
        foreach (var item in itemPresets)
        {
            if (itemDict.ContainsKey(item.itemType))
            {
                Debug.LogWarning(
                    $"Item {item.itemType} has already been added" +
                    $"{itemDict[item.itemType].gameObject.name}");
            }
            itemDict.Add(item.itemType, item.prefab);
        }
    }

    public Weapon Create(Weapon.WeaponType itemType)
    {
        var sample = itemDict[itemType];
        var weapon = Instantiate(sample,  Vector3.zero, sample.transform.rotation);
        weapon.transform.localScale = sample.transform.localScale;
        return weapon;
    }
        
}
