using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFactory : MonoBehaviour
{
    public static ItemFactory instance;
    
    [System.Serializable]
    public struct ItemPreset
    {
        public Item.ItemType itemType;
        public GameObject prefab;
    }
    
    public ItemPreset[] itemPresets;

    public Weapon Create(Item.ItemType itemType)
    {
        return null;
    }
        
}
