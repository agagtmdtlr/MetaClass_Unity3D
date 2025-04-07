using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonState : BossState
{
    public override StateName Name => StateName.SummonState;
    public override void Initialize(BossMonster bossMonster)
    {
        InitializeDefault(bossMonster);
    }

    public override void Enter()
    {
        throw new System.NotImplementedException();
    }

    public override void Exit()
    {
        throw new System.NotImplementedException();
    }
}
