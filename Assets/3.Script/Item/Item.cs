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
        if (Player.localPlayer.MainCollider.Equals(other))
        {
            Player.localPlayer.OnTouchedItem(itemType);
            Debug.Log("Item Destroyed");
            Destroy(gameObject);
        }

    }
}
