using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossState : MonoBehaviour
{
    public enum StateName
    {
        Idle,
        Breath,
        Scratch,
        Dead
    }

    public abstract StateName Name { get; }
    
    public abstract void Intialize(BossMonster bossMonster);
    public abstract void Enter();
    public abstract void Exit();
    
    
}
