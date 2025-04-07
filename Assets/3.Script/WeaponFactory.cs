using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFactory : MonoBehaviour
{
    public static WeaponFactory instance;
    
    [System.Serializable]
    public struct ItemPreset
    {
        public Item.ItemType itemType;
        public Weapon prefab;
    }
    public ItemPreset[] itemPresets;
    Dictionary<Item.ItemType,Weapon> itemDict = new Dictionary<Item.ItemType, Weapon>();

    private void Awake()
    {
        instance = this;
    }

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

    public Weapon Create(Item.ItemType itemType)
    {
        var sample = itemDict[itemType];
        var weapon = Instantiate(sample,  Vector3.zero, sample.transform.rotation);
        weapon.transform.localScale = sample.transform.localScale;
        return weapon;
    }
        
}
