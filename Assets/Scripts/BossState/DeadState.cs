using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : BossState
{
    public override StateName Name => StateName.Dead;
    public override void Intialize(BossMonster bossMonster)
    {
    }

    public override void Enter()
    {
    }

    public override void Exit()
    {
    }
}
