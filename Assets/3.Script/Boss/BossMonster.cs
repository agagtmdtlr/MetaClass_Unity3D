using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Build.Content;
using UnityEngine;

public partial class BossMonster : MonoBehaviour , IDamagable
{
    public class BaseStat
    {
        public const int HIT_COUNT = 20;
        public int CurrentHp { get; set; }
        public int CurrentHitCount { get; set; }
    }
    
    public class Events
    {
        /// <summary>
        /// int currentHp, int maxHp
        /// </summary>
        public Action<int,int> OnDamage;
        public Action<BossState.StateName> OnChangedState;
    }
    
    public GameObject GameObject => gameObject;
 
    public Events Event = new Events();
    public BaseStat BossStat = new BaseStat();
    private List<HitBox> hitBoxes;
    
    public Animator Animator { get; private set; }
    private Dictionary<BossState.StateName, BossState> stateDic 
        = new Dictionary<BossState.StateName, BossState>();
    
    public int Hp;
    
    private BossState previousState;
    private BossState currentState;

    private void Awake()
    {
        Animator = GetComponentInChildren<Animator>();
        BossStat.CurrentHp = Hp;
        CurrentSceneBossMonster = this;

        BossState[] myState =  gameObject.GetComponentsInChildren<BossState>(true);
        for (int i = 0; i < myState.Length; i++)
        {
            var state = myState[i];
            state.Initialize(this);
            stateDic.Add(state.Name, state);
        }
            
        ChangeState(BossState.StateName.IdleState);
    }

    private void Start()
    {
        ChangeHp(0);
        hitBoxes = GetComponentsInChildren<HitBox>(true).ToList();
        foreach (var hitbox in hitBoxes)
        {
            CombatSystem.Instance.RegisterMonster(hitbox, this);
        }
    }

    private void OnDestroy()
    {
        foreach (var hitbox in hitBoxes)
        {
            CombatSystem.Instance.UnregisterMonster(hitbox);
        }
    }

    public void TakeDamage(CombatEvent combatEvent)
    {
        ChangeHp(-combatEvent.Damage);
    }

    public void ChangeState(BossState.StateName state)
    {
        previousState = currentState;
        
        if (previousState != null)
        {
            previousState.Exit();
            previousState.gameObject.SetActive(false);
        }
        
        currentState = stateDic[state];
        currentState.Enter();
        currentState.gameObject.SetActive(true);
    }
    
    public void ChangeHp(int amount)
    {
        BossStat.CurrentHp += amount;
        BossStat.CurrentHp = Mathf.Clamp(BossStat.CurrentHp, 0, Hp);
        Event.OnDamage?.Invoke(BossStat.CurrentHp, Hp);

        if (BossStat.CurrentHp <= 0)
        {
            Animator.ResetTrigger(SCRATCH);
            Animator.ResetTrigger(BREATH);
            Animator.ResetTrigger(HIT);
            Animator.SetTrigger(DEAD);
        }
        else
        {
            BossStat.CurrentHitCount++;
            if (BossStat.CurrentHitCount >= BaseStat.HIT_COUNT)
            {
                BossStat.CurrentHitCount = 0;
                Animator.SetTrigger(HIT);
            }
        }
    }
    
    
}
