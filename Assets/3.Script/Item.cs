using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum ItemType
    {
        Unknown,
        AssaultRifle,
        GrenadeLauncher,
        Shotgun,
    }
    [SerializeField] private ItemType itemType;

    private void OnTriggerEnter(Collider other)
    {
        var collector = ItemSystem.Instance.GetCollectorOrNull(other);
        if (collector is not null)
        {
            ItemEvent evt = new ItemEvent()
            {
                Receiver = collector,
                Item = itemType,
            };
            ItemSystem.Instance.AddWeaponEvent(evt);
            Debug.Log("Item Destroyed");
            Destroy(gameObject);
        }

    }
}
