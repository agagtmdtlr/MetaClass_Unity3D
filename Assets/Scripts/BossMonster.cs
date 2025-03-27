using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public partial class BossMonster : MonoBehaviour
{
    [System.Serializable]
    public class BossStat
    {
        public int HIT_COUNT;
        
        public int Hp;
        public int CurrentHp { get; set; }
        public int CurrentHitCount { get; set; }
    }
    
    public BossStat bossStat;
    private BossState previousState;
    private BossState currentState;
    
    [HideInInspector] public Animator animator;
    
    private readonly Dictionary<BossState.StateName, BossState> stateDic 
        = new Dictionary<BossState.StateName, BossState>();

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        
        bossStat.CurrentHp = bossStat.Hp;
        
        CrrentSceneBossMonster = this;
        s_bossMonsterColliders = GetComponentsInChildren<Collider>();
     
        BossState[] myStates = gameObject.GetComponentsInChildren<BossState>(true);
        foreach (var state in myStates)
        {
            stateDic[state.Name] = state;
            state.Intialize(this);
        }
    }

    public void ChangeState(BossState.StateName newState)
    {
        previousState = currentState;
        if (previousState is not null)
        {
            previousState.Exit();
            previousState.gameObject.SetActive(false);
        }
        
        currentState = stateDic[newState];
        currentState.Enter();
        currentState.gameObject.SetActive(true);
    }
    
    public void ChangeHp(int hp)
    {
        bossStat.CurrentHp += hp;
        if (bossStat.CurrentHp <= 0)
        {
            animator.ResetTrigger(SCRATCH);
            animator.ResetTrigger(BREATH);
            animator.ResetTrigger(HIT);
            animator.SetTrigger(DEAD);
        }
        else
        {
            bossStat.CurrentHitCount++;
            if (bossStat.CurrentHitCount >= bossStat.HIT_COUNT)
            {
                animator.SetTrigger(HIT);
                bossStat.CurrentHitCount = 0;
            }

        }
    }
}
