using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossState : MonoBehaviour , IState<BossState.StateName, BossMonster>
{
    public enum StateName
    {
        IdleState,
        ScratchState,
        BreathState,
        SummonState,
        DeadState,
    }

    protected Animator Animator { get; private set; }
    private BossMonster bossMonster;
    public BossMonster Context => bossMonster;
    public abstract StateName StateType { get; }

    public string AnimatorStateName;
    public float ExitTime;

    protected void InitializeDefault(BossMonster bossMonster)
    {
        Animator = bossMonster.Animator;
        this.bossMonster = bossMonster;
    }
    
    public abstract void Initialize(BossMonster bossMonster);
    public abstract void Enter();
    public abstract void Update();

    public abstract void Exit();
}
