using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Creature : MonoBehaviour , IDamagable
{
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
    public GameObject GameObject => gameObject;
    public Events events = new Events();
    BaseStat stat = new BaseStat();
    
    List<HitBox> hitboxes = new List<HitBox>();
    public readonly Dictionary<string,List<Scratch>> ScratchesPerArea
        = new Dictionary<string, List<Scratch>>();
    
    [SerializeField] private AnimStateEventListener listener;
    public float relaxTime;
    
    Coroutine updateCoroutine;
    [HideInInspector] public bool isAttacking = false;

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
        if(agent is not null)
            updateCoroutine = StartCoroutine(UpdateMove());
        else
            updateCoroutine = StartCoroutine(UpdateIdle());
        
        HpBar.instance.RegisterMonster(this);
    }


    private IEnumerator UpdateIdle()
    {
        while (gameObject.activeInHierarchy)
        {
            var targetPos = Player.localPlayer.transform.position;
            yield return StartCoroutine(UpdateAttack());
            yield return new WaitForSeconds(relaxTime);
        }
    }

    private IEnumerator UpdateMove()
    {
        while (gameObject.activeInHierarchy)
        {
            var targetPos = Player.localPlayer.transform.position;
            agent.SetDestination(targetPos);
            yield return null;
            
            while (agent.pathPending)
                yield return null;

            while (agent.hasPath)
            {
                animator.SetFloat("Speed", 1f);
                yield return null;
            }
            animator.SetFloat("Speed", 0f);

            var dist = Player.localPlayer.transform.position - transform.position;
            if (dist.magnitude < 0.5f)
            {
                yield return StartCoroutine(UpdateAttack());
            }
            yield return new WaitForSeconds(relaxTime);
        }
    }

    private IEnumerator UpdateAttack()
    {
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
        
        stat.hp -= combatEvent.Damage;
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
    }

    private void OnEndDeath()
    {
        Destroy(gameObject);
    }
}
