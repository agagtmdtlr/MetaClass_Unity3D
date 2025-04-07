using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class BaseStat
{
    public const int HIT_COUNT = 20;
        
    [HideInInspector]
    public int CurrentHp { get; set; }

    [SerializeField] private int maxHp = 100;
    public int MaxHp => maxHp;
    
        
    public int CurrentHitCount { get; set; }
}
