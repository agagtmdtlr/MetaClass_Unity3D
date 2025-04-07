using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossState : MonoBehaviour
{
    public enum StateName
    {
        IdleState,
        ScratchState,
        BreathState,
        DeadState,
        SummonState
    }

    protected Animator Animator { get; private set; }
    protected BossMonster BossMonster { get; private set; }

    
    public abstract StateName Name { get; }

    public string AnimatorStateName;
    public float ExitTime;

    protected void InitializeDefault(BossMonster bossMonster)
    {
        Animator = bossMonster.Animator;
        this.BossMonster = bossMonster;
    }
    
    public abstract void Initialize(BossMonster bossMonster);
    public abstract void Enter();
    public abstract void Exit();
}
