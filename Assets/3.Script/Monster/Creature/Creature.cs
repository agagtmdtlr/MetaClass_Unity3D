using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Creature : MonoBehaviour , IDamagable
{
    [System.Serializable]
    public enum MoveType
    {
        None,
        NavMeshAgent
    }
    public MoveType moveType = MoveType.None;
    
    [System.Serializable]
    public enum AttackType
    {
        Melee,
        Ranged
    }
    public AttackType attackType = AttackType.Melee;
    
    private static readonly int ATTACK_INDEX = Animator.StringToHash("AttackIndex");
    private static readonly int ATTACK = Animator.StringToHash("Attack");
    public int hp = 1000;
    
    [System.Serializable]
    public class BaseStat
    {
        public int hp;
    }

    public class Events
    {
        public Action<int, int> OnDamage;
    }

    public Animator animator;
    public NavMeshAgent agent;
    
    // TODO: add collider
    public Collider MainCollider { get; }
    public GameObject GameObject => gameObject;
    public Type DamageableType => typeof(Creature);
    public Events events = new Events();
    BaseStat stat = new BaseStat();
    
    List<HitBox> hitboxes = new List<HitBox>();
    public readonly Dictionary<string,List<Scratch>> ScratchesPerArea
        = new Dictionary<string, List<Scratch>>();
    
    [SerializeField] private AnimStateEventListener listener;
    public float relaxTime;
    
    Coroutine updateMoveCoroutine;
    Coroutine updateAttackCoroutine;
    [HideInInspector] public bool isAttacking = false;

    public IAttackStrategy AttackStrategy;
    public IMoveStrategy MoveStrategy;
    
    private void Awake()
    {
        hitboxes = GetComponentsInChildren<HitBox>(true).ToList();
        foreach (var hitbox in hitboxes)
        {
            CombatSystem.Instance.RegisterMonster(hitbox, this);

            if (hitbox.TryGetComponent(out Scratch scratch) == false)
                continue;

            var strKey = hitbox.DamageArea.ToString();
            if (ScratchesPerArea.ContainsKey(strKey) == false)
                ScratchesPerArea.Add(strKey, new List<Scratch>());
            
            ScratchesPerArea[strKey].Add(scratch);
            scratch.enabled = false;
        }
        stat.hp = hp;
        
        listener.OnCallEvent += OnAnimStateEvent;
    }
    
    

    private void OnAnimStateEvent(string eventName, string parameter)
    {
        switch (eventName)
        {
            case "BeginAttack":
                BeginAttack(parameter);
                break;
            case "EndAttack":
                EndAttack(parameter);
                break;
            case "CompleteAttack":
                isAttacking = false;
                break;
            case "EndDeath":
                OnEndDeath();
                break;
                    
        }
    }

    private void BeginAttack(string key)
    {
        var sracths = ScratchesPerArea[key];
        foreach (var scratch in sracths)
            scratch.enabled = true;
    }

    private void EndAttack(string key)
    {
        var sracths = ScratchesPerArea[key];
        foreach (var scratch in sracths)
            scratch.enabled = false;
    }
    

    private void Start()
    {
        updateMoveCoroutine = StartCoroutine(UpdateMove());
        updateAttackCoroutine  = StartCoroutine(UpdateAttack());
    }

    private IEnumerator UpdateMove()
    {
        while (gameObject.activeInHierarchy)
        {
            var targetPos = Player.localPlayer.transform.position;
            agent.SetDestination(targetPos);
            animator.SetFloat("Speed", 1f);
            
            yield return new WaitForSeconds(relaxTime);
        }
    }

    private IEnumerator UpdateAttack()
    {
        while (gameObject.activeInHierarchy)
        {
            var dist = Player.localPlayer.transform.position - transform.position;
            if (dist.magnitude > 1f)
            {
                yield return null;
                continue;
            }
            
            int attackIndex = Random.Range(0, 2);
            animator.SetInteger(ATTACK_INDEX, attackIndex);
            animator.ResetTrigger(ATTACK);
            animator.SetTrigger(ATTACK);
        
            isAttacking = true;
            while (isAttacking)
            {
                yield return null;
            }
            
        }
        
        
        
    }

    private void OnDestroy()
    {
        foreach (var hitbox in hitboxes)
        {
            CombatSystem.Instance.UnregisterMonster(hitbox);
        }
    }


    public void TakeDamage(CombatEvent combatEvent)
    {
        if (stat.hp <= 0)
        {
            return; // already death do noting
        }
        
        var damage =combatEvent.Sender.Damage;
        switch (combatEvent.HitBox.DamageArea)
        {
            case DamageArea.Head:
                damage *= 2;
                break;
            case DamageArea.Body:
                break;
            case DamageArea.LeftArm:
            case DamageArea.RightArm:
            case DamageArea.LeftLeg:
            case DamageArea.RightLeg:
                damage += damage / 2;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        TakeDamageEvent evt = new TakeDamageEvent()
        {
            Taker = this,
            Damage = damage,
            HitPosition = combatEvent.HitPosition,
            HitNormal = combatEvent.HitNormal,
            HitBox = combatEvent.HitBox
        };
        CombatSystem.Instance.AddGameEvent(evt);
        
        stat.hp -= damage;
        stat.hp = Mathf.Clamp(stat.hp, 0, stat.hp);
        events.OnDamage?.Invoke(stat.hp, stat.hp);  


        if (stat.hp <= 0)
        {
            OnDeath();
        }
    }

    public void OnDeath()
    {
        if(agent is not null)
            agent.enabled = false;
        
        animator.SetTrigger("Death");
        StopAllCoroutines();

        DeathEvent evt = new DeathEvent()
        {
            Dead = this,
            DeathPosition = transform.position
        };
        CombatSystem.Instance.AddGameEvent(evt);
    }

    private void OnEndDeath()
    {
        Destroy(gameObject);
    }
}
