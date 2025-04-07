using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : BossState
{
    public override StateName StateType => StateName.DeadState;
    
    public override void Initialize(BossMonster bossMonster)
    {
        InitializeDefault(bossMonster);
    }

    public override void Enter()
    {
    }

    public override void Update()
    {
        
    }

    public override void Exit()
    {
        
    }
}
