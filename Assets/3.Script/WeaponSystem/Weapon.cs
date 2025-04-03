using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Header("Gun Data")]
    public GunData data;
    
    public int CurrentAmmo { get; protected set; }
    public float CurrentFireRate { get; protected set; }

    public abstract bool Fire();
}
