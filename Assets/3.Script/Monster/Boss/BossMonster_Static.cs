using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public partial class BossMonster
{
    private static BossMonster _currentSceneBossMonster;

    public static BossMonster CurrentSceneBossMonster
    {
        get
        {
            if (_currentSceneBossMonster == null)
            {
                _currentSceneBossMonster = FindObjectOfType<BossMonster>(true);
            }
            return _currentSceneBossMonster;
        }
        set 
        {
            _currentSceneBossMonster = value;
        }
    }

    public static readonly int SCRATCH = Animator.StringToHash("Scratch");
    public static readonly int BREATH = Animator.StringToHash("Breath");
    public static readonly int DEAD = Animator.StringToHash("Dead");
    public static readonly int HIT = Animator.StringToHash("Hit");
    public static readonly int SUMMON = Animator.StringToHash("Summon");
    
    public static bool IsBossMonster(Collider collider)
    {
        for (int i = 0; i < CurrentSceneBossMonster.hitBoxes.Count(); i++)
        {
            if (CurrentSceneBossMonster.hitBoxes[i].HitCollider == collider) return true;
        }
        return false;
    }

}